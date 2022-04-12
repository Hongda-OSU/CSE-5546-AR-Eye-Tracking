using UnityEngine;

public class AutoZoomByPoint : MonoBehaviour
{
    // Use [a,b] [c,d] value of the important portion in image
    // Calculate the center point
    // Change the center point position relative to screen position
    // Move camera to the point

    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;

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
    
    }

    private void RecoverImage()
    {

    }

    private Vector2 CalculateFocusPoint()
    {
        return Vector2.one;
    }

    private Vector2 PointTransfer(Vector2 centerPoint)
    {
        return Vector2.one;
    }
}
