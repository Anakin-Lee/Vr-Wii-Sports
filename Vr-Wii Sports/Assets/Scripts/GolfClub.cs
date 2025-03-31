using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfClub : MonoBehaviour
{
    public float forceMultiplier = 10f; // Adjust for sensitivity

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GolfBall"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                // Calculate the force based on club speed and direction
                Vector3 clubVelocity = GetComponent<Rigidbody>().velocity;
                Vector3 forceDirection = collision.contacts[0].normal * -1f; // Reflective force

                float impactForce = clubVelocity.magnitude * forceMultiplier;
                ballRigidbody.AddForce(forceDirection * impactForce, ForceMode.Impulse);
            }
        }
    }
}

public class GolfBallReset : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    public void ResetBall()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = initialPosition;
    }
}

// Attach the GolfClub script to your VR golf club object (with a Rigidbody and Collider)
// Attach the GolfBallReset script to the golf ball object (with a Rigidbody and Collider)
// Ensure both objects have proper colliders and physics applied
// Tag the ball as "GolfBall" for detection

