using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFanPool : MonoBehaviour
{
    public static EnemyFanPool enemyPoolInstance;

    [SerializeField]
    private GameObject pooledEnemy;
    private bool notEnoughEnemiesInPool = true;

    private List<GameObject> enemy;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyPoolInstance = this;
    }
    void Start()
    {
        enemy = new List<GameObject>();
    }
    public GameObject GetEnemy()
    {
        if (enemy.Count > 0)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                if (!enemy[i].activeInHierarchy)
                {
                    return enemy[i];
                }
            }
        }
        if (notEnoughEnemiesInPool)
        {
            GameObject e = Instantiate(pooledEnemy);
            e.SetActive(false);
            enemy.Add(e);
            return e;
        }
        return null;
    }
}
