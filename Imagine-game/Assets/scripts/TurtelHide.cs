using UnityEngine;

public class TurtelHide : MonoBehaviour
{
    public GameObject turtelActive;
    public GameObject turtelRock;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            turtelActive.SetActive(false);
            turtelRock.SetActive(true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            turtelActive.SetActive(true);
            turtelRock.SetActive(false);
        }

    }
}
