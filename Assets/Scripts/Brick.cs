using UnityEngine;
using UnityEngine.InputSystem;

public class Brick : MonoBehaviour
{
    public int hp = 1;
    public int brickValue;
    private GameManager gameManager;
    private LootManager lootManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lootManager = GameObject.Find("LootManager").GetComponent<LootManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ball")) {
            hp--;

            if (hp <= 0) {
                gameManager.UpdateScore(brickValue);
                gameManager.onBrickDestroyed();
                lootManager.SpawnLootItem(gameObject.transform.position);
                Destroy(gameObject);
            }
        }
    }

}
