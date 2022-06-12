using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFanSpawner : MonoBehaviour
{
    public GameObject Enemy;
    private Quaternion rotation;
    private Vector2 position;

    //[SerializeField] public bool spawnLeft = true;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }
    private void OnEnable()
    {
        position = transform.position;
        rotation = transform.rotation;
        //InvokeRepeating("spawnEnemy", 0f, 1f);
       
        StartCoroutine(startSpawn());

    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    IEnumerator startSpawn()
    {
        yield return new WaitForSeconds(50);
        InvokeRepeating("spawnEnemy", 0f, 1f);
        yield return new WaitForSeconds(4);
        CancelInvoke();
        yield return new WaitForSeconds(6);
        InvokeRepeating("spawnEnemy", 0f, 1f);
        yield return new WaitForSeconds(4);
        CancelInvoke();
        yield return new WaitForSeconds(6);
        InvokeRepeating("spawnEnemy", 0f, 1f);
        yield return new WaitForSeconds(4);
        CancelInvoke();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void spawnEnemy()
    {
        float num = Random.Range(350f, 700f);
        GameObject e = EnemyFanPool.enemyPoolInstance.GetEnemy();
        e.transform.position = new Vector2(num, position.y);
        e.transform.rotation = transform.rotation;
        //e.bul.GetComponent<SpriteRenderer>().color = Color.red;
        e.SetActive(true);

        GameObject e2 = EnemyFanPool.enemyPoolInstance.GetEnemy();
        e2.transform.position = new Vector2 (num, position.y + 50);
        e2.transform.rotation = transform.rotation;
        //e2.bul.GetComponent<SpriteRenderer>().color = Color.red;
        e2.SetActive(true);
    }
}