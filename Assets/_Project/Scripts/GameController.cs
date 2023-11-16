using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        public GridCell.Color selectedColor;
        public bool isChipMoving;
        public int currentDiceValue;

        public List<GridCell> gridCells;
        public List<GridCell> redHomeCells, blueHomeCells, greenHomeCells, yellowHomeCells;
        public GridCell[] startingPoints;
        public List<GridCell> selectedWinGridCells;

        public GameObject ludoBoard;

        [SerializeField] private List<PlayerChip> _redPlayerChips;
        [SerializeField] private List<PlayerChip> _bluePlayerChips;
        [SerializeField] private List<PlayerChip> _greenPlayerChips;
        [SerializeField] private List<PlayerChip> _yellowPlayerChips;

        [SerializeField] private string redChipSpriteAddress;
        [SerializeField] private string blueChipSpriteAddress;
        [SerializeField] private string greenChipSpriteAddress;
        [SerializeField] private string yellowChipSpriteAddress;
        [SerializeField] private string boardAddress;

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
            LoadAddressables();
        }

        private void LoadAddressables()
        {
            var addressables = Addressables.InitializeAsync();

            addressables.Completed += _ =>
            {
                var redSprites = Addressables.LoadAssetAsync<Sprite>(redChipSpriteAddress);
                var blueSprites = Addressables.LoadAssetAsync<Sprite>(blueChipSpriteAddress);
                var greenSprites = Addressables.LoadAssetAsync<Sprite>(greenChipSpriteAddress);
                var yellowSprites = Addressables.LoadAssetAsync<Sprite>(yellowChipSpriteAddress);
                var boardSprite = Addressables.LoadAssetAsync<Sprite>(boardAddress);
                
                redSprites.Completed += operation =>
                {
                    foreach (var redChip in _redPlayerChips)
                    {
                        redChip.GetComponent<SpriteRenderer>().sprite = operation.Result;
                    }
                };
                
                blueSprites.Completed += operation =>
                {
                    foreach (var blueChip in _bluePlayerChips)
                    {
                        blueChip.GetComponent<SpriteRenderer>().sprite = operation.Result;
                    }
                };
                
                greenSprites.Completed += operation =>
                {
                    foreach (var greenChip in _greenPlayerChips)
                    {
                        greenChip.GetComponent<SpriteRenderer>().sprite = operation.Result;
                    }
                };
                
                yellowSprites.Completed += operation =>
                {
                    foreach (var yellowChip in _yellowPlayerChips)
                    {
                        yellowChip.GetComponent<SpriteRenderer>().sprite = operation.Result;
                    }
                };
                
                boardSprite.Completed += operation =>
                {
                    ludoBoard.GetComponent<SpriteRenderer>().sprite = operation.Result;
                };
            };
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
