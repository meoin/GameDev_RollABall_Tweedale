using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControllerNew : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    public GameObject ball;
    public float ballThrowStrength = 5;

    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Quaternion.Euler(0.0f, mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(movementX, 0.0f, movementY);
        rb.linearVelocity = movement * speed;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0.5f;
            cameraForward.Normalize();


            mainCamera.GetComponent<CameraController>().target = mainCamera.GetComponent<CameraController>().ball;

            ball.GetComponent<BallController>().holdingBall = false;
            ball.GetComponent<Rigidbody>().AddForce(cameraForward * ballThrowStrength, ForceMode.Impulse);
        }
    }

    public void Move(InputAction.CallbackContext movementValue)
    {
        movementX = 0f;
        movementY = 0f;

        Vector2 movementVector = movementValue.ReadValue<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

        //Debug.Log($"X{movementX} Y{movementY}");
    }
}
