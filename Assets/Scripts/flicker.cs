using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flicker : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float flickerInterval = 0.2f; // Adjust this to change the flicker speed
    private float nextFlickerTime;
    private float firstTime;
    private float inTime;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        //firstTime = Time.time;
        nextFlickerTime = Time.time + flickerInterval;
        
    }

    private void Update()
    {
        //inTime = Time.time - firstTime;
        if (Time.time > nextFlickerTime)
        {
            // Toggle the sprite's visibility by enabling/disabling the SpriteRenderer component
            spriteRenderer.enabled = !spriteRenderer.enabled;

            // Set the time for the next flicker
            nextFlickerTime = Time.time + flickerInterval;
        }
    }
}