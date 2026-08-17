using Unity.VisualScripting;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField] private float dropChance = .30f;
    [SerializeField] private GameObject[] lootItems;
    [SerializeField] private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnLootItem(Vector3 lootPosition) {

        int randItem = Random.Range(0, lootItems.Length);

        if(Random.Range(0.0f, 1.0f) <= dropChance) {
            
            GameObject newPowerUp = Instantiate(
                lootItems[randItem], 
                lootPosition, 
                lootItems[randItem].transform.rotation);

            PowerUpMovement powerUp = newPowerUp.GetComponent<PowerUpMovement>();

            powerUp.SetGameManager(gameManager);

        }
    }
}
