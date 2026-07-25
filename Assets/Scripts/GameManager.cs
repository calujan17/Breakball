using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public TextMeshProUGUI scoreText;
    public Button restartButton;
    public GameObject ball;
    public GameObject paddle;
    public Animator paddleAnimator;

    private int ballCount;
    private int brickCount;
    private int score;
    private bool isGamePaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        isGameActive = true;
        score = 0;
        GameStart();
    }

    // Update is called once per frame
    void Update(){
        ballCount = FindObjectsByType<BallMovement>(FindObjectsSortMode.None).Length;
        brickCount = FindObjectsByType<Brick>(FindObjectsSortMode.None).Length;
        
        //End Game is Ball Count is 0
        if (ballCount == 0 && isGameActive) {
            GameOver();
        }

        //End Game if Brick Count is 0
        if (brickCount == 0 && isGameActive ) {
            GameOver();
        }
    }

    public void UpdateScore(int scoreToAdd) {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameStart() {
        CreateBall(new Vector3(0f, -3f));
    }

    public void CreateBall(Vector3 startPosition) {
        Instantiate(ball, startPosition, ball.transform.rotation);
    }

    public void GameOver() {
        Time.timeScale = 0f;
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void LongPaddlePowerUp() {

        if (paddleAnimator.GetCurrentAnimatorStateInfo(0).IsName("LongPaddle") && paddleAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) {
            paddleAnimator.Play("LongPaddle", -1, 0f);
        }
        else {
            paddleAnimator.Play("LongPaddle");
        }
    }

    public void PauseGame() {
        if (isGameActive) { 
            if (!isGamePaused) {
                Time.timeScale = 0f;
                isGamePaused = true;
            } else {
                Time.timeScale = 1f;
                isGamePaused = false;
            }
        }
    }
}
