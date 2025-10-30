using UnityEngine;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    public Transform ballHoldPoint;
    public bool holdingBall;
    private Rigidbody rb;
    public float frictionPower = 0.1f;

    private CameraController camera;

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
            transform.position = ballHoldPoint.position;
        }
        else 
        {
            Vector3 velocity = rb.linearVelocity;
            float speed = rb.linearVelocity.magnitude;
            rb.AddForce(velocity.normalized * -1 * frictionPower);
            Debug.Log($"Speed: {speed}");
            if (speed < 1f) 
            {
                holdingBall = true;
                camera.target = camera.player;
            }
        }
    }
}
