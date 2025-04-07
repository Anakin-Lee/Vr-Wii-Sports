using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
