using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBoardBehave : MonoBehaviour
{
    public GameObject hitMarker;
    public Transform dartboardCenter;

    private void OnCollisionEnter(Collision hit)
    {
        float radius = GetComponent<Collider>().bounds.extents.x;
        Vector3 center = new Vector3(0, 0, 0);
        ContactPoint contact = hit.contacts[0]; // Get the first contact point
        Vector3 worldHitPoint = contact.point; // Get world coordinates of hit
        Vector3 localHitPoint = transform.InverseTransformPoint(worldHitPoint); // Convert to local coordinates

        float dist = Vector3.Distance(center, localHitPoint);
        Debug.Log("This is the distance between center and the last hit spot: " + dist);

        float angle = Mathf.Atan2(localHitPoint.y, localHitPoint.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360; // Ensure angle is in [0, 360] range

        Debug.Log("Angle of hit: " + angle);


        if (hitMarker)
        {
            Instantiate(hitMarker, worldHitPoint, Quaternion.identity); // Place a marker at the hit point
        }

    }

    private int CalculateScore(Vector3 localHitPoint)
    {
        return 0;
    }
}