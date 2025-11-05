using UnityEngine;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    public Transform ballHoldPoint;
    public bool holdingBall;
    private Rigidbody rb;
    public float frictionPower = 0.1f;
    private bool ballThrow;
    private Vector3 throwVector;
    private float throwBufferTime = 2;

    private CameraController camera;

    public GameObject loopDestination;
    public PlayerControllerNew playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingBall)
        {
            //rb.useGravity = false;
            transform.position = ballHoldPoint.position;
            rb.linearVelocity = new Vector3(0f, 0f, 0f);
            //Debug.Log(rb.linearVelocity);
        }
        else if (ballThrow) 
        {
            rb.AddForce(throwVector, ForceMode.Impulse);
            ballThrow = false;
        }
        else
        {
            if (throwBufferTime > 0) throwBufferTime -= Time.deltaTime;

            //rb.useGravity = true;
            Vector3 velocity = rb.linearVelocity;
            float speed = rb.linearVelocity.magnitude;
            rb.AddForce(velocity * -1 * frictionPower * Time.deltaTime);
            //Debug.Log($"Speed: {speed}");
            if ((speed < 1f && throwBufferTime <= 0) || transform.position.y < -5f || Input.GetKeyDown(KeyCode.R))
            {
                playerController.pickupValue = 1;
                rb.linearVelocity = new Vector3(0f, 0f, 0f);
                holdingBall = true;
                camera.target = camera.player;
                ReactivateAllCoins();
            }
        }
    }

    public void ThrowBall(Vector3 passedVector) 
    {
        holdingBall = false;
        ballThrow = true;
        throwBufferTime = 2;
        throwVector = passedVector;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickupHitbox"))
        {
            other.gameObject.GetComponentInParent<PickupManager>().isPickedUp = true;
        }
        else if (other.gameObject.CompareTag("Portal"))
        {
            transform.position = loopDestination.transform.position;
            ReactivateAllCoins();
            playerController.pickupValue = playerController.pickupValue * 2;
        }
    }

    private void ReactivateAllCoins() 
    {
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

        Debug.Log("Pickups: " + pickups.Length);

        // Iterate through the array and activate each GameObject
        foreach (GameObject obj in pickups)
        {
            obj.GetComponent<PickupManager>().ResetPosition();
        }
    }
}
