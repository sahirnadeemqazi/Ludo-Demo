using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Scripts
{
    // This class manages the initialization and loading of Addressables for various game assets.
    public static class LoadAddressables
    {
        // Addresses for the different sprites used in the game.
        private const string redChipSpriteAddress = "Assets/_Project/Sprites/Ludo Board/ChipRed.png";
        private const string blueChipSpriteAddress = "Assets/_Project/Sprites/Ludo Board/ChipBlue.png";
        private const string greenChipSpriteAddress = "Assets/_Project/Sprites/Ludo Board/ChipGreen.png";
        private const string yellowChipSpriteAddress = "Assets/_Project/Sprites/Ludo Board/ChipYellow.png";
        private const string boardAddress = "Assets/_Project/Sprites/Ludo Board/LudoBoard.png";
        private const string diceFace01 = "Assets/_Project/Sprites/Dice/01.png";
        private const string diceFace02 = "Assets/_Project/Sprites/Dice/02.png";
        private const string diceFace03 = "Assets/_Project/Sprites/Dice/03.png";
        private const string diceFace04 = "Assets/_Project/Sprites/Dice/04.png";
        private const string diceFace05 = "Assets/_Project/Sprites/Dice/05.png";
        private const string diceFace06 = "Assets/_Project/Sprites/Dice/06.png";

        // Initializes Addressables and sets up the loading of sprites.
        public static void InitializeAddressables()
        {
            // Initializing Addressables asynchronously.
            var addressables = Addressables.InitializeAsync();
            // Attaching the LoadAndAssignSprites method as a callback when initialization is completed.
            addressables.Completed += LoadAndAssignSprites;
        }

        // Callback method called when Addressables initialization is completed.
        private static void LoadAndAssignSprites(AsyncOperationHandle<IResourceLocator> handle)
        {
            // Checking if Addressables initialization was successful.
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                // Loading various sprites asynchronously.
                var redSprites = Addressables.LoadAssetAsync<Sprite>(redChipSpriteAddress);
                var blueSprites = Addressables.LoadAssetAsync<Sprite>(blueChipSpriteAddress);
                var greenSprites = Addressables.LoadAssetAsync<Sprite>(greenChipSpriteAddress);
                var yellowSprites = Addressables.LoadAssetAsync<Sprite>(yellowChipSpriteAddress);
                var boardSprite = Addressables.LoadAssetAsync<Sprite>(boardAddress);
                var dice01 = Addressables.LoadAssetAsync<Sprite>(diceFace01);
                var dice02 = Addressables.LoadAssetAsync<Sprite>(diceFace02);
                var dice03 = Addressables.LoadAssetAsync<Sprite>(diceFace03);
                var dice04 = Addressables.LoadAssetAsync<Sprite>(diceFace04);
                var dice05 = Addressables.LoadAssetAsync<Sprite>(diceFace05);
                var dice06 = Addressables.LoadAssetAsync<Sprite>(diceFace06);

                // Adding completed event handlers for each loaded sprite.
                redSprites.Completed += operation =>
                {
                    AssignSprites(operation.Result, GameController.Instance._redPlayerChips);
                };
                blueSprites.Completed += operation =>
                {
                    AssignSprites(operation.Result, GameController.Instance._bluePlayerChips);
                };
                greenSprites.Completed += operation =>
                {
                    AssignSprites(operation.Result, GameController.Instance._greenPlayerChips);
                };
                yellowSprites.Completed += operation =>
                {
                    AssignSprites(operation.Result, GameController.Instance._yellowPlayerChips);
                };

                // Assigning the loaded board sprite to the game controller's ludo board.
                boardSprite.Completed += operation =>
                {
                    GameController.Instance.ludoBoard.GetComponent<SpriteRenderer>().sprite = operation.Result;
                };

                // Adding each loaded dice face to the game controller's diceFaces list.
                dice01.Completed += operation => { GameController.Instance.diceFaces.Add(operation.Result); };
                dice02.Completed += operation => { GameController.Instance.diceFaces.Add(operation.Result); };
                dice03.Completed += operation => { GameController.Instance.diceFaces.Add(operation.Result); };
                dice04.Completed += operation => { GameController.Instance.diceFaces.Add(operation.Result); };
                dice05.Completed += operation => { GameController.Instance.diceFaces.Add(operation.Result); };
                dice06.Completed += operation => { GameController.Instance.diceFaces.Add(operation.Result); };
            }
            else
            {
                // Logging an error if Addressables initialization fails.
                Debug.LogError("Failed to load the Addressable asset: " + handle.DebugName);
            }
        }

        // Assigns a sprite to each player chip in the provided list.
        private static void AssignSprites(Sprite sprite, List<PlayerChip> playerChips)
        {
            foreach (var playerChip in playerChips)
            {
                // Setting the sprite for each player chip.
                playerChip.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
    }

}
