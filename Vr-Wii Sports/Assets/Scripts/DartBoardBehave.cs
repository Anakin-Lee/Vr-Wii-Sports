using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class DartBoardBehave : MonoBehaviour
{
    public GameObject hitMarker;
    public Transform dartboardCenter;
    [SerializeField] DartBoardScriptable _board;
    private int[] dartboardNumbers = { 6, 13, 4, 18, 1, 20, 5, 12, 9, 14, 11, 8, 16, 7, 19, 3, 17, 2, 15, 10 };
    [SerializeField] private float _number;
    private void Start()
    {
        transform.localScale = new Vector3(_board.radius, _board.radius, _board.radius);
    }
    private void OnValidate()
    {
        Debug.Log("I've been called");
        transform.localScale = new Vector3(_board.radius, _board.radius, _board.radius);
    }
    private void OnCollisionEnter(Collision hit)
    {
        float radius = GetComponent<Collider>().bounds.extents.x;
        Vector3 center = transform.position;
        ContactPoint contact = hit.contacts[0]; 
        Vector3 worldHitPoint = contact.point; // world coords
        Vector3 localHitPoint = transform.InverseTransformPoint(worldHitPoint); // local coords

        float distance = Vector3.Distance(center, localHitPoint) / radius;
        Debug.Log("This is the radius" + distance);
        Debug.Log("This is the distance between center and the last hit spot: " + distance);

        float angle = Mathf.Atan2(-localHitPoint.z, -localHitPoint.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        Debug.Log("Angle of hit: " + angle);

        int score = CalculateScore(localHitPoint, distance, angle);
        Debug.Log("Score: " + score);

        if (hitMarker) Instantiate(hitMarker, worldHitPoint, Quaternion.identity); //Marker prefab placement
    }

    private int CalculateScore(Vector3 localHitPoint, float distance, float angle)
    {
        if (distance <= _board.bullseye) //Inner bullseye
            return 50;
        if (distance <= _board.outerBullseye) //Outer Bullseye
            return 25;

        int section = Mathf.FloorToInt((angle + 9) / 18f) % 20;
        int baseScore = dartboardNumbers[section];

        if (distance >= _board.tripleRingInner && distance <= _board.tripleRingOuter) //triple ring
            return baseScore * 3;
        if (distance >= _board.doubleRingInner && distance <= _board.doubleRingOuter) //double ring
            return baseScore * 2;
        return baseScore;
    }
}