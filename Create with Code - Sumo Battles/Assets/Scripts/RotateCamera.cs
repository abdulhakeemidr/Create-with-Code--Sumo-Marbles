using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Gets the Horizontal axis input "A & D"
        float horizontalInput = Input.GetAxis("Horizontal") + player.rightJoystick.Horizontal;
        // Rotates the camera's focal point about the Y axis (left-right)
        transform.Rotate(Vector3.up, -horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
