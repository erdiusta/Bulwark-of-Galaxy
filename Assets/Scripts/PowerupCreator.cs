using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCreator : MonoBehaviour
{
    [SerializeField] List<GameObject> powerupPrefabs;
    [SerializeField] float targetTime;

    [HideInInspector] public AudioSource audioSource;

    float timer;
    float spawnTimer;
    [HideInInspector] public float SpawnTimer { get { return spawnTimer; } set { spawnTimer = value; } }
        
    SpriteRenderer rd;

    void Start()
    {
        timer += Time.deltaTime;
        targetTime = 7;
    }

    void Update() 
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0)
        {
            GameObject prefab = CreatePowerup();
            if (prefab)
            {
                spawnTimer += Time.deltaTime;
                Debug.Log(timer);
                if(SpawnTimer > 10f) 
                {
                    Debug.Log(timer);
                    Destroy(prefab);
                }
            }

            targetTime = Random.Range(5, 10);
        }
    }

    GameObject CreatePowerup() 
    {
        GameObject prefab = Instantiate(powerupPrefabs[Random.Range(0, powerupPrefabs.Count)],
                                new Vector2(Random.Range(-2, 3), Random.Range(-3, 3)),
                                Quaternion.identity);
        return prefab;
    }
}

