using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControllerNew : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float rotationSpeed = 1f;

    public GameObject ball;
    public float ballThrowStrength = 5;

    private Camera mainCamera;
    private CameraController cameraController;
    private BallController ballController;
    private bool throwing;
    private bool charging;
    private float chargePercentage;

    private int pickupCount;

    public Slider chargeSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        ballController = ball.GetComponent<BallController>();
        cameraController = mainCamera.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {

        // get movement direction
        Vector3 movement = Quaternion.Euler(0.0f, mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(movementX, 0.0f, movementY);

        // rotate player if movement isnt zero
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // move player based on movement direction & speed
        rb.linearVelocity = movement * speed;

        if (ballController.holdingBall)
        {
            if (Input.GetMouseButton(0))
            {
                charging = true;
                chargeSlider.gameObject.SetActive(true);

                chargePercentage = Mathf.Sin(Time.time * 3) + 1;
                //Debug.Log($"Charge: {chargePercentage}");

                chargeSlider.value = chargePercentage;
            }

            if (Input.GetMouseButtonUp(0) && charging)
            {
                charging = false;
                throwing = true;
                chargeSlider.gameObject.SetActive(false);
            }
        }

        // throw the ball
        if (throwing)
        {
            Vector3 cameraForward = transform.forward;
            Vector3 throwVector = cameraForward * ballThrowStrength * chargePercentage;
            throwVector.y = 5f;
            //cameraForward.Normalize();


            cameraController.target = cameraController.ball;

            chargePercentage = 0;
            throwing = false;
            ballController.ThrowBall(throwVector);
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

    public void PickupObject()
    {
        pickupCount++;
        Debug.Log("Coins: " + pickupCount);
    }
}
