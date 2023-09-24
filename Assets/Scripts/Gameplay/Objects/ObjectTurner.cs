using DG.Tweening;
using Gameplay.Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Objects
{
    public class ObjectTurner : MonoBehaviour
    {
        private float nextTick;
        private float minTwistTime = 0f;
        private float maxTwistTime = 1f;

        private void OnEnable()
        {
            nextTick = Time.time + Random.Range(minTwistTime, maxTwistTime);
            TimeUpdater.OnTimeTick.AddListener(UpdateTime);
        }

        private void OnDisable()
        {
            TimeUpdater.OnTimeTick.RemoveListener(UpdateTime);
        }

        private void UpdateTime()
        {
            if (nextTick <= Time.time)
            {
                SetRandomRotation();
                nextTick = Time.time + Random.Range(minTwistTime, maxTwistTime);
            }
        }

        private void SetRandomRotation()
        {
            var randomRotationY = Random.Range(0.0f, 360.0f);

            var randomEulerAngles = new Vector3(0, 0, randomRotationY);

            transform.rotation = Quaternion.Euler(randomEulerAngles);
        }
    }
}
