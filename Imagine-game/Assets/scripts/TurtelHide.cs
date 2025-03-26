using UnityEngine;

public class TurtelHide : MonoBehaviour
{
    public GameObject turtelActive;
    public GameObject turtelRock;

    private void Start()
    {
        turtelActive.SetActive(false);
        turtelRock.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Activate turt");
            turtelActive.SetActive(true);
            turtelRock.SetActive(false);
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Disable turt");
            turtelActive.SetActive(false);
            turtelRock.SetActive(true);
        }

    }
}
