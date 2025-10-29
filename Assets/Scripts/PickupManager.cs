using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PickupManager : MonoBehaviour
{
    public bool isPickedUp = false;
    private bool collected = false;
    private float speed = 5;
    private float acceleration = 15f;
    private GameObject player;
    private Vector3 defaultScale;
    private Vector3 centerPosition;
    private float bounceIntensity = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        defaultScale = transform.localScale;
        centerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickedUp)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

            speed += acceleration * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, player.transform.position);

            float scaleMultiplier = Mathf.Max(Mathf.Min(distance / 2, 1), 0.5f);

            transform.localScale = defaultScale * scaleMultiplier;

            Debug.Log($"Distance: {distance} and scale: {scaleMultiplier}");

            if (distance < 0.3f && !collected)
            {
                collected = true;
                player.GetComponent<PlayerController>().PickupObject();
                Destroy(gameObject);
            }
        }
        else 
        {
            transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);

            transform.position = centerPosition + new Vector3(0, Mathf.Sin(Time.time) * bounceIntensity);
        }
    }
}
