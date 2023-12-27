using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class TransitionScript : MonoBehaviour
{
    public float maxSpeed = 5f;
    public LayerMask wallLayerMask; // Layer containing the wall

    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Animator animator;
    bool isGrounded = true;
    float moveDirection = 1.0f;
    CustomSceneManager sceneLoader; // Reference to the SceneLoader script


    
    

    void Start()
    {
        // Get Rigidbody
        r2d = GetComponent<Rigidbody2D>();

        // Get capsule collider of player
        mainCollider = GetComponent<CapsuleCollider2D>();

        // Get animator
        animator = GetComponent<Animator>();

                // Find the SceneLoaderManager script
        sceneLoader = FindObjectOfType<CustomSceneManager>();

        // might not dead these
        r2d.freezeRotation = true;
        r2d.gravityScale = 1f; // Disable gravity for horizontal movement
    }

    void Update()
    {

        //moveDirection = 1.0f;
        r2d.velocity = new Vector2(maxSpeed, r2d.velocity.y); // Move automatically to the right
        
        
        // Update the animator

         // tell the animator is this grounded
        animator.SetBool("Grounded", isGrounded);

         // get the downward velocity
        animator.SetFloat("VerticalSpeed", r2d.velocity.y);

        // get the horizontal speed
        animator.SetFloat("Speed", Mathf.Abs(maxSpeed));
    }


        void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided");
        // Check collision on the right side and change direction to left
        if (collision.gameObject.CompareTag("RightWall"))
        {
            // Stop player from moving
            float moveDirection = 0.0f;
            maxSpeed = 0.0f;

            // Load next level
            sceneLoader.LoadScene("Level2");
        }
    }

        private void FixedUpdate()
    {
        // Apply the movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);
        
    }
}

