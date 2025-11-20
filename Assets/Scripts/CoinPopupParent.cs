using UnityEngine;

public class CoinPopupParent : MonoBehaviour
{
    public Transform ball;

    // Update is called once per frame
    void Update()
    {
        transform.position = ball.position;
        //transform.rotation = Camera.main.transform.rotation;
    }
}
