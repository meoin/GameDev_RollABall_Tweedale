using UnityEngine;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    public Transform ballHoldPoint;
    public Transform ballThrowPoint;
    public bool holdingBall;
    private Rigidbody rb;
    public float frictionPower = 0.1f;
    private bool ballThrow;
    private Vector3 throwVector;
    private float throwBufferTime = 2;
    public float pushStrength = 1f;

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
            transform.position = ballThrowPoint.position;
            rb.AddForce(throwVector, ForceMode.Impulse);
            ballThrow = false;
        }
        else
        {
            if (throwBufferTime > 0) throwBufferTime -= Time.deltaTime;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
            {
                Vector3 pushVector = new Vector3(-1f, 0f, 0f) * pushStrength;
                rb.AddForce(pushVector * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 pushVector = new Vector3(1f, 0f, 0f) * pushStrength;
                rb.AddForce(pushVector * Time.deltaTime);
            }

            //rb.useGravity = true;
            Vector3 velocity = rb.linearVelocity;
            float speed = rb.linearVelocity.magnitude;
            rb.AddForce(velocity * -1 * frictionPower * Time.deltaTime);
            //Debug.Log($"Speed: {speed}");
            if ((speed < 1f && throwBufferTime <= 0) || transform.position.y < -5f || Input.GetKeyDown(KeyCode.R))
            {
                playerController.pickupValue = 1;
                rb.linearVelocity = new Vector3(0f, 0f, 0f);
                transform.position = ballHoldPoint.position;
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
