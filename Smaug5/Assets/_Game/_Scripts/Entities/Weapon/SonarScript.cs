using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SonarScript : MonoBehaviour
{
    public float force = 700f;
    private List<GameObject> affectedObjects = new ();

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("FUSRODAH");
            affectedObjects.ForEach(obj =>
            {
                var rb = obj.GetComponent<Rigidbody>();
                rb.AddExplosionForce(force, transform.position, 15f, 2f);
            });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            affectedObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            affectedObjects.Remove(other.gameObject);
        }
    }
}