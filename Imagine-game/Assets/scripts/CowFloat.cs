using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CowFloat : MonoBehaviour
{
    public GameObject objectToMove;
    public float launchForce = 10.0f;
    public PhysicsMaterial bouncyMaterial; // Assign in the Inspector
    public float triggerDistance = 20.0f;

    private bool hasLaunched = false;
    private Rigidbody rb;
    private GameObject player;
    public Animator cowAnimator;

    void Start()
    {
        if (objectToMove != null)
        {
            rb = objectToMove.GetComponent<Rigidbody>();
            rb.isKinematic = true; // Disable physics until launch
            player = GameObject.FindGameObjectWithTag("Player"); // Find player by tag
        }
    }

    void Update()
    {
        if (!hasLaunched && player != null)
        {
            float distance = Vector3.Distance(player.transform.position, objectToMove.transform.position);
            if (distance <= triggerDistance)
            {
                LaunchObject();
            }
        }
    }

    void LaunchObject()
    {
        if (rb != null)
        {
            cowAnimator.SetTrigger("isPuffed");
            rb.isKinematic = false;
            rb.useGravity = true;

            // Apply an upward force to simulate a launch
            rb.linearVelocity = Vector3.up * launchForce;

            if (bouncyMaterial != null)
            {
                Collider collider = objectToMove.GetComponent<Collider>();
                if (collider != null)
                {
                    collider.material = bouncyMaterial;
                }
            }

            hasLaunched = true;
        }
    }
}