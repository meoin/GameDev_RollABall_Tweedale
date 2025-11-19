using TMPro;
using UnityEngine;

public class CoinTextPopup : MonoBehaviour
{
    public double value = 1;
    public int speed = 40;

    private int arcRange = 50;
    private float xOffset = 0;
    private float yOffset = 0.5f;
    private float zOffset = 0;
    private float timer = 1;
    private float xSpeed;
    private float ySpeed;

    public Transform ball;
    private TextMeshPro textComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(double val, Transform ballTransform)
    {
        ball = ballTransform;
        value = val;
        textComponent = GetComponent<TextMeshPro>();
        textComponent.text = TruncateNumber(value);

        float arc = ((Random.value * arcRange) - (arcRange/2)) + 90;
        float radianAngle = arc * Mathf.Deg2Rad;

        xSpeed = speed * Mathf.Cos(radianAngle);
        ySpeed = speed * Mathf.Sin(radianAngle);

        xSpeed *= (1 + Random.value);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);

        timer -= Time.deltaTime;

        xOffset += xSpeed * Time.deltaTime;
        yOffset += ySpeed * Time.deltaTime;
        zOffset = 1 - timer;

        ySpeed -= Time.deltaTime * 8;

        transform.position = ball.position + new Vector3(xOffset, yOffset, zOffset);

        textComponent.alpha = timer;

        if (timer <= 0) 
        {
            Destroy(gameObject);
        }
    }

    private string TruncateNumber(double val) 
    {
        if (val < 1000)
        {
            return $"{val}";
        }
        else if (val < 1000000.0)
        {
            double truncatedCoins = val / 1000;
            return truncatedCoins.ToString("F1") + "k";
        }
        else if (val < 1000000000.0)
        {
            double truncatedCoins = val / 1000000;
            return truncatedCoins.ToString("F1") + "m";
        }
        else if (val < 1000000000000.0)
        {
            double truncatedCoins = val / 1000000000.0;
            return truncatedCoins.ToString("F1") + "b";
        }
        else
        {
            double truncatedCoins = val / 1000000000000.0;
            return truncatedCoins.ToString("F1") + "t";
        }
    }
}
