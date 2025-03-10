using UnityEngine;

public class whalemovement : MonoBehaviour
{
    public Transform whaleBoi;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float speed = 2.0f;
    public float rotationSpeed = 5.0f;
    public bool isMoving = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
        }
    }

    void Update()
    {
        if (isMoving == true) 
        {
            if (waypoints.Length == 0)
                return;

            MoveToWaypoint();
        }
        
    }

    void MoveToWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - whaleBoi.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        whaleBoi.transform.rotation = Quaternion.Slerp(whaleBoi.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        whaleBoi.transform.position = Vector3.MoveTowards(whaleBoi.transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(whaleBoi.transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
