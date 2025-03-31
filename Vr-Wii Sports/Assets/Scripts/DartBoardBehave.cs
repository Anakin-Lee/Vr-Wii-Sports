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
        Vector3 center = new Vector3(0, 0, 0);
        ContactPoint contact = hit.contacts[0]; 
        Vector3 worldHitPoint = contact.point; // world coords
        Vector3 localHitPoint = transform.InverseTransformPoint(worldHitPoint); // local coords

        float dist = Vector3.Distance(center, localHitPoint);
        Debug.Log("This is the distance between center and the last hit spot: " + dist);

        float angle = Mathf.Atan2(localHitPoint.y, localHitPoint.x) * Mathf.Rad2Deg;
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
        if (dist <= 1.0f) //Inner bullseye
        {
            return 50;
        }
        if (dist > 1.0f && dist <= 2.0f) //Outer Bullseye
        {
            return 25;
        }

        int section = Mathf.FloorToInt(angle / 18f);
        int baseScore = dartboardNumbers[section];


        if (dist >= 10.0f && dist <= 14.0f) //triple ring
        {
            return baseScore * 3;
        }
        if (dist >= 16.0f && dist <= 20.0f) //double ring
        {
            return baseScore * 2;
        }
        return baseScore;
    }
}