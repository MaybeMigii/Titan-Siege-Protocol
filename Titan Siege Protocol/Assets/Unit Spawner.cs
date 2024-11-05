using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    //Almost exact same as our titan spawner
    public GameObject recruitPrefab;
    public Vector2 spawnPosition;
    public float spawnInterval = 320f; 
    private float nextSpawnTime;
    void Start()
    {
        nextSpawnTime = Time.time;
    }


    void Update()
    {
        if (Time.time >= nextSpawnTime) {
            SpawnRecruit();
            nextSpawnTime += spawnInterval;
        }
    }
    //Recruits spawn in but have no targeted location like our titans do
    void SpawnRecruit() {
        if (recruitPrefab != null) {
            GameObject newRecruit = Instantiate(recruitPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
