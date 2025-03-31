using UnityEngine;

public class ZoomUnZoom : MonoBehaviour
{
    private Animator mAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mAnimator != null)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("zooming");
                mAnimator.SetTrigger("isZooming");
            }
            if(Input.GetKeyUp(KeyCode.Mouse1))
            {
                mAnimator.SetTrigger("isUnzooming");
            }
        }
    }
}
