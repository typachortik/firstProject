using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;

    public event Action Triggered = delegate { };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(typeof(PlayerMovement), out var playerComponent))
        {
            Triggered?.Invoke();
            obstacle.SetActive(false);
            Debug.Log("Hello brother!");
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(typeof(PlayerMovement), out var playerComponent))
        {
            obstacle.SetActive(true);
            Debug.Log("Bye brother!");
        }
    }
}
