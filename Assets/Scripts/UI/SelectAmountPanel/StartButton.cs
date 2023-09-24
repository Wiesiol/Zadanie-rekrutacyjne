using Gameplay.Management;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.SelectAmountPanel
{
    public class StartButton : MonoBehaviour
    {
        public static readonly UnityEvent<int> OnAmountSelected = new();

        [SerializeField] private Sprite readyToUseSprite;
        [SerializeField] private Button startButton;
        [SerializeField] private GameObject gamepadHint;
        [SerializeField] private GameObject panel;

        private int currentAmount;

        private void OnEnable()
        {
            OnAmountSelected.AddListener(SelectAmout);
            startButton.onClick.AddListener(SpawnObjects);
            startButton.onClick.AddListener(DisablePanel);
        }

        private void OnDisable()
        {
            OnAmountSelected.RemoveListener(SelectAmout);
            startButton.onClick.RemoveListener(SpawnObjects);
            startButton.onClick.RemoveListener(DisablePanel);
        }

        private void SelectAmout(int amount)
        {
            currentAmount = amount;

            EnableStartButton();
        }

        private void EnableStartButton()
        {
            startButton.interactable = true;
            startButton.image.sprite = readyToUseSprite;
            gamepadHint.SetActive(true);
        }

        private void SpawnObjects()
        {
            ObjectsSpawner.OnObjectsAmountSpawn.Invoke(currentAmount);
        }

        private void DisablePanel()
        {
            panel.SetActive(false);
        }
    }
}