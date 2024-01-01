using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainCamera : MonoBehaviour
{
    public Slider slider;
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0f, 5f, -7f); 
    public float smoothSpeed = 5f; 
    private float rotationSpeed=0.01f ;
    private Vector3 minOffset = new Vector3(0f, 100f, -10f);
    private Vector3 maxOffset = new Vector3(0f, 600f, -10f);
    void LateUpdate()
    {
        if (playerTransform == null || slider == null)
        {
            Debug.LogError("Player Transform or Slider not assigned!");
            return;
        }

     
        float t = slider.value;
        Vector3 offset = Vector3.Lerp(minOffset, maxOffset, t);

        Vector3 targetPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        RotateWithPlayerMovement();

        transform.LookAt(playerTransform.position);
    }

    void RotateWithPlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, playerTransform.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}