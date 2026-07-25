using System.Collections;
using UnityEngine;


public class PowerUpMovement : MonoBehaviour
{

    public float speed = 1.0f;
    public enum POWERUPS { BALL3, LONGPADDLE, STICKYPADDLE };
    public POWERUPS powerup;

    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update(){
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //gameObject.SetActive(false);
        if (collision.gameObject.CompareTag("Paddle")) {
            
            switch (powerup) {
                case POWERUPS.BALL3:
                    GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
                    gameManager.CreateBall(balls[0].transform.position);
                    Destroy(gameObject);
                    break;
                case POWERUPS.LONGPADDLE:
                    //StartCoroutine(ChangePaddleSize(2.0f));
                    //StartCoroutine(LongPaddleCountdownRoutine());
                    gameManager.LongPaddlePowerUp();
                    Destroy(gameObject);
                    break;
                case POWERUPS.STICKYPADDLE:

                    break;
                default:
                    Debug.Log("Unknown PowerUp");
                    break;
            }

        }
    }

    private IEnumerator LongPaddleCountdownRoutine() {
        yield return new WaitForSeconds(10);
        StartCoroutine(ChangePaddleSize(0.5f));
    }

    // Smoothly scale to double size over 1 second
    IEnumerator ChangePaddleSize(float sizeChange) {
        Vector3 startScale = gameManager.paddle.transform.localScale;
        Vector3 endScale = startScale * sizeChange;
        float duration = 0.5f;
        float elapsed = 0;
        while (elapsed < duration) {
            gameManager.paddle.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(0.0001f);
        }
        gameManager.paddle.transform.localScale = endScale;
        Destroy(gameObject);
    }


}
