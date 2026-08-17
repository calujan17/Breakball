using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private enum GameState {
        Active,
        Paused,
        GameOver
    };

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject ball;
    [SerializeField] private PlayerController paddle;
    [SerializeField] private Animator paddleAnimator;

    public int ballCount { get; private set; }
    public int brickCount { get; private set; }
    private int score;
    private GameState currState;
    private LevelManager levelManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        currState = GameState.Active;
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        GameStart();
    }

    // Update is called once per frame
    void Update() {

    }

    private void UpdateHUD() {
        scoreText.text =
            $"Score: {score}  \nBricks: {brickCount} \nBalls: {ballCount} \nGame State: {currState}";
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
        ResetLevelState();
        levelManager.LoadLevel();
        CreateBall(new Vector3(0f, -3f));

    }
    
    public void updateBrickCount(int i) {
        brickCount += i;
    }

    public void CreateBall(Vector3 startPosition) {
        GameObject newBall = Instantiate(
            ball, 
            startPosition, 
            ball.transform.rotation);
        BallMovement ballMovement = newBall.GetComponent<BallMovement>();
        ballMovement.SetGameManager(this);
        ballCount++;
        UpdateHUD();
    }

    public void GameOver() {
        
        currState = GameState.GameOver;
        Time.timeScale = 0f;

        restartButton.gameObject.SetActive(true);
        UpdateHUD();
    }

    public void RestartLevel() {

        if (currState != GameState.GameOver &&
            currState != GameState.Paused) {
            return;
        }

        Time.timeScale = 1f;

        currState = GameState.Active;
        resumeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        ResetLevelState();
        levelManager.RestartLevel();
        CreateBall(new Vector3(0f, -3f));
        UpdateHUD();
    }

    public void ResetLevelState() {
        brickCount = 0;
        ballCount = 0;
        score = 0;
        paddleAnimator.Play("Idle");
        paddle.SetStartPosition();
    }

    public void LongPaddlePowerUp() {

        if (paddleAnimator.GetCurrentAnimatorStateInfo(0).IsName("LongPaddle") && paddleAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) {
            paddleAnimator.Play("LongPaddle", -1, 0f);
        }
        else {
            paddleAnimator.Play("LongPaddle");
        }
    }

    public void TogglePause() {
        if (currState == GameState.Active) {
            PauseGame();
        }
        else if (currState == GameState.Paused) {
            ResumeGame();
        }
    }

    private void PauseGame() {
        currState = GameState.Paused;
        Time.timeScale = 0f;

        resumeButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        UpdateHUD();
    }

    private void ResumeGame() {
        currState = GameState.Active;
        Time.timeScale = 1f;

        resumeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        UpdateHUD();
    }

}
