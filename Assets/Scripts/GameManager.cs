using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private enum GameState {
        Active,
        Paused,
        GameOver
    };

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject paddle;
    [SerializeField] private Animator paddleAnimator;

    private int ballCount = 0;
    private int brickCount;
    private int score;
    private GameState currState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        currState = GameState.Active;
        GameStart();
    }

    // Update is called once per frame
    void Update(){

    }

    private void UpdateHUD() {
        scoreText.text =
            $"Score: {score}  \nBricks: {brickCount} \nBalls: {ballCount}";
    }

    public void UpdateScore(int scoreToAdd) {
        score += scoreToAdd;
        UpdateHUD();
    }

    public void OnBrickDestroyed() {
        brickCount--;

        UpdateHUD();

        if (brickCount <= 0) {
            GameOver();
        }
    }

    public void OnBallDestroyed() {
        ballCount--;

        UpdateHUD();

        if (ballCount <= 0) {
            GameOver();
        }
    }

    public void GameStart() {
        brickCount = FindObjectsByType<Brick>(FindObjectsSortMode.None).Length;
        score = 0;
        CreateBall(new Vector3(0f, -3f));
    }

    public void CreateBall(Vector3 startPosition) {
        Instantiate(ball, startPosition, ball.transform.rotation);
        ballCount++;
        UpdateHUD();
    }

    public void GameOver() {
        Time.timeScale = 0f;
        currState = GameState.GameOver;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame() {
        if(currState == GameState.GameOver) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        }
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

        if (currState == GameState.Active) {
            Time.timeScale = 0f;
            currState = GameState.Paused;
        } else if (currState == GameState.Paused) {
            Time.timeScale = 1f;
            currState = GameState.Active;
        }
    }
}
