using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public GameObject player;

    public float smoothTime;

    public float maxSafety;

    private Vector2 refVelocity;
    private bool isCentered = false;

    private void Start()
    {
        refVelocity = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cameraPosition = transform.position;
        Vector2 playerPosition = player.transform.position;

        float distanceToCamera = (cameraPosition - playerPosition).sqrMagnitude;

        if(distanceToCamera > maxSafety)
        {
            isCentered = false;
        }

        if(!isCentered)
        {
            // Get the smoothed positioned
            Vector2 newPosition = Vector2.SmoothDamp(transform.position, player.transform.position, ref refVelocity, smoothTime);

            // Update the camera position
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

            if(distanceToCamera < 0.1f) { isCentered = true; }
        }
    }
}
