using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // this script allows the camera to be rotated with the mouse

    public bool lockCursor;
    public float mouseSensitivity = 10;
    public Transform target;
    public float distanceFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    // smooth out rotation
    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    // yaw is the x-axis and pitch is the y-axis
    float yaw;
    float pitch;

    // following start method will lock cursor in game
    private void Start()
    {
        if (lockCursor)
        {
            // to get cursor back just hit escape and move mouse out of game screen area
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // use mouse sensitivity to determine speed of camera rotation along x and y axes
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        // clamping the max pitch angles so the camera doesn't rotate through the ground
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        // create a vector3 that rotates using pitch and yaw
        transform.eulerAngles = currentRotation;

        // uses the distanceFromTarget to rotate around the target at the specified distance
        transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
