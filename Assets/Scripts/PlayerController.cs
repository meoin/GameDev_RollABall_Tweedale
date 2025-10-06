using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Android;
//using System.Diagnostics;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 500f;

    public float speed = 0;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private int pickupCount;
    public GameObject levelManager;
    private GameObject winText;
    private GameObject countText;
    private Camera mainCamera;

    private int pickupsNeededToWin;

    public PlayerController Instance;

    void Awake() 
    {
        Instance = this;
        mainCamera = Camera.main;
        levelManager = FindFirstObjectByType<LevelManager>().gameObject;
        winText = levelManager.gameObject.GetComponent<LevelManager>().winText;
        countText = levelManager.gameObject.GetComponent<LevelManager>().countText;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickupsNeededToWin = levelManager.GetComponent<LevelManager>().roomPickups;

        rb = GetComponent<Rigidbody>();
        pickupCount = 0;

        if (GetGameState() == GameState.Roll)
        {
            rb.useGravity = true;
        }
        else 
        {
            rb.useGravity = false;
        }

        if (GetGameState() == GameState.Roll) 
        {
            SetCountText();
            levelManager.gameObject.GetComponent<LevelManager>().winText.SetActive(false);
        }
    }

    private GameState GetGameState() 
    {
        return levelManager.gameObject.GetComponent<LevelManager>().currentState;
    }

    public void Move(InputAction.CallbackContext movementValue)
    {
        if (GetGameState() == GameState.Roll) 
        {
            Vector2 movementVector = movementValue.ReadValue<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    }

    void SetCountText() 
    {
        countText.gameObject.SetActive(true);

        countText.GetComponent<TextMeshProUGUI>().text = $"Count: {pickupCount}";

        if (pickupCount >= pickupsNeededToWin) 
        {
            winText.SetActive(true);
            winText.GetComponent<TextMeshProUGUI>().text = "You Win!";
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            levelManager.GetComponent<LevelManager>().PauseScene();
        }
    }

    private void FixedUpdate()
    {
        GameState state = GetGameState();

        Debug.Log("Game state is " + state);

        if (state == GameState.Menu || state == GameState.Shop)
        {
            transform.Rotate(new Vector3(30, 90, 30) * Time.deltaTime);
        }
        else if (state == GameState.Roll) 
        {
            Vector3 movement = Quaternion.Euler(0.0f, mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(movementX, 0.0f, movementY);
            rb.AddForce(movement * speed);
        }
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
            winText.gameObject.SetActive(true);
            winText.GetComponent<TextMeshProUGUI>().text = "You Lose!";

            levelManager.GetComponent<LevelManager>().PauseScene();
        }

        else if (collision.gameObject.CompareTag("Respawn")) 
        {
            transform.position = new Vector3(0, 0.5f, 0);
            rb.linearVelocity = Vector3.zero; // Stops movement
            rb.angularVelocity = Vector3.zero; // Stops rotation
        }
    }
}
