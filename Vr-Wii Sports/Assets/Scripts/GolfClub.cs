using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfClub : MonoBehaviour
{
    public float forceMultiplier = 10f;
    [SerializeField, Range(0f, 1f)] public float launchAngle = 0f;

    private Vector3 lastPosition;
    private Vector3 calculatedVelocity;

    private AudioSource hitSound;

    private void Start()
    {
        hitSound = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        calculatedVelocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
        lastPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (!collision.gameObject.CompareTag("GolfBall")) return;

        Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (ballRigidbody == null) return;

        Vector3 clubVelocity = calculatedVelocity;
        Vector3 forceDirection = clubVelocity.normalized + Vector3.up * launchAngle;
        float impactForce = clubVelocity.magnitude * forceMultiplier;

        Debug.Log($"Club velocity: {clubVelocity}, Force direction: {forceDirection}, Impact force: {impactForce}");

        ballRigidbody.AddForce(forceDirection * impactForce, ForceMode.Impulse);

        // Play sound
        if (hitSound != null) hitSound.Play();

        // Start reset timer on the ball
        GolfBallReset resetScript = collision.gameObject.GetComponent<GolfBallReset>();
        if (resetScript != null)
        {
            Debug.Log("Starting ball reset timer...");
            resetScript.StartResetTimer();
        }
        else
        {
            Debug.LogWarning("GolfBallReset component not found on the golf ball.");
        }
    }
}
