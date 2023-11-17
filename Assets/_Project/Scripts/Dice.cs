using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class Dice : MonoBehaviour
    {
        private SpriteRenderer _diceSprite;

        private void Start()
        {
            _diceSprite = GetComponent<SpriteRenderer>();
        }

        public void DiceAnimStart()
        {
            StartCoroutine(StartDiceAnimation());
        }

        public void DiceAnimationStop()
        {
            StopCoroutine(StartDiceAnimation());
            _diceSprite.sprite = GameController.Instance.diceFaces[GameController.Instance.currentDiceValue - 1];
        }

        private IEnumerator StartDiceAnimation()
        {
            int random = Random.Range(0, 6);
            _diceSprite.sprite = GameController.Instance.diceFaces[random];
            yield return new WaitForSeconds(0.1f);
            if(GameController.Instance.isGeneratingRandom)
                StartCoroutine(StartDiceAnimation());
        }
    }
}
