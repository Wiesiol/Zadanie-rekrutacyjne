using Gameplay.Deaths;
using Gameplay.Management;
using System;
using UnityEngine;

namespace Gameplay.Objects
{
    public class HealthManager : MonoBehaviour, IHitable
    {
        [field: SerializeField] public GridOccupier GridOccupier { get; private set; }
        private int maxHealthPoints = 3;
        private int healthPoints = 3;
        private int timeToRespawn = 2;
        private bool wasHited = false;

        public void Hit()
        {
            DecreaseHealth();
        }

        private void OnEnable()
        {
            wasHited = false;
            DeathsCounter.OnGameFinishes.AddListener(DisableGameObject);
        }

        private void OnDisable()
        {
            DeathsCounter.OnGameFinishes.RemoveListener(DisableGameObject);
        }

        private void DisableGameObject()
        {
            FreeGameObject();
        }

        private void DecreaseHealth()
        {
            if (wasHited) return;

            healthPoints -= 1;

            if (healthPoints > 0)
            {
                var calculatedTimeToRespawn = timeToRespawn + Time.time;
                ObjectRespawner.InvokeAddObjectToRespawn(GridOccupier, calculatedTimeToRespawn);
            }

            FreeGameObject();
            DeathsCounter.OnObjectDie.Invoke();

            wasHited = true;
        }

        private void FreeGameObject()
        {
            gameObject.SetActive(false);
            GridOccupier.FreeGridCell();
        }

        public void ResetHealth()
        {
            healthPoints = maxHealthPoints;
        }
    }
}
