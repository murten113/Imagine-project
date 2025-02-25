using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomFOV = 30f;  // FOV when zoomed in
    public float normalFOV = 60f; // Default FOV
    public float zoomSpeed = 10f; // Speed of zoom transition

    private Camera cam;
    public GameObject ui;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1)) // Right-click
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomFOV, Time.deltaTime * zoomSpeed);
            ui.SetActive(true);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, Time.deltaTime * zoomSpeed);
            ui.SetActive(false);
        }
    }
}