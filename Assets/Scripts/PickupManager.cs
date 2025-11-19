using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PickupManager : MonoBehaviour
{
    public bool isPickedUp = false;
    private bool collected = false;
    private float speed = 5;
    private float acceleration = 50f;
    private GameObject player;
    private GameObject ball;
    private Vector3 defaultScale;
    private Vector3 centerPosition;
    private Quaternion initialRotation;
    private float bounceIntensity = 0.2f;

    public GameObject hitbox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("Ball");
        defaultScale = transform.localScale;
        centerPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickedUp && !collected)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, ball.transform.position, step);

            acceleration += (acceleration * 3f * Time.deltaTime);

            speed += acceleration * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, ball.transform.position);

            float scaleMultiplier = Mathf.Max(Mathf.Min(distance / 2, 1), 0.5f);

            transform.localScale = defaultScale * scaleMultiplier;

            //Debug.Log($"Distance: {distance} and scale: {scaleMultiplier}");

            if (distance < 0.3f && !collected)
            {
                collected = true;
                player.GetComponent<PlayerControllerNew>().PickupObject();
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                //ball.GetComponent<BallController>().PickupEffects();

            }
        }
        else if (!collected)
        {
            transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);

            transform.position = centerPosition + new Vector3(0, Mathf.Sin(Time.time) * bounceIntensity);
        }
    }

    public void ResetPosition() 
    {
        if (isPickedUp && !collected) 
        {
            player.GetComponent<PlayerControllerNew>().PickupObject();
            collected = true;
        }

        transform.position = centerPosition;
        transform.localScale = defaultScale;
        transform.rotation = initialRotation;
        acceleration = 50f;
        speed = 5f;
        isPickedUp = false;
        collected = false;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
