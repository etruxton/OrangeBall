using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFallEnemySpawner : MonoBehaviour
{
    public GameObject SlowFallEnemy;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
    }
    private void OnEnable()
    {
        rotation = transform.rotation;
        //InvokeRepeating("spawnEnemy", 0f, 1.5f);
        StartCoroutine(start());

    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    // Update is called once per frame
    private void spawnEnemy()
    {
        GameObject e = SlowFallEnemyPool.enemyPoolInstance.GetEnemy();
        //e.transform.position = transform.position;
        e.transform.position = new Vector2(Random.Range(100, 450), -25);
        e.transform.rotation = transform.rotation;
        e.SetActive(true);
        
    }
    IEnumerator start()
    {
        yield return new WaitForSeconds(10);
        InvokeRepeating("spawnEnemy", 0f, 1f);
        yield return new WaitForSeconds(7);
        CancelInvoke();
        yield return new WaitForSeconds(23);
        InvokeRepeating("spawnEnemy", 0f, 1f);
        yield return new WaitForSeconds(7);
        CancelInvoke();
        yield return new WaitForSeconds(23);
        InvokeRepeating("spawnEnemy", 0f, 0.5f);
        yield return new WaitForSeconds(7);
        CancelInvoke();
    }
}
