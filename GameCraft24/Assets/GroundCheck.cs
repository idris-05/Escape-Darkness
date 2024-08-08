using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInPolygonChecker : MonoBehaviour
{
    // Reference to the player or object you want to check
    public Transform player;

    // Public variable to indicate if the player is inside the polygon
    public bool check;

    private PolygonCollider2D polygonCollider;
    private Vector2[] polygonPoints;

    void Start()
    {
        // Get the PolygonCollider2D component on this GameObject
        polygonCollider = GetComponent<PolygonCollider2D>();

        // Convert the local points to world space
        polygonPoints = polygonCollider.points;
        for (int i = 0; i < polygonPoints.Length; i++)
        {
            polygonPoints[i] = polygonCollider.transform.TransformPoint(polygonPoints[i]);
        }
    }

    void Update()
    {
        // Get the player's position
        Vector2 playerPosition = player.position;

        // Check if the player's position is within the polygon
        check = IsPointInPolygon(polygonPoints, playerPosition);

        if (check)
        {
            Debug.Log("Player is inside the polygon!");
        }
        else
        {
            Debug.Log("Player is outside the polygon!");
        }
    }

    // Method to check if a point is inside a polygon
    public bool IsPointInPolygon(Vector2[] polygonPoints, Vector2 point)
    {
        int j = polygonPoints.Length - 1;
        bool inside = false;
        for (int i = 0; i < polygonPoints.Length; j = i++)
        {
            if (((polygonPoints[i].y > point.y) != (polygonPoints[j].y > point.y)) &&
                (point.x < (polygonPoints[j].x - polygonPoints[i].x) * (point.y - polygonPoints[i].y) / (polygonPoints[j].y - polygonPoints[i].y) + polygonPoints[i].x))
            {
                inside = !inside;
            }
        }
        return inside;
    }
}
