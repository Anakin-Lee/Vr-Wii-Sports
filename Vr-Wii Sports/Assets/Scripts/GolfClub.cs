using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfClub : MonoBehaviour
{
    public float forceMultiplier = 10f;
    [SerializeField, Range(0f, 1f)] public float launchAngle = 0f;

    private Vector3 lastPosition;
    private Vector3 calculatedVelocity;

    private AudioSource hitSound; // Add this

    private void Start()
    {
        hitSound = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    private void FixedUpdate()
    {
        calculatedVelocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
        lastPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision logged");

        if (!collision.gameObject.CompareTag("GolfBall")) return;

        Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (ballRigidbody == null) return;

        Vector3 clubVelocity = calculatedVelocity;
        Vector3 forceDirection = clubVelocity.normalized + Vector3.up * launchAngle;
        float impactForce = clubVelocity.magnitude * forceMultiplier;

        //Debug.Log($"Club velocity: {clubVelocity}, Force Direction: {forceDirection}, Impact Force: {impactForce}");

        ballRigidbody.AddForce(forceDirection * impactForce, ForceMode.Impulse);

        // Play sound
        if (hitSound != null) hitSound.Play();
    }
}
