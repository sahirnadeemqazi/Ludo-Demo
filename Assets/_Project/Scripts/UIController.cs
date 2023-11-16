using UnityEngine;

namespace _Project.Scripts
{
    public class UIController : MonoBehaviour
    {
        public GameObject mainScreen;
        public GameObject gamePlayScreen;
        
        
        public void RollDiceButton()
        {
            GameController.Instance.currentDiceValue = Random.Range(1, 7);
        }

        public void SelectColor(int colorID)
        {
            GameController.Instance.selectedColor = (GridCell.Color)colorID;
            GameController.Instance.ludoBoard.transform.localEulerAngles = new Vector3(0, 0, (colorID * 90));
            GameController.Instance.AssignSelectedWinCells();
            mainScreen.SetActive(false);
            gamePlayScreen.SetActive(true);
        }
    }
}
