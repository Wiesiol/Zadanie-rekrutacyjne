using System;
using System.Collections;
using System.Collections.Generic;
using UI.SelectAmountPanel;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Deaths
{
    public class DeathsCounter : MonoBehaviour
    {
        public static readonly UnityEvent OnObjectDie = new();
        public static readonly UnityEvent OnGameFinishes = new();

        private int deathsCount = 0;
        private int maxDeaths = 0;

        private void OnEnable()
        {
            OnObjectDie.AddListener(AddDeath);
            StartButton.OnAmountSelected.AddListener(ChangeMaxDeathsCount);
        }

        private void OnDisable()
        {
            OnObjectDie.RemoveListener(AddDeath);
            StartButton.OnAmountSelected.RemoveListener(ChangeMaxDeathsCount);
        }

        private void AddDeath()
        {
            deathsCount++;

            if (deathsCount == maxDeaths)
            {
                OnGameFinishes.Invoke();
                deathsCount = 0;
            }
        }

        private void ChangeMaxDeathsCount(int objectsSpawned)
        {
            maxDeaths = (objectsSpawned * 3) - 1;
        }
    }
}