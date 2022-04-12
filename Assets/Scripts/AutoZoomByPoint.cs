using UnityEngine;

public class AutoZoomByPoint : MonoBehaviour
{
    // Use [a,b] [c,d] value of the important portion in image
    // Calculate the center point
    // Camera zoom in to the center point (FOV down)

    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;

    void Update()
    {
        Zoom();
    }

    void Zoom()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit point;
        Physics.Raycast(ray, out point, 1000);
        Vector3 Scrolldirection = ray.GetPoint(5);

        float step = zoomSpeed * Time.deltaTime;

        // Allows zooming in and out via the mouse wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Scrolldirection.y > minZoom)
        {
            transform.position = Vector3.MoveTowards(transform.position, Scrolldirection, Input.GetAxis("Mouse ScrollWheel") * step);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Scrolldirection.y < maxZoom)
        {
            transform.position = Vector3.MoveTowards(transform.position, Scrolldirection, Input.GetAxis("Mouse ScrollWheel") * step);
        }
    
    }
}
