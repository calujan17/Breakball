using System.Collections;
using UnityEngine;


public class PowerUpMovement : MonoBehaviour
{

    
    private enum POWERUPS { BALL3, LONGPADDLE, STICKYPADDLE };
    [SerializeField] private POWERUPS powerup;
    [SerializeField] private float speed = 7.5f;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){

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

    public void SetGameManager(GameManager gameManager) {
        this.gameManager = gameManager;
    }
}
