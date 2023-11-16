using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts
{
    public class PlayerChip : MonoBehaviour
    {
        public GridCell.Color thisChipColor;
        private bool _isInWinCells;
        private bool _isInHome = true;

        private Vector3 _startingPosition;
        private GridCell _currentGridCell;
        private int _counter = 0;

        public void Start()
        {
            _startingPosition = transform.position;
        }

        public void ResetChip()
        {
            transform.position = _startingPosition;
        }

        private void OnMouseDown()
        {
            if (GameController.Instance.selectedColor != thisChipColor)
            {
                return;
            }
            
            if (!GameController.Instance.isChipMoving)
            {
                if (_isInHome)
                {
                    if (GameController.Instance.currentDiceValue != 6)
                    {
                        return;
                    }
                    else
                    {
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
                    if (GameController.Instance.currentDiceValue > 0)
                    {
                        if (_isInWinCells)
                        {
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


        private void MoveChip()
        {
            if (_isInWinCells)
            {
                int startingIndex = GameController.Instance.selectedWinGridCells.IndexOf(_currentGridCell);
                transform.DOMove(GameController.Instance.selectedWinGridCells[startingIndex + 1].transform.position,
                    0.1f).OnComplete(() =>
                {
                    MovementComplete(GameController.Instance.selectedWinGridCells[startingIndex + 1]);
                });
            }
            else
            {
                if (_currentGridCell.isEntryCell && _currentGridCell.thisCellColor == thisChipColor)
                {
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
                    int startingIndex = GameController.Instance.gridCells.IndexOf(_currentGridCell);
                    int nextIndex = startingIndex + 1;
                    if (startingIndex == GameController.Instance.gridCells.Count - 1)
                    {
                        nextIndex = 0;
                    }
                    transform.DOMove(GameController.Instance.gridCells[nextIndex].transform.position,
                        0.1f).OnComplete(() =>
                    {
                        MovementComplete(GameController.Instance.gridCells[nextIndex]);
                    });
                }
            }
        }

        private void MovementComplete(GridCell gridCell)
        {
            _currentGridCell = gridCell;
            _counter++;
            if (_counter != GameController.Instance.currentDiceValue)
            {
                MoveChip();
            }
            else
            {
                GameController.Instance.isChipMoving = false;
                GameController.Instance.currentDiceValue = 0;
                if (_currentGridCell.isWinCell)
                {
                    Debug.Log("This Chip Won");
                    GetComponent<Collider>().enabled = false;
                }
            }
        }
    }
}
