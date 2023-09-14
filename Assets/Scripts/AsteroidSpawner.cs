using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] asteroidPrefabs;
    [SerializeField] [Range(0f, 50f)]float xSpeed = 5f;
    [SerializeField][Range(0f, 300f)] float ySpeed = 50f;
    [SerializeField] float secondsBetweenAsteroids = 1.5f;

    Camera mainCamera;
    float timer;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnAsteroid();

            timer += secondsBetweenAsteroids;
        }
    }

    void SpawnAsteroid()
    {
        Vector2 spawnpoint = Vector2.zero;
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), -1f);

        spawnpoint.x = Random.value;
        spawnpoint.y = 1f;

        Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnpoint);
        worldSpawnPoint.z = 0f;

        GameObject selectedAsteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
        GameObject asteroidInstance = Instantiate(selectedAsteroid, worldSpawnPoint, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        Rigidbody2D rb = asteroidInstance.GetComponent<Rigidbody2D>();
        rb.velocity = direction * Random.Range(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime);
    }
}
