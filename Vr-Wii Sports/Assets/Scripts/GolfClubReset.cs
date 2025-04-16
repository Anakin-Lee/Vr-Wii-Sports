using System.Collections;
using UnityEngine;

public class GolfClubReset : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    [SerializeField] private float resetDelay = 3f;
    [SerializeField] private float fallThreshold = -5f;

    private Rigidbody rb;
    private bool isResetting = false;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isResetting && transform.position.y < fallThreshold)
        {
            Debug.Log("Golf club fell below threshold. Starting reset...");
            StartResetTimer();
        }
    }

    public void StartResetTimer()
    {
        isResetting = true;
        StopAllCoroutines();
        StartCoroutine(ResetAfterDelay(resetDelay));
    }

    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Resetting golf club...");

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        transform.position = initialPosition;
        transform.rotation = initialRotation;

        yield return null;
        rb.isKinematic = false;

        isResetting = false; // allow future resets
    }
}
