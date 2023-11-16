
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        public GridCell.Color selectedColor;
        public bool isChipMoving;
        public bool isGeneratingRandom;
        public int currentDiceValue;

        public List<GridCell> gridCells;
        public List<GridCell> redHomeCells, blueHomeCells, greenHomeCells, yellowHomeCells;
        public GridCell[] startingPoints;
        public List<GridCell> selectedWinGridCells;

        public GameObject ludoBoard;

        public List<PlayerChip> _redPlayerChips;
        public List<PlayerChip> _bluePlayerChips;
        public List<PlayerChip> _greenPlayerChips;
        public List<PlayerChip> _yellowPlayerChips;
        

        public List<Sprite> diceFaces;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            LoadAddressables.InitializeAddressables();
        }

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
