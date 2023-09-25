using Gameplay.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 1.0f;
    private GameObject immuneObject;
    private float timeToDestroy = 5f;
    private float nextTimeToDestroy;

    private void OnEnable()
    {
        rb.velocity = transform.up * moveSpeed;
        nextTimeToDestroy = timeToDestroy + Time.time;
        TimeUpdater.OnTimeTick.AddListener(DestroyAfterTime);
    }

    private void OnDisable()
    {
        TimeUpdater.OnTimeTick.RemoveListener(DestroyAfterTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == immuneObject) return;

        if (other.TryGetComponent<IHitable>(out var hitable))
        {
            hitable.Hit();
            gameObject.SetActive(false);
        }
    }

    public void MarkImmuneObject(GameObject newImmuneObject)
    {
        immuneObject = newImmuneObject;
    }

    private void DestroyAfterTime()
    {
        if (nextTimeToDestroy < Time.time)
        {
            gameObject.SetActive(false);
        }
    }
}
