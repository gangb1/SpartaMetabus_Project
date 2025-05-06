using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HiddenEnemyManager : MonoBehaviour
{
    private Coroutine waveRoutine;

    [SerializeField] private List<GameObject> enemyPrefabs;

    [SerializeField] List<Rect> spawnAreas;
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, .3f);
    private List<HiddenEnemyController> activeEnemies = new List<HiddenEnemyController> ();

    private bool enemySpawnComplite;

    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;


    HiddenGameManager gameManager;
    public void Init(HiddenGameManager gameManage)
    {
        this.gameManager = gameManage;
    }

    public void StartWave(int waveCount)
    {
        if(waveCount <= 0)
        {
            gameManager.EndOfWave();
            return;
        }


        if(waveRoutine != null)
        {
            StopCoroutine(waveRoutine);
        }
        waveRoutine = StartCoroutine(SpawnWave(waveCount));

    }
    public void StopWave()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWave(int waveCount)
    {
        enemySpawnComplite = false;
        yield return new WaitForSeconds(timeBetweenWaves);

        for(int i = 0; i < waveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomEnemy();
        }
        enemySpawnComplite= true;
    }

    private void SpawnRandomEnemy()
    {
        if(enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
            return;
        }
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        Vector2 randomPosition = new Vector2(Random.Range(randomArea.xMin, randomArea.xMax),Random.Range(randomArea.yMin, randomArea.yMax));

        GameObject spawnEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        HiddenEnemyController enemyController = spawnEnemy.GetComponent<HiddenEnemyController>();
        enemyController.Init(this, gameManager.player.transform);

        activeEnemies.Add(enemyController);
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach(var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);

            Gizmos.DrawCube(center, size);
        }
    }

    public void RemoveEnemyOnDeath(HiddenEnemyController enemy)
    {
        activeEnemies.Remove(enemy);
        if(enemySpawnComplite && activeEnemies.Count == 0)
        {
            gameManager.EndOfWave();
        }
    }
}
