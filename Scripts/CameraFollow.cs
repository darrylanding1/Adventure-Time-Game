using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float maxZoomOut = 15f;
    public float zoomSpeed = 5f;
    public Vector3 cameraCenterOffset;

    private Camera cam;
    private float originalOrthographicSize;
    private float originalDistanceY;

    void Start()
    {
        cam = GetComponent<Camera>();
        originalOrthographicSize = cam.orthographicSize;
        originalDistanceY = Mathf.Abs(player1.position.y - player2.position.y);
    }

    void Update()
    {
        if (player1 == null || player2 == null)
        {
            Debug.LogWarning("Both players must be assigned.");
            return;
        }

        // Calculate the distance between players along the y-axis
        float distanceY = Mathf.Abs(player1.position.y - player2.position.y);

        // Calculate the zoom factor based on player distance
        float zoomFactor = Mathf.Clamp(distanceY - originalDistanceY, 0f, maxZoomOut);

        // Set the camera's position to the original position
        Vector3 midpoint = (player1.position + player2.position) / 2;
        Vector3 cameraCenter = midpoint + cameraCenterOffset;
        transform.position = new Vector3(cameraCenter.x, cameraCenter.y, transform.position.z);

        // Reset camera's orthographic size to original value
        cam.orthographicSize = originalOrthographicSize;

        // Zoom out based on the calculated zoom factor
        cam.orthographicSize += zoomFactor;

        // Apply zoom speed
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, originalOrthographicSize + zoomFactor, Time.deltaTime * zoomSpeed);
    }
}
