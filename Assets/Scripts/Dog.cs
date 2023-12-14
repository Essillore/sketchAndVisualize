using UnityEngine;

public class Dog : MonoBehaviour
{
    public float gatherRange = 3f;
    public LayerMask resourceLayer;
    public Transform resourceDetector;
    public float followDistance = 5f;
    public float detectionRange = 10f;
    public Transform player;
    public bool isFollowingPlayer = true;
    public float speedOfDog = 20f;

    private bool isGatheringResource = false;
    private Transform targetResource = null;

    private void Update()
    {
        if (isFollowingPlayer && !isGatheringResource)
        {
            FollowPlayer();
        }

        if (isGatheringResource)
        {
            MoveToResource();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Resource")) // Replace "ResourceLayer" with the actual name of your resource layer
        {
            targetResource = other.transform;
            isFollowingPlayer = false;
            isGatheringResource = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == targetResource)
        {
            targetResource = null;
            isGatheringResource = false;
            isFollowingPlayer = true;
        }
    }

    private void MoveToResource()
    {
        if (targetResource != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetResource.position, Time.deltaTime * speedOfDog);

            if (Vector2.Distance(transform.position, targetResource.position) < 1f) // 1f is a small threshold distance
            {
                GatherResource(targetResource);
            }
        }
    }

    private void GatherResource(Transform resourceTransform)
    {
        Resource resource = resourceTransform.GetComponent<Resource>();
        if (resource != null)
        {
            resource.GetComponent<Resource>().Collected();
             Debug.Log("Dog gathered ham");
            Destroy(resource.gameObject);
        }

        isGatheringResource = false;
        isFollowingPlayer = true;
    }

    private void FollowPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * speedOfDog);
        }
    }

    public void StopFollowingPlayer()
    {
        isFollowingPlayer = false;
    }

    public void ResumeFollowingPlayer()
    {
        isFollowingPlayer = true;
    }
}