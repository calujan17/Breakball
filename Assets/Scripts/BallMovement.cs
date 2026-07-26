using UnityEngine;
public class BallMovement : MonoBehaviour
{

    public float ballSpeed = 10.0f;
    public AudioSource audio;
    private Rigidbody2D rb;
    private const float MinimumVerticalVelocity = 3f;
    private const int WallPentalty = 5;

    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){

        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        LaunchBall();
        
    }

    // Update is called once per frame
    void Update(){

        if(rb.linearVelocity.sqrMagnitude != Mathf.Pow(ballSpeed, 2)) {
            Vector2 dir = rb.linearVelocity;
            rb.linearVelocity = dir.normalized * ballSpeed;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Paddle")) {
            float hitPoint = (transform.position.x - collision.transform.position.x);
            Vector2 dir = new Vector2(hitPoint, 1).normalized;
            rb.linearVelocity = dir * ballSpeed;
        } else {
            //Check if Velocity is too horizontal then change
            if (rb.linearVelocityY > -2 && rb.linearVelocity.y <= 0) {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, -MinimumVerticalVelocity).normalized * ballSpeed;
            }
            else if (rb.linearVelocityY < 2 && rb.linearVelocity.y > 0) {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, MinimumVerticalVelocity).normalized * ballSpeed;
            }
         }

        if (collision.gameObject.CompareTag("Wall")) {
            gameManager.UpdateScore(-WallPentalty);
        }

        if (audio.isActiveAndEnabled) {
            audio.Play();
        }
    }

    void LaunchBall() {
        Vector2 dir = new Vector2(Random.Range(-1f, 1f), 1).normalized;
        rb.linearVelocity = dir * ballSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("OutArea")) {
            Destroy(gameObject);
            //gameManager.GameOver();
        }
    }
}
