using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An object pool would be useful here, but I'm out of time
public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    private struct EnemyCategory
    {
        public Enemy enemy;
        public int count;
    }

    [SerializeField] float timeBetweenSpawns = 0.3f;
    [SerializeField] float spawnAreaRadius = 2f;
    /// <summary>
    /// Is the enemy following a path before entering the main road?
    /// </summary>
    [SerializeField] bool enemyIsFollowingPath;
    /// <summary>
    /// Where should the enemy go before starting the race on the main road?
    /// </summary>
    [SerializeField] Transform enemyDestination;
    [SerializeField] List<EnemyCategory> enemies;


    private void Start()
    {
        StartCoroutine(Start_Cor());
    }

    private IEnumerator Start_Cor()
    {
        if (enemyIsFollowingPath)
        {
            foreach (EnemyCategory category in enemies)
            {
                for (int i = 0; i < category.count; i++)
                {
                    Enemy newEnemy = SpawnEnemy(category);
                    newEnemy.SetDestinationArea(enemyDestination.position);
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
            }
        }
        else
        {
            foreach (EnemyCategory category in enemies)
            {
                for (int i = 0; i < category.count; i++)
                {
                    SpawnEnemy(category);
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
            }

        }
    }

    private Enemy SpawnEnemy(EnemyCategory enemyCategory) // What enemy to spawn and how many of them
    {
        Vector3 spawnLocation = transform.position + new Vector3(Random.Range(0, 1f), 0, Random.Range(0, 1f)).normalized * Random.Range(0, spawnAreaRadius);
        Enemy newEnemy = Instantiate(enemyCategory.enemy.gameObject, spawnLocation, transform.rotation, transform).GetComponent<Enemy>();
        return newEnemy;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnAreaRadius);
    }
}
