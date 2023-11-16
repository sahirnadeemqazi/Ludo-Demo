using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        public GridCell.Color selectedColor;
        public bool isChipMoving;
        public int currentDiceValue;

        public List<GridCell> gridCells;
        public List<GridCell> redHomeCells,blueHomeCells,greenHomeCells,yellowHomeCells;
        public GridCell[] startingPoints;
        public List<GridCell> selectedWinGridCells;

        public GameObject ludoBoard;

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
    }
}
