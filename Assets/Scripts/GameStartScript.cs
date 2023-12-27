using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
        CustomSceneManager sceneLoader; // Reference to the SceneLoader script

        void Start()
    {
        // Find the SceneLoaderManager script
        sceneLoader = FindObjectOfType<CustomSceneManager>();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check collision with the sun
        if (collision.gameObject.CompareTag("Player"))
        {
            // Disable the sun (set it inactive)
            collision.gameObject.SetActive(false);
            sceneLoader.LoadScene("Level1");
            

        }
    }

}
