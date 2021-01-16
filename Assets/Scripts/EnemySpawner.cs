using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaweConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWawes());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWawes()
    {
        for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {

            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
    private IEnumerator SpawnAllEnemiesInWave(WaweConfig waveConfig)
    {
       for(int x =0; x<waveConfig.GetNumberOfEnemies(); x++)
        {
           var newEnemy =  Instantiate(
               waveConfig.GetEnemyPrefab(),
               waveConfig.GetWayPoints()[0].transform.position,
               Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }

    }
}
