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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        defaultScale = transform.localScale;
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
    }
}
