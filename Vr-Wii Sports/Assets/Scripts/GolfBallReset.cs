using System.Collections;
using UnityEngine;

public class GolfBallReset : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void StartResetTimer()
    {
        Debug.Log("Reset timer started...");
        StopAllCoroutines(); // Prevent overlapping resets
        StartCoroutine(ResetAfterDelay(3f));
    }

    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Resetting golf ball...");

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Temporarily disable physics so the ball doesn't freak out
        rb.isKinematic = true;

        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Re-enable physics next frame
        yield return null;
        rb.isKinematic = false;
    }
}
