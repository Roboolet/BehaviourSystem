using System;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField] private float lifetime;
    private float startTime;
    private Vector3 startScale;

    private void Awake()
    {
        startTime = Time.time;
        startScale = transform.localScale;
    }

    private void Update()
    {
        float progress = (Time.time - startTime) / lifetime;
        transform.localScale = Vector3.Lerp(startScale, Vector3.zero, progress);

        if (progress >= 1)
        {
            Destroy(gameObject);
        }
    }
}
