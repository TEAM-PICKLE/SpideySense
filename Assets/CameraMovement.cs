using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the camera movement
    private Vector3 targetPosition = new Vector3(0, 0, 0); // Default target position
    private bool shouldMove = false; // Flag to trigger movement

    void Update()
    {
        // Check if KeyCode.K is pressed
        if (Input.GetKeyDown(KeyCode.K))
        {
            targetPosition = new Vector3(0, 1.7f, -1f); // Set the target position
            shouldMove = true; // Enable movement
        }

        // Smoothly move the camera to the target position
        if (shouldMove)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Stop movement when the camera is close enough to the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                shouldMove = false;
            }
        }
    }
}
