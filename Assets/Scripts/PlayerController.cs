using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15.0f;
    private float limitX;
    private InputAction moveAction;
    private InputAction pauseAction;
    private float playArea;
    private Animator animator;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();

        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.Enable();

        pauseAction = InputSystem.actions.FindAction("Pause");
        //pauseAction.Enable();

        //Calculate the Limits for the play area based on the initial paddle size
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject rightBorder = null;
        GameObject leftBorder = null;
        for (int i = 0; i < walls.Length; i++) {
            if (walls[i].gameObject.name == "RightBorder") {
                rightBorder = walls[i].gameObject;
            } else if (walls[i].gameObject.name == "LeftBorder") {
                leftBorder = walls[i].gameObject;
            }
        }

        playArea = Mathf.Abs(rightBorder.transform.position.x) + Mathf.Abs(leftBorder.transform.position.x);
        limitX = (playArea - GetComponent<Renderer>().bounds.size.x) / 2.0f;

    }

    // Update is called once per frame
    void Update(){
        float horizontalInput = moveAction.ReadValue<Vector2>().x;

        Vector3 paddlePos = transform.position;

        if (horizontalInput > 0) {
            Debug.Log(horizontalInput);
        }

        paddlePos.x += horizontalInput * moveSpeed * Time.deltaTime;

        //Keep Paddle inside background
        limitX = (playArea - GetComponent<Renderer>().bounds.size.x) / 2.0f;
        paddlePos.x = Mathf.Clamp(paddlePos.x,-limitX, limitX);
        transform.position = paddlePos;

        if (pauseAction.WasPressedThisFrame()) {
            gameManager.PauseGame();
        }

    }

    public void LongPaddle() {
        animator.Play("LongPaddle");
    }
}
