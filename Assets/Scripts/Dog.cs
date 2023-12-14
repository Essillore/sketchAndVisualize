using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

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


    [Header("Animation")]
    public GameObject playerSpriteObject;
    public bool animationsON = true;
    public bool facingLeft = true;
    public bool isFlying = false;
    public float horizontal;
    public float vertical;
    private bool moving = false;
    private bool movingHor = false;
    private bool movingVert = false;
    public Rigidbody2D dogRB;
    public GameObject dogSpriteObject;
    public SpriteRenderer dogSprite;
    public Animator dogAnimator;
    private Vector3 previousPosition;
    private Vector3 currentVelocity;

    private void Start()
    {
        dogRB = gameObject.GetComponent<Rigidbody2D>();
    }

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

        if (movingHor || movingVert)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        //swap facing direction
        if (horizontal < -0.1f && facingLeft)
        {
            Flip();
        }
        if (horizontal > 0.1f && !facingLeft)
        {
            Flip();
        }

        horizontal = currentVelocity.x;
        movingHor = Mathf.Abs(horizontal) > 0.1f;
        movingVert = Mathf.Abs(vertical) > 0.1f;

        // Calculate time elapsed since last frame
        float timeDelta = Time.deltaTime;

        // Calculate current velocity
        currentVelocity = (transform.position - previousPosition) / timeDelta;

        // Update previous position for the next frame
        previousPosition = transform.position;

        // Optionally, you can print the velocity to the console
        Debug.Log("Current Velocity: " + currentVelocity);

        if (moving == true)
        {
            dogAnimator.SetBool("isFlying", true);
        }
        if (!moving)
        {
            dogAnimator.SetBool("isFlying", false);
        }
    }

    public void Flip()
    {

        Vector3 currentScale = dogSpriteObject.transform.localScale;

        if (facingLeft)
        {
            currentScale.x = 1;
            dogSpriteObject.transform.localScale = currentScale;
            facingLeft = false;
        }
        else if (!facingLeft)
        {
            currentScale.x = -1;
            dogSpriteObject.transform.localScale = currentScale;
            facingLeft = true;
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