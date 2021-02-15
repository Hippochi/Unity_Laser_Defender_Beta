using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    //can make start a coroutine
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }
    
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBtwnSpawns());
        }

    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveInd = 0; waveInd < waveConfigs.Count; waveInd++)
        {
            var currentWave = waveConfigs[waveInd];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
}
