using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the arrow itself has the tag "ArrowCoin"
            if (gameObject.CompareTag("ArrowCoin"))
            {
                // Disable the arrow (set it inactive)
                gameObject.SetActive(false);
            }
            
            // Update player score
            Debug.Log("Score updated");
            GameManager.S.PlayerScore(10);
        }
    }
}