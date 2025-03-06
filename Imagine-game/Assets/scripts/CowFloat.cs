using UnityEngine;

public class CowFloat : MonoBehaviour
{
    public GameObject objectToMove; 
    public Vector3 pointA; 
    public Vector3 pointB; 
    public float speed = 1.0f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        if (objectToMove != null)
        {
            pointA += objectToMove.transform.position;
            pointB += objectToMove.transform.position;
            targetPosition = pointB;
        }
    }

    void Update()
    {
        if (isMoving && objectToMove != null)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(objectToMove.transform.position, targetPosition) < 0.01f)
            {
                targetPosition = targetPosition == pointA ? pointB : pointA;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            isMoving = true;
        }
    }
}
