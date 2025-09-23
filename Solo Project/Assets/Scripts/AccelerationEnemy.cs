using UnityEngine;
using UnityEngine.AI;

public class AccelerationEnemy : MonoBehaviour
{
    public float moveSpeed = 0f; // Initial speed
    public float speedIncreaseRate = 0.01f; // How much speed increases per second
    public float maxSpeed = 10f; // Optional: Maximum speed limit
    public Transform target; // Reference to the player or target
                             // Start is called once before the first execution of Update after the MonoBehaviour is created
                             // If using NavMeshAgent:
    private NavMeshAgent agent;

    void Start()
    {
        {
            agent = GetComponent<NavMeshAgent>();
        }
        {
            // ... (speed increase logic as above) ...

            agent.speed = moveSpeed;
            // Set agent.destination to the target position
        }
    }

    // Update is called once per frame
    void Update()
    {
        {
            // Increase speed over time
            moveSpeed += speedIncreaseRate * Time.deltaTime;

            // Optional: Cap the speed at a maximum value
            moveSpeed = Mathf.Min(moveSpeed, maxSpeed);

            // Apply movement using the updated moveSpeed
            // (e.g., using transform.Translate, Rigidbody.velocity, or NavMeshAgent)
            // Example with transform.Translate:
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
