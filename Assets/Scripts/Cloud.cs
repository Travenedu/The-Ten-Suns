using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of cloud movement
    public bool canMove = true; // Flag to determine if cloud can move
    public bool disappearAfterLanding = false; // Flag to determine if cloud disappears after landing

    Rigidbody2D rb;
    Animator animator;
    bool moveRight = true; // Initially moving right
    bool disappeared = false;
    private List<Collider2D> playersOnCloud = new List<Collider2D>(); // List to store players on the cloud

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove) // Check if cloud can move
        {
            if (moveRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        }

    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check collision with wall
        if (collision.gameObject.CompareTag("RightWall"))
        {
            moveRight = false;
        }
        else if (collision.gameObject.CompareTag("LeftWall"))
        {
            moveRight = true;
        }

        // Check if the cloud should disappear after landing
        if (disappearAfterLanding && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with Cloud");
            StartCoroutine(DisappearCoroutine()); // Start the coroutine for disappearing
            Debug.Log("Disappear Coroutine Started");
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D playerCollider = collision.collider;

            if (!playersOnCloud.Contains(playerCollider))
            {
                playersOnCloud.Add(playerCollider);
            }

            // Move the player along with the cloud
            playerCollider.transform.position += (Vector3)(rb.velocity * Time.deltaTime);

        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D playerCollider = collision.collider;

            if (playersOnCloud.Contains(playerCollider))
            {
                playersOnCloud.Remove(playerCollider);
            }
        }
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@

    IEnumerator ReappearCoroutine()
    {
        Debug.Log("Waiting for 5 second to reappear");
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        gameObject.SetActive(true); // Re-enable the cloud
        disappeared = false;
        // Update the animator
        animator.SetBool("disappeared", disappeared); // Tells animator if cloud has disappeared
        Debug.Log("Set game object to active");
        

    }

    IEnumerator DisappearCoroutine()
    {
        // Give user time to stand on cloud
        yield return new WaitForSeconds(3f);
        
        disappeared = true;

        // Update the animator that cloud has disappeared
        animator.SetBool("disappeared", disappeared);
        Debug.Log("Update the animator that cloud has disappeared");

        
        // Deactivate the cloud
        gameObject.SetActive(false);
        Debug.Log("Set game object to inactive");

        // Wait for cloud to reappear
        yield return new WaitForSeconds(3f);

        // Re-enable the cloud
        gameObject.SetActive(true); 
        Debug.Log("Set game object to active");
        disappeared = false;

        // Update the animator that the cloud has reappeared 
        animator.SetBool("disappeared", disappeared); // Tells animator if cloud has disappeared
        Debug.Log("Update the animator that cloud has appeared");
    }
}
