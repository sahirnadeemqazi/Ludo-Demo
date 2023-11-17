
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    // This class manages the overall game state and holds references to various game elements.
    public class GameController : MonoBehaviour
    {
        // Singleton instance of the GameController.
        public static GameController Instance;

        // The currently selected color for player actions.
        public GridCell.Color selectedColor;

        // Flags to track if a chip is currently in motion and if the game is generating a random number.
        public bool isChipMoving;
        public bool isGeneratingRandom;

        // The current value of the dice.
        public int currentDiceValue;

        // Lists to store references to different types of grid cells.
        public List<GridCell> gridCells;
        public List<GridCell> redHomeCells, blueHomeCells, greenHomeCells, yellowHomeCells;

        // Array of starting points for player chips.
        public GridCell[] startingPoints;

        // List to store selected winning grid cells based on the selected color.
        public List<GridCell> selectedWinGridCells;

        // Reference to the ludo board GameObject.
        public GameObject ludoBoard;

        // Lists to store references to player chips of different colors.
        public List<PlayerChip> _redPlayerChips;
        public List<PlayerChip> _bluePlayerChips;
        public List<PlayerChip> _greenPlayerChips;
        public List<PlayerChip> _yellowPlayerChips;

        // List to store references to different dice face sprites.
        public List<Sprite> diceFaces;

        // Reference to the Dice script.
        public Dice diceScript;

        // Counter for tracking the number of winning chips.
        public int winChipsCount;

        // Called when the script instance is being loaded.
        private void Awake()
        {
            // Ensuring that only one instance of GameController exists.
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                // If an instance already exists, destroy this duplicate instance.
                Destroy(this.gameObject);
            }
        }

        // Called when the script is enabled.
        private void Start()
        {
            // Initializing Addressables to load game assets.
            LoadAddressables.InitializeAddressables();
        }

        // Assigns the selected winning grid cells based on the selected color.
        public void AssignSelectedWinCells()
        {
            switch (selectedColor)
            {
                case GridCell.Color.Red:
                    selectedWinGridCells = new List<GridCell>(redHomeCells);
                    break;
                case GridCell.Color.Blue:
                    selectedWinGridCells = new List<GridCell>(blueHomeCells);
                    break;
                case GridCell.Color.Green:
                    selectedWinGridCells = new List<GridCell>(greenHomeCells);
                    break;
                case GridCell.Color.Yellow:
                    selectedWinGridCells = new List<GridCell>(yellowHomeCells);
                    break;
            }
        }

        // Resets all player chips to their initial state.
        public void ResetChips()
        {
            foreach (var playerChip in _redPlayerChips)
            {
                playerChip.ResetChip();
            }

            foreach (var playerChip in _bluePlayerChips)
            {
                playerChip.ResetChip();
            }

            foreach (var playerChip in _greenPlayerChips)
            {
                playerChip.ResetChip();
            }

            foreach (var playerChip in _yellowPlayerChips)
            {
                playerChip.ResetChip();
            }
        }
    }

}
