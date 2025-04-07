using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomFOV = 30f;
    public float normalFOV = 60f;
    public float zoomSpeed = 10f;

    private Camera cam;
    public GameObject ui;
    private Animator mAnimator;

    private int zoomFrameCounter = 0;
    private bool uiActivated = false;
    private const int delayFrames = 50;

    void Start()
    {
        cam = GetComponent<Camera>();
        mAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (mAnimator != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                mAnimator.SetTrigger("isZooming");
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                mAnimator.SetTrigger("isUnzooming");
            }
        }

        if (Input.GetMouseButton(1)) // Right-click held
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomFOV, Time.deltaTime * zoomSpeed);

            if (zoomFrameCounter < delayFrames)
            {
                zoomFrameCounter++;
            }

            if (zoomFrameCounter >= delayFrames && !uiActivated)
            {
                ui.SetActive(true);
                uiActivated = true;
            }
        }
        else // Right-click released
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, Time.deltaTime * zoomSpeed);
            zoomFrameCounter = 0;
            uiActivated = false;
            ui.SetActive(false);
        }
    }
}
