using UnityEngine;
using UnityEngine.Events;

public class TimeUpdater : MonoBehaviour
{
    public static readonly UnityEvent OnTimeTick = new();

    private void Update()
    {
        OnTimeTick.Invoke();
    }
}