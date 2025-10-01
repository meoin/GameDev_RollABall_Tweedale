using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 500f;

    public float speed = 0;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private int pickupCount;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject pauseMenu;
    public GameObject levelManager;
    private Camera mainCamera;

    private int pickupsNeededToWin;

    void Awake() 
    {
        mainCamera = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickupsNeededToWin = levelManager.GetComponent<LevelManager>().roomPickups;

        rb = GetComponent<Rigidbody>();
        pickupCount = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    public void Move(InputAction.CallbackContext movementValue)
    {
        Vector2 movementVector = movementValue.ReadValue<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText() 
    {
        countText.text = $"Count: {pickupCount}";

        if (pickupCount >= pickupsNeededToWin) 
        {
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Win!";
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            pauseMenu.gameObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = Quaternion.Euler(0.0f, mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            pickupCount++;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";

            pauseMenu.gameObject.SetActive(true);
        }

        else if (collision.gameObject.CompareTag("Respawn")) 
        {
            transform.position = new Vector3(0, 0.5f, 0);
            rb.linearVelocity = Vector3.zero; // Stops movement
            rb.angularVelocity = Vector3.zero; // Stops rotation
        }
    }
}
