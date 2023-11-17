using System;
using UnityEngine;

namespace _Project.Scripts
{
    // This class manages the user interface interactions and screens in the game.
    public class UIController : MonoBehaviour
    {
        // Reference to the main screen and gameplay screen GameObjects.
        public GameObject mainScreen;
        public GameObject gamePlayScreen;

        // Method triggered when the Roll Dice button is clicked.
        public void RollDiceButton()
        {
            // Check if the game is already generating a random number or if a chip is in motion.
            if (GameController.Instance.isGeneratingRandom || GameController.Instance.isChipMoving)
                return;

            // Set the flag to indicate that the game is generating a random number.
            GameController.Instance.isGeneratingRandom = true;

            // Start the dice animation.
            GameController.Instance.diceScript.DiceAnimStart();

            // Callback function to handle the generated random number.
            Action<int> randomNumber = (result) =>
            {
                // Update game state variables and stop the dice animation.
                GameController.Instance.isGeneratingRandom = false;
                GameController.Instance.currentDiceValue = result;
                GameController.Instance.diceScript.DiceAnimationStop();
            };

            // Start the coroutine to generate a random number.
            StartCoroutine(GetRandom.GenerateRandomNumber(randomNumber));
        }

        // Method triggered when a color is selected.
        public void SelectColor(int colorID)
        {
            // Set the selected color based on the color ID.
            GameController.Instance.selectedColor = (GridCell.Color) colorID;

            // Rotate the game board based on the selected color.
            GameController.Instance.ludoBoard.transform.localEulerAngles = new Vector3(0, 0, (colorID * 90));

            // Assign selected winning cells based on the selected color.
            GameController.Instance.AssignSelectedWinCells();

            // Switch screens from the main screen to the gameplay screen.
            mainScreen.SetActive(false);
            gamePlayScreen.SetActive(true);
        }

        // Method triggered to reset all chips.
        public void ResetChips()
        {
            // Reset the current dice value and reset all player chips.
            GameController.Instance.currentDiceValue = 0;
            GameController.Instance.ResetChips();
        }
    }

}
