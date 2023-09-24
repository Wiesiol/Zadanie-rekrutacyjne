using Gameplay.Management;
using UnityEngine;

namespace Gameplay.Objects
{
    public class Shooter : MonoBehaviour, IShootable
    {
        private void OnEnable()
        {
            BulletsManager.OnTimeToShootElapsed.AddListener(Shoot);
        }

        private void OnDisable()
        {
            BulletsManager.OnTimeToShootElapsed.RemoveListener(Shoot);
        }

        public void Shoot()
        {
            BulletsManager.InvokeShoot(transform.position, transform.rotation, gameObject);
        }
    }
}
