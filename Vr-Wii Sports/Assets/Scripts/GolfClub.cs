using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfClub : MonoBehaviour
{
    public float forceMultiplier = 10000000000000f; // Adjust for sensitivity

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision logged");
        Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (!collision.gameObject.CompareTag("GolfBall")) return;
        if (ballRigidbody == null) return;

        // Calculate the force based on club speed and direction
        Vector3 clubVelocity = GetComponent<Rigidbody>().velocity;
        Vector3 forceDirection = collision.contacts[0].normal * -1f; // Reflective force

        float impactForce = clubVelocity.magnitude * forceMultiplier;
        ballRigidbody.AddForce(forceDirection * impactForce, ForceMode.Impulse);
    }
}
