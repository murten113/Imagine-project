using System.Xml.Serialization;
using UnityEngine;

public class TurtelHide : MonoBehaviour
{
    public GameObject turtelActive;
    public Animator turtleAnimator;
    public Material prettyMaterial;
    public Material rockyMaterial;

    private void Start()
    {
        turtelActive.GetComponent<SkinnedMeshRenderer>().material = rockyMaterial;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            turtleAnimator.SetTrigger("isAwake");
            turtelActive.GetComponent<SkinnedMeshRenderer>().material = prettyMaterial;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            turtleAnimator.SetTrigger("isAsleep");
            turtelActive.GetComponent<SkinnedMeshRenderer>().material = rockyMaterial;
        }

    }
}
