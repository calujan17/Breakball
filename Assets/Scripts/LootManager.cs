using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField] private float dropChance = .30f;

    public GameObject[] lootItems;
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
            Instantiate(lootItems[randItem], lootPosition, lootItems[randItem].transform.rotation);
        }
    }
}
