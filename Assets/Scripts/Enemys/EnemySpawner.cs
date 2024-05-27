using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyBase[] enemyPrefabs;
    [SerializeField] Transform playerPos;
    [SerializeField] float spawnRate = 1;
    List<ObjectPool<EnemyBase>> pools;
    float spawnTimer = 0;

    GameManager gm = GameManager.Instance;
    private void Awake()
    {
        pools = new List<ObjectPool<EnemyBase>>();
        foreach (EnemyBase prefab in enemyPrefabs)
        {
            ObjectPool<EnemyBase> pool = new(() => Instantiate(prefab),
                ActionOnGet, ActionOnRelease, ActionOnDestroy,
                collectionCheck: true, defaultCapacity: 100, maxSize: 1000);
            pools.Add(pool);
        }
    }

    private void ActionOnGet(EnemyBase obj)
    {
        obj.ResetNewEnemy(transform.position);
        obj.transform.parent = transform;
        obj.gameObject.SetActive(true);
    }

    private void ActionOnRelease(EnemyBase obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(EnemyBase obj)
    {
        Destroy(obj);
    }

    void Update()
    {
        if (!Playergamesystem.Instance.isdead)
        {
            SpawnEnemy();
        }
        //*
        transform.position = new Vector2(playerPos.position.x + 37f, 10f);
        //*
    }

    void SpawnEnemy()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer < spawnRate) return;
        spawnTimer = 0;

        int index = Random.Range(0, pools.Count);
        EnemyBase enemyClone = pools[index].Get();
        enemyClone.refPool ??= pools[index];
    }
}
