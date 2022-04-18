using UnityEngine;

public class AutoZoomByPoint : MonoBehaviour
{
    // Author: Hongda Lin

    // Use [a,b] [c,d] value of the important portion in image
    // Calculate the center point
    // Change the center point position relative to screen position
    // Move camera to the point

    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;

    private Vector3 cameraOriginPos;

    void Awake()
    {
        cameraOriginPos = Camera.main.transform.position;
    }

    void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit point;
        Physics.Raycast(ray, out point, 1000);

        // direction to the mouse position 
        Vector3 Scrolldirection = ray.GetPoint(5);

        float step = zoomSpeed * Time.deltaTime;

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Scrolldirection.y > minZoom)
        {
            transform.position = Vector3.MoveTowards(transform.position, Scrolldirection, Input.GetAxis("Mouse ScrollWheel") * step);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Scrolldirection.y < maxZoom)
        {
            transform.position = Vector3.MoveTowards(transform.position, Scrolldirection, Input.GetAxis("Mouse ScrollWheel") * step);
        }
        if (Input.GetKey(KeyCode.R))
        {
            RecoverImage();
        }
    
    }

    // Slerp Camera position back to origin position
    private void RecoverImage()
    {
        Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, cameraOriginPos, zoomSpeed * Time.deltaTime);
    }

    // calculate the center point of important area
    private Vector2 CalculateFocusPoint(Vector2 p1, Vector2 p2)
    {
        return new Vector2((p1.x + p2.x)/2, (p1.y + p2.y) / 2);
    }

    // convert the point on image(world) to point on image(screen position)
    private Vector2 PointTransfer(Vector2 centerPoint)
    {
        return Camera.main.WorldToScreenPoint(new Vector3(centerPoint.x, centerPoint.y, 0));
    }
}
