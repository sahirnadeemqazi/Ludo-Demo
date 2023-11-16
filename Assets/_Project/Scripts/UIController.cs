using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class UIController : MonoBehaviour
    {
        public GameObject mainScreen;
        public GameObject gamePlayScreen;
        
        
        public void RollDiceButton()
        {
            //GameController.Instance.currentDiceValue = Random.Range(1, 7);
            
            if (GameController.Instance.isGeneratingRandom || GameController.Instance.isChipMoving) return;

            GameController.Instance.isGeneratingRandom = true;
            Action<int> randomNumber = (result) =>
            {
                GameController.Instance.isGeneratingRandom = false;
                GameController.Instance.currentDiceValue = result;
            };

            StartCoroutine(GetRandom.GenerateRandomNumber(randomNumber));
        }

        public void SelectColor(int colorID)
        {
            GameController.Instance.selectedColor = (GridCell.Color)colorID;
            GameController.Instance.ludoBoard.transform.localEulerAngles = new Vector3(0, 0, (colorID * 90));
            GameController.Instance.AssignSelectedWinCells();
            mainScreen.SetActive(false);
            gamePlayScreen.SetActive(true);
        }

        public void ResetChips()
        {
            GameController.Instance.currentDiceValue = 0;
            GameController.Instance.ResetChips();
        }
    }
}
