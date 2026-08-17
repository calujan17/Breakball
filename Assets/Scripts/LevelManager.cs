using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] brickPreFabs;
    [SerializeField] private Transform topLeft;
    [SerializeField] private float horizontalSpacing = 2f;
    [SerializeField] private float verticalSpacing = 0.6f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private LootManager lootManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        //saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel() {

        int currentLevel = saveManager.CurrentLevel;

        string fileName = $"Levels/Level{saveManager.CurrentLevel:00}";

        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);

        if (jsonFile == null) {
            Debug.LogError($"Could not find level file: {fileName}");
            return;
        }

        LevelData level = JsonUtility.FromJson<LevelData>(jsonFile.text);

        for (int row = 0; row < level.rows.Length; row++) {
            for (int col = 0; col < level.rows[row].Length; col++) {
                int brickType = level.rows[row][col] - '0';

                if (brickType == 0)
                    continue;

                GameObject newBrick = Instantiate(
                    brickPreFabs[brickType - 1],
                    GridToWorld(row, col),
                    Quaternion.identity);

                Brick brick = newBrick.GetComponent<Brick>();
                brick.SetGameManager(gameManager);
                brick.SetLootManager(lootManager);

                gameManager.updateBrickCount(1);
            }
        }
    }

    Vector3 GridToWorld(int row, int column) {
        return topLeft.position +
               new Vector3(
                   column * horizontalSpacing,
                  -row * verticalSpacing,
                   0);
    }

    public void RestartLevel() {
        StartCoroutine(RestartLevelRoutine());
    }

    private IEnumerator RestartLevelRoutine() {
        ClearLevel();

        yield return null;

        LoadLevel();
    }

    public void ClearLevel() {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");

        foreach (GameObject brick in bricks) {
            Destroy(brick);
        }

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls) {
            Destroy(ball);
        }
    }

}
