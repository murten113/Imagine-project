using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendEnd : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        SceneManager.LoadScene("EndScreen");
        }
    }
}
