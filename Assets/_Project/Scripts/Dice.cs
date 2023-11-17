
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    // This class manages the behavior of a dice GameObject.
    public class Dice : MonoBehaviour
    {
        // Reference to the SpriteRenderer component attached to the dice GameObject.
        private SpriteRenderer _diceSprite;

        // Called when the script instance is being loaded.
        private void Start()
        {
            // Getting the SpriteRenderer component of the dice GameObject.
            _diceSprite = GetComponent<SpriteRenderer>();
        }

        // Initiates the start of the dice animation coroutine.
        public void DiceAnimStart()
        {
            StartCoroutine(StartDiceAnimation());
        }

        // Stops the dice animation coroutine and sets the dice face based on the current dice value.
        public void DiceAnimationStop()
        {
            // Stopping the dice animation coroutine.
            StopCoroutine(StartDiceAnimation());
        
            // Setting the dice face based on the current dice value stored in GameController.
            _diceSprite.sprite = GameController.Instance.diceFaces[GameController.Instance.currentDiceValue - 1];
        }

        // Coroutine for the dice animation.
        private IEnumerator StartDiceAnimation()
        {
            // Generating a random number to simulate a dice roll.
            int random = Random.Range(0, 6);
        
            // Setting the dice face to the randomly chosen sprite.
            _diceSprite.sprite = GameController.Instance.diceFaces[random];

            // Yielding for a short duration to create a visual effect.
            yield return new WaitForSeconds(0.1f);

            // If the game is still generating a random number, continue the dice animation.
            if (GameController.Instance.isGeneratingRandom)
            {
                StartCoroutine(StartDiceAnimation());
            }
        }
    }

}
