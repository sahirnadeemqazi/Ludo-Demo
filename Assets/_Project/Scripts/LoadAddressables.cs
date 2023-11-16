using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Scripts
{
    public class LoadAddressables
    {
        
        public void LoadSprite(string address,SpriteRenderer spriteRenderer)
        {
            // Load the Addressable asset asynchronously
            AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(address);

            // Attach a completed callback
            handle.Completed += OnLoadComplete;
        }

        void OnLoadComplete(AsyncOperationHandle<Sprite> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                
            }
            else
            {
                Debug.LogError("Failed to load the Addressable asset: " + handle.DebugName);
            }
        }
    }
}
