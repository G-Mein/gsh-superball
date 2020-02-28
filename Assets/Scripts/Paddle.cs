using UnityEngine;

public class Paddle : MonoBehaviour {
    public float speed = 10;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (GameManager.gameRunning) {
            float v = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(v*this.speed, 0);
        } else {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
