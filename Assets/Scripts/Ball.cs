using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody2D rb;

    public float speed = 10;

    public Collider2D player;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Paddle b = collision.collider.GetComponent<Paddle>();
        
        if (!b) {
            return;
        }
        
        ResetGame();
    }

    public void ResetGame()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(0, 6);
        FindObjectOfType<GameManager>().stopGame();
    }
}
