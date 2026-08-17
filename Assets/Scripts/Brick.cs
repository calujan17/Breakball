using UnityEngine;
using UnityEngine.InputSystem;

public class Brick : MonoBehaviour
{
    [SerializeField] private int hp = 1;
    [SerializeField] private int brickValue;
    private GameManager gameManager;
    private LootManager lootManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ball")) {
            hp--;

            if (hp <= 0) {
                gameManager.UpdateScore(brickValue);
                gameManager.OnBrickDestroyed();
                lootManager.SpawnLootItem(gameObject.transform.position);
                Destroy(gameObject);
            }
        }
    }

    public void SetGameManager(GameManager gameManager) {
        this.gameManager = gameManager;
    }

    public void SetLootManager(LootManager lootManager) {
        this.lootManager = lootManager;
    }

}
