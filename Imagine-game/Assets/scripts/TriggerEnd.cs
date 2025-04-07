using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    public GameObject EndTrig;
    


    private void Start()
    {
        EndTrig.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndTrig.SetActive(true);
        }
    }


}
