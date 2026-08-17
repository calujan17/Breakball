using UnityEngine;
using UnityEngine.InputSystem;

public class Brick : MonoBehaviour
{
    [SerializeField] private int hp = 1;
    [SerializeField] private int brickValue;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LootManager lootManager;

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

}
