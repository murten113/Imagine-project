using UnityEngine;
using System.Collections; // Required for coroutines

public class TriggerMoveObject : MonoBehaviour
{
    public Transform objectToMove;
    public Vector3 firstMoveDirection = new Vector3(0, 5, 0); // First movement (Up)
    public Vector3 secondMoveDirection = new Vector3(5, 0, 0); // Second movement (Right)
    public float moveSpeed = 2f;
    public float changeDirectionTime = 2f; // Time before changing direction
    private bool shouldMove = false;
    private Vector3 currentMoveDirection;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldMove = true;
            currentMoveDirection = firstMoveDirection;
            StartCoroutine(ChangeDirectionAfterTime());
        }
    }

    void Update()
    {
        if (shouldMove)
        {
            objectToMove.position += currentMoveDirection * moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator ChangeDirectionAfterTime()
    {
        yield return new WaitForSeconds(changeDirectionTime);
        currentMoveDirection = secondMoveDirection; // Change direction
    }
}
