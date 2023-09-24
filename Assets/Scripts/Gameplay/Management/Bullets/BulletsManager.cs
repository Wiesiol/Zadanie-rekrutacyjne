using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Management
{
    public class BulletsManager : MonoBehaviour
    {
        public static readonly UnityEvent OnTimeToShootElapsed = new();
        private static readonly UnityEvent<Vector3, Quaternion, GameObject> OnShoot = new();
        public static void InvokeShoot(Vector3 bulletPosition, Quaternion bulletRotation, GameObject immuneObject)
            => OnShoot.Invoke(bulletPosition, bulletRotation, immuneObject);

        [SerializeField] private Bullet bulletPrefab;
        private List<Bullet> bullets = new();
        private PoolSpawner<Bullet> pool;

        private float firstShootTime = 3f;
        private float timeBetweenShoot = 2;
        private float nextTimeToShoot;

        private void Awake()
        {
            pool = new(bullets, bulletPrefab);
            nextTimeToShoot = Time.time + timeBetweenShoot;
        }

        private void OnEnable()
        {
            TimeUpdater.OnTimeTick.AddListener(UpdateTimeToShoot);
            OnShoot.AddListener(SpawnBullet);
        }

        private void OnDisable()
        {
            TimeUpdater.OnTimeTick.RemoveListener(UpdateTimeToShoot);
            OnShoot.RemoveListener(SpawnBullet);
        }

        private void UpdateTimeToShoot()
        {
            if(nextTimeToShoot < Time.time)
            {
                nextTimeToShoot = Time.time + timeBetweenShoot;
                OnTimeToShootElapsed.Invoke();
            }
        }

        private void SpawnBullet(Vector3 bulletPosition, Quaternion bulletRotation, GameObject immuneObject)
        {
            var bullet = pool.Spawn(bulletPosition, bulletRotation, null);
            bullet.MarkImmuneObject(immuneObject);
        }
    }
}
