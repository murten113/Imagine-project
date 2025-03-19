using Unity.VisualScripting;
using UnityEngine;

public class PhotoBook : MonoBehaviour
{
    public GameObject Book;
    bool CanvasIsOn = true;

    void Update()

    {

        if (Input.GetKeyDown(KeyCode.E))
        {

            if (CanvasIsOn == true)
            {
                CanvasIsOn = false;
                Book.SetActive(true);
            }

            else
            {
                CanvasIsOn = true;
                Book.SetActive(false);
            }
        }
    }
}
