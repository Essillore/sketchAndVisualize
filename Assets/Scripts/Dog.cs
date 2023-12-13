using UnityEngine;

public class Dog : MonoBehaviour
{
    public float gatherRange = 3f;
    public LayerMask resourceLayer;
    public Transform resourceDetector; // Object used to detect resources in front of the dog
    public float followDistance = 5f; // Distance within which the dog follows the player
    public float detectionRange = 10f; // Range within which the dog detects resources
    public Transform player; // Reference to the player
    public bool isFollowingPlayer = true; // Flag to control following behavior
    public float speedOfDog = 30f;

    private void Update()
    {
        if (isFollowingPlayer)
        {
            FollowPlayer();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GatherResources();
        }
    }

    private void GatherResources()
    {
        RaycastHit2D hit = Physics2D.Raycast(resourceDetector.position, transform.right, gatherRange, resourceLayer);

        if (hit.collider != null)
        {
            Resource resource = hit.collider.GetComponent<Resource>();
            if (resource != null)
            {
            //    Debug.Log("Dog gathered " + resource.amount + " " + resource.resourceName);
                Destroy(resource.gameObject); // Destroy the collected resource
                // Handle the collected resource as needed
            }
        }
    }
private void FollowPlayer()
{
    float distanceToPlayer = Vector2.Distance(transform.position, player.position);
    if (distanceToPlayer > followDistance)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * speedOfDog);
    }
}


// Method to command the dog to stop following the player
public void StopFollowingPlayer()
{
    isFollowingPlayer = false;
}

// Method to command the dog to resume following the player
public void ResumeFollowingPlayer()
{
    isFollowingPlayer = true;
}
}