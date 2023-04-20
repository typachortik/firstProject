using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PointsTrigger : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(typeof(PlayerMovement), out var playerComponent))
        {
            Triggered?.Invoke();
            obstacle.SetActive(false);
            _points++;
            _pointsCounter.text = $" {_points}";
        }
    }
}
