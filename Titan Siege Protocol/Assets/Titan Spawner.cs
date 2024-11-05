using UnityEngine;

public class TitanSpawner : MonoBehaviour
{
    public GameObject titanPrefab; // Prefab to spawn
    public Vector2 spawnPosition; // Position to spawn titans
    public Vector2 targetPosition; // Where the titan should move
    public float spawnInterval = 3f; // Interval in seconds between spawns
    private float nextSpawnTime; // Time to the next spawn

    void Start() {   
        // There will be a delay at the beginning when game starts
        // and for when titans will spawn based on our spawn interval
        nextSpawnTime = Time.time + spawnInterval; 
    }

    void Update() {
        //Checks if its time to spawn a titan and if it is, will spawns one
        if (Time.time >= nextSpawnTime) {
            SpawnTitan();
            nextSpawnTime += spawnInterval;
        }
    }

    void SpawnTitan() {
        //if a titanPrefab exists it will spawn a titan at the spawnPosition
        //this titan then will move towards the targetPosition given
        if (titanPrefab != null) {
            GameObject newTitan = Instantiate(titanPrefab, spawnPosition, Quaternion.identity);
            newTitan.GetComponent<TitanMovement>().targetPosition = targetPosition; // Assign the target position
        }
    }
}