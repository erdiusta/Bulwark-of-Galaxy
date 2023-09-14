using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping;
    [SerializeField ] TextMeshProUGUI roundClear;
    [SerializeField] Button nextLevelButton;

    WaveConfigSO currentWave;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves() 
    {
        do
        {
            foreach(WaveConfigSO wave in waveConfigs)
            {

                currentWave = wave;
          
                for(int j = 0; j < currentWave.GetEnemyCount(); j++)
                {
                    if (wave == waveConfigs[^1])
                    {
                        roundClear.gameObject.SetActive(true);
                        nextLevelButton.gameObject.SetActive(true);
                        yield return new WaitForSeconds(5f);
                    }
                    Instantiate(currentWave.GetEnemyPrefab(j), 
                                currentWave.GetStartingWaypoint().position, 
                                Quaternion.Euler(0, 0, 180), 
                                transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);            
            }
        }
        while(isLooping);
    }
}
