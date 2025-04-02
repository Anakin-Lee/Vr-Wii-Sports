using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBoardBehave : MonoBehaviour
{
    public GameObject hitMarker;
    public Transform dartboardCenter;

    private int[] dartboardNumbers = { 6, 13, 4, 18, 1, 20, 5, 12, 9, 14, 11, 8, 16, 7, 19, 3, 17, 2, 15, 10 }; 

    private void OnCollisionEnter(Collision hit)
    {
        float radius = GetComponent<Collider>().bounds.extents.x;
        Vector3 center = transform.InverseTransformPoint(dartboardCenter.position);
        ContactPoint contact = hit.contacts[0]; 
        Vector3 worldHitPoint = contact.point; // world coords
        Vector3 localHitPoint = transform.InverseTransformPoint(worldHitPoint); // local coords

        float dist = Vector3.Distance(center, localHitPoint);
        Debug.Log("This is the distance between center and the last hit spot: " + dist);

        float angle = Mathf.Atan2(-localHitPoint.z, -localHitPoint.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;

        Debug.Log("Angle of hit: " + angle);

        int score = CalculateScore(localHitPoint, dist, angle);
        Debug.Log("Score: " + score);




        if (hitMarker)
        {
            Instantiate(hitMarker, worldHitPoint, Quaternion.identity); //Marker prefab placement
        }

    }

    private int CalculateScore(Vector3 localHitPoint, float dist, float angle)
    {
        float bullseye = 1.0f;
        float outerBullseye = 2.0f;
        float tripleRingInner = 8.0f;
        float tripleRingOuter = 12.0f;
        float doubleRingInner = 14.0f;
        float doubleRingOuter = 18.0f;

        if (dist <= bullseye) //Inner bullseye
        {
            return 50;
        }
        if (dist <= outerBullseye) //Outer Bullseye
        {
            return 25;
        }

        int section = Mathf.FloorToInt((angle + 9) / 18f) % 20;
        int baseScore = dartboardNumbers[section];


        if (dist >= tripleRingInner && dist <= tripleRingOuter) //triple ring
        {
            return baseScore * 3;
        }
        if (dist >= doubleRingInner && dist <= doubleRingOuter) //double ring
        {
            return baseScore * 2;
        }
        return baseScore;
    }
}