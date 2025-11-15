using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PlayerControllerNew : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float rotationSpeed = 1f;

    public GameObject ball;
    public float ballThrowStrength = 10;

    private Camera mainCamera;
    private CameraController cameraController;
    private BallController ballController;

    private int pickupCount;
    public int pickupValue = 1;

    public GameObject aimArrow;
    public Slider chargeSlider;
    private bool throwing;
    private bool charging;
    private float chargePercentage;
    
    public GameObject petPrefab;
    public Transform petFollowPoint;
    public Transform petFollowPointTwo;
    public Transform petFollowPointThree;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI strengthText;

    public InventoryManager inventoryManager;
    public GameObject inventoryCanvas;
    public bool paused;

    public void StartGame() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        paused = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        paused = true;
        Time.timeScale = 0;

        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        ballController = ball.GetComponent<BallController>();
        cameraController = mainCamera.GetComponent<CameraController>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            inventoryManager.AddPetToInventory(new PetDetails("Goobert", 500, 1, "Red"));
            SpawnPet();
        }

        if (Input.GetKeyDown(KeyCode.Y)) 
        {
            inventoryManager.AddPetToInventory(new PetDetails("Yellert", 50, 3, "Yellow"));
            SpawnPet();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            inventoryManager.AddPetToInventory(new PetDetails("Demon", 5000, 3, "Black"));
            SpawnPet();
        }

        if (Input.GetKeyDown(KeyCode.I)) 
        {
            ToggleInventory();
        }


        // get movement direction
        Vector3 movement = Quaternion.Euler(0.0f, mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(movementX, 0.0f, movementY);

        // only move and rotate if holding ball
        if (ballController.holdingBall) 
        {
            // rotate player if movement isnt zero
            if (movement != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // move player based on movement direction & speed
            rb.linearVelocity = movement * speed;
        }

        if (ballController.holdingBall && !paused)
        {
            if (Input.GetMouseButton(0))
            {
                charging = true;
                chargeSlider.gameObject.SetActive(true);
                aimArrow.SetActive(true);

                chargePercentage = Mathf.Sin(Time.time * 3) + 1;
                //Debug.Log($"Charge: {chargePercentage}");

                chargeSlider.value = chargePercentage;
            }

            if (Input.GetMouseButtonUp(0) && charging)
            {
                charging = false;
                throwing = true;
                chargeSlider.gameObject.SetActive(false);
                aimArrow.SetActive(false);
            }
        }

        // throw the ball
        if (throwing)
        {
            float totalThrowStrength = GetTotalStrength();

            Vector3 cameraForward = transform.forward;
            Vector3 throwVector = cameraForward * (totalThrowStrength / 10)* chargePercentage;
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
        pickupCount = Mathf.Min(pickupCount + pickupValue, int.MaxValue);
        //Debug.Log("Coins: " + pickupCount);
        UpdateUI();
    }

    public void SpawnPet() 
    {
        GameObject[] activePets = GameObject.FindGameObjectsWithTag("Pet");

        foreach (GameObject obj in activePets)
        {
            Destroy(obj);
        }

        if (inventoryManager.inventory[0] != null) 
        {
            GameObject newPet = Instantiate(petPrefab, transform.position, transform.rotation);
            newPet.GetComponent<Pet>().details = inventoryManager.inventory[0];
            newPet.GetComponent<Pet>().LoadMaterial();
            newPet.GetComponent<Pet>().followPoint = petFollowPoint;
        }

        if (inventoryManager.inventory[1] != null)
        {
            GameObject newPet = Instantiate(petPrefab, transform.position, transform.rotation);
            newPet.GetComponent<Pet>().details = inventoryManager.inventory[1];
            newPet.GetComponent<Pet>().LoadMaterial();
            newPet.GetComponent<Pet>().followPoint = petFollowPointTwo;
        }

        if (inventoryManager.inventory[2] != null)
        {
            GameObject newPet = Instantiate(petPrefab, transform.position, transform.rotation);
            newPet.GetComponent<Pet>().details = inventoryManager.inventory[2];
            newPet.GetComponent<Pet>().LoadMaterial();
            newPet.GetComponent<Pet>().followPoint = petFollowPointThree;
        }


        UpdateUI();
    }

    public void UpdateUI() 
    {
        if (pickupCount < 1000)
        {
            coinText.text = "$" + pickupCount;
        }
        else if (pickupCount < 1000000)
        {
            float truncatedCoins = pickupCount / 1000;
            coinText.text = "$" + truncatedCoins.ToString("F1") + "k";
        }
        else 
        {
            float truncatedCoins = pickupCount / 1000000;
            coinText.text = "$" + truncatedCoins.ToString("F1") + "m";
        }

        if (GetTotalStrength() < 1000)
        {
            strengthText.text = "Str: " + GetTotalStrength();
        }
        else
        {
            float truncatedStrength = GetTotalStrength() / 1000;
            strengthText.text = "$" + truncatedStrength.ToString("F1") + "k";
        }
    }

    private float GetTotalStrength() 
    {
        float totalThrowStrength = ballThrowStrength;

        if (inventoryManager.inventory[0] != null)
        {
            totalThrowStrength += inventoryManager.inventory[0].Strength;
        }

        if (inventoryManager.inventory[1] != null)
        {
            totalThrowStrength += inventoryManager.inventory[1].Strength;
        }

        if (inventoryManager.inventory[2] != null)
        {
            totalThrowStrength += inventoryManager.inventory[2].Strength;
        }

        return totalThrowStrength;
    }

    public void ToggleInventory()
    {
        if (!inventoryCanvas.activeSelf)
        {
            inventoryCanvas.SetActive(true);
            inventoryManager.PopulateGrid();
            Time.timeScale = 0;
            paused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            inventoryManager.ClearGrid();
            inventoryCanvas.SetActive(false);
            Time.timeScale = 1;
            paused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            SpawnPet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shop"))
        {
            other.gameObject.GetComponent<Shop>().ToggleBuyIcon(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Shop"))
        {
            other.gameObject.GetComponent<Shop>().ToggleBuyIcon(false);
        }
    }

    public int Cash() 
    {
        return pickupCount;
    }

    public void SpendMoney(int charge) 
    {
        pickupCount -= charge;
        UpdateUI();
    }
}
