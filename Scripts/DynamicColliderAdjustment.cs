using UnityEngine;
using System.Collections.Generic;

public class DynamicColliderAdjustment : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;

    void Start()
    {
        // Get references to SpriteRenderer and PolygonCollider2D components
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        // Ensure we have both components
        if (spriteRenderer == null || polygonCollider == null)
        {
            Debug.LogError("Missing SpriteRenderer or PolygonCollider2D component.");
            return;
        }
    }

    void Update()
    {
        // Check if the sprite has been assigned
        if (spriteRenderer.sprite != null)
        {
            AdjustCollider();
        }
    }

    void AdjustCollider()
    {
        // Get the sprite
        Sprite sprite = spriteRenderer.sprite;

        // List to hold physics shape points
        List<Vector2> physicsShapePoints = new List<Vector2>();

        // Get the number of paths
        int shapeCount = sprite.GetPhysicsShapeCount();
        polygonCollider.pathCount = shapeCount;

        // Set the collider paths based on the physics shape
        for (int i = 0; i < shapeCount; i++)
        {
            physicsShapePoints.Clear();
            sprite.GetPhysicsShape(i, physicsShapePoints);
            polygonCollider.SetPath(i, physicsShapePoints.ToArray());
        }
    }
}