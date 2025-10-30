using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public Transform ball;
    private float distanceToPlayer;
    private Vector2 input;

    [SerializeField] private MouseSensitivity mouseSensitivity;
    [SerializeField] private CameraAngle cameraAngle;

    private CameraRotation cameraRotation;

    private void Awake()
    {
        distanceToPlayer = Vector3.Distance(transform.position, target.position);
    }

    private void Update() 
    {
        cameraRotation.Yaw += input.x * mouseSensitivity.horizontal * Time.deltaTime;
        cameraRotation.Pitch -= input.y * mouseSensitivity.vertical * Time.deltaTime;
        cameraRotation.Pitch = Mathf.Clamp(cameraRotation.Pitch, cameraAngle.min, cameraAngle.max);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = player.transform.position + distanceToPlayer;
        transform.eulerAngles = new Vector3(cameraRotation.Pitch, cameraRotation.Yaw, 0.0f);
        transform.position = target.position - transform.forward * distanceToPlayer;
    }

    public void Look(InputAction.CallbackContext context) 
    {
        input = context.ReadValue<Vector2>();
    }
}

[Serializable]
public struct MouseSensitivity 
{
    public float horizontal;
    public float vertical;
}

public struct CameraRotation 
{
    public float Pitch;
    public float Yaw;
}

[Serializable]
public struct CameraAngle 
{
    public float min;
    public float max;
}
