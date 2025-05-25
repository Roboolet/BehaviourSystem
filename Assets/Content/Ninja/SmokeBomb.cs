using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class SmokeBomb : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float throwForce;
    [SerializeField] private GameObject smokePrefab;
    
    public void Launch(GameObject _target)
    {
        Vector3 diff = _target.transform.position - transform.position;
        float force = diff.sqrMagnitude * throwForce;
        rb.AddForce(diff.normalized * (force * 0.7f) + Vector3.up * (force * 0.3f), ForceMode.Impulse);
    }

    public void Explode()
    {
        Instantiate(smokePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Explode();
    }
}
