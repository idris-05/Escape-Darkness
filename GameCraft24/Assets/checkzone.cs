using UnityEngine;

public class ZoneCheck : MonoBehaviour
{
    public Transform player;  // The player's transform
    public Vector3 zoneCenter;  // The center of the zone
    public Vector3 zoneSize;    // The size of the zone

    private Bounds zoneBounds;

    void Start()
    {
        // Define the bounds using the center and size of the zone
        zoneBounds = new Bounds(zoneCenter, zoneSize);
    }

    void Update()
    {
        // Check if the player's position is within the zone bounds
        if (zoneBounds.Contains(player.position))
        {
            Debug.Log("Player is within the zone.");
        }
        else
        {
            Debug.Log("Player is outside the zone.");
        }
    }

    // This is just for visualization in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(zoneCenter, zoneSize);
    }
}
