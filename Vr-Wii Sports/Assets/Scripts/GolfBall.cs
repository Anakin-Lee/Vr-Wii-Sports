using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GolfBall : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded = false;

    [Header("Physics Settings")]
    public float groundDrag = 2f;
    public float airDrag = 0.1f;
    public float stopThreshold = 0.05f;
    public float settleDelay = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Adjust drag based on grounded state
        rb.drag = isGrounded ? groundDrag : airDrag;

        // If the ball is barely moving, start checking for settle
        if (rb.velocity.magnitude < stopThreshold && isGrounded)
        {
            StartCoroutine(SettleBall());
        }
    }

    private IEnumerator SettleBall()
    {
        yield return new WaitForSeconds(settleDelay);
        if (rb.velocity.magnitude < stopThreshold)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            //Debug.Log("Ball has settled.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ground check based on tag or layer
        if (collision.gameObject.CompareTag("Ground") || collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
