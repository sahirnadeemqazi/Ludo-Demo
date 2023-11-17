
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts
{
    // This class represents a player chip on the game board.
    public class PlayerChip : MonoBehaviour
    {
        // The color of this player chip.
        public GridCell.Color thisChipColor;

        // Flags to track if the chip is in winning cells or home.
        private bool _isInWinCells;
        private bool _isInHome = true;

        // The starting position of the chip.
        private Vector3 _startingPosition;

        // Reference to the current grid cell the chip is on.
        private GridCell _currentGridCell;

        // Counter to track the number of moves made by the chip.
        private int _counter = 0;

        // Called when the script instance is being loaded.
        public void Start()
        {
            // Store the initial position of the chip.
            _startingPosition = transform.position;
        }

        // Resets the chip to its starting position.
        public void ResetChip()
        {
            transform.position = _startingPosition;
        }

        // Called when the mouse is pressed down on the chip.
        private void OnMouseDown()
        {
            // Check if it's the player's turn to move chips of this color.
            if (GameController.Instance.selectedColor != thisChipColor)
            {
                return;
            }

            // Check if another chip is already in motion.
            if (!GameController.Instance.isChipMoving)
            {
                // If the chip is in home and the dice value is not 6, return.
                if (_isInHome)
                {
                    if (GameController.Instance.currentDiceValue != 6)
                    {
                        return;
                    }
                    else
                    {
                        // If the dice value is 6, move the chip to the starting point.
                        GameController.Instance.currentDiceValue = 0;
                        GameController.Instance.isChipMoving = true;
                        transform.DOMove(GameController.Instance.startingPoints[(int) thisChipColor].transform.position,
                            0.5f).OnComplete(() =>
                        {
                            _currentGridCell = GameController.Instance.startingPoints[(int) thisChipColor];
                            GameController.Instance.isChipMoving = false;
                            _isInHome = false;
                        });
                    }
                }
                else
                {
                    // If the chip is already on the board, and the dice value is greater than 0, move the chip.
                    if (GameController.Instance.currentDiceValue > 0)
                    {
                        if (_isInWinCells)
                        {
                            // If the chip is in winning cells, check if it can move out based on the dice value.
                            int startingIndex = GameController.Instance.selectedWinGridCells.IndexOf(_currentGridCell);
                            startingIndex = 5 - startingIndex;
                            if (GameController.Instance.currentDiceValue > startingIndex)
                            {
                                GameController.Instance.currentDiceValue = 0;
                                return;
                            }
                        }

                        MoveChip();
                        _counter = 0;
                    }
                }
            }
        }

        // Move the chip based on the current dice value.
        private void MoveChip()
        {
            if (_isInWinCells)
            {
                // If the chip is in winning cells, move to the next cell in the winning cells list.
                int startingIndex = GameController.Instance.selectedWinGridCells.IndexOf(_currentGridCell);
                transform.DOMove(GameController.Instance.selectedWinGridCells[startingIndex + 1].transform.position,
                    0.1f).OnComplete(() =>
                {
                    MovementComplete(GameController.Instance.selectedWinGridCells[startingIndex + 1]);
                });
            }
            else
            {
                // If the chip is on the board, move to the next grid cell.
                if (_currentGridCell.isEntryCell && _currentGridCell.thisCellColor == thisChipColor)
                {
                    // If the current grid cell is an entry cell for the chip color, move to the first winning cell.
                    int nextIndex = 0;
                    transform.DOMove(GameController.Instance.selectedWinGridCells[nextIndex].transform.position,
                        0.1f).OnComplete(() =>
                    {
                        _isInWinCells = true;
                        MovementComplete(GameController.Instance.selectedWinGridCells[nextIndex]);
                    });
                }
                else
                {
                    // If the current grid cell is not an entry cell, move to the next grid cell.
                    int startingIndex = GameController.Instance.gridCells.IndexOf(_currentGridCell);
                    int nextIndex = startingIndex + 1;
                    if (startingIndex == GameController.Instance.gridCells.Count - 1)
                    {
                        nextIndex = 0;
                    }

                    transform.DOMove(GameController.Instance.gridCells[nextIndex].transform.position,
                        0.1f).OnComplete(() => { MovementComplete(GameController.Instance.gridCells[nextIndex]); });
                }
            }
        }

        // Called when the chip movement is complete.
        private void MovementComplete(GridCell gridCell)
        {
            // Update the current grid cell and increment the move counter.
            _currentGridCell = gridCell;
            _counter++;

            // If the chip has more moves, continue moving.
            if (_counter != GameController.Instance.currentDiceValue)
            {
                MoveChip();
            }
            else
            {
                // If all moves are completed, check for other chips on the final position.
                CheckForOtherChipOnThisPosition();

                // Update game state variables.
                GameController.Instance.isChipMoving = false;
                GameController.Instance.currentDiceValue = 0;

                // If the chip is in a win cell, log a message and disable its collider.
                if (_currentGridCell.isWinCell)
                {
                    Debug.Log("This Chip Won");
                    GetComponent<Collider2D>().enabled = false;
                }
            }
        }

        // Check if there are other chips on the final position.
        private void CheckForOtherChipOnThisPosition()
        {
            // Check if there is any opponent on the reached grid point.
            // Logic for dealing with opponents would be implemented here.
        }
    }

}
