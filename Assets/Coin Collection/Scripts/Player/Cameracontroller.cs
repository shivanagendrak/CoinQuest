using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontroller : MonoBehaviour
{
    public Transform target; // The player's Transform to follow
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Offset from the target

    public float smoothSpeed = 5f; // Smoothing factor for camera movement
    public float sensitivity = 5.0f;


    void LateUpdate()
    {
        if (target == null)
        {
            // Ensure there is a target to follow
            return;
        }

        // Calculate the desired position for the camera
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

       

    }

    public float LimitAngleX = 45;
    public float LimitAngleY = 45;

    private float AngleX;
    private float AngleY;
    public void Update()
    {

        if (Input.GetMouseButton(1))
        {
            var angles = transform.localEulerAngles;

            var xAxis = Input.GetAxis("Mouse X");
            var yAxis = Input.GetAxis("Mouse Y");

            AngleX = Mathf.Clamp(AngleX - yAxis, -LimitAngleX, LimitAngleX);
            AngleY = Mathf.Clamp(AngleY + xAxis, -LimitAngleY, LimitAngleY);

            angles.x = AngleX;
            angles.y = AngleY;

            transform.localRotation = Quaternion.Euler(angles);

            transform.localEulerAngles = angles;
        }
    }
}
