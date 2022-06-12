using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner1 : MonoBehaviour
{
    public GameObject Enemy;
    private Quaternion rotation;


    [SerializeField] public bool spawnLeft = true;
    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation; 
    }
    private void OnEnable()
    {
        rotation = transform.rotation;
        //InvokeRepeating("spawnEnemy", 0f, 0.5f);
        if(spawnLeft == true)
            StartCoroutine(startLeft());
        else
            StartCoroutine(startRight());
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    IEnumerator startLeft()
    {
        InvokeRepeating("spawnEnemy", 0f, 0.5f);
        yield return new WaitForSeconds(7);
        CancelInvoke();
        yield return new WaitForSeconds(23);
        InvokeRepeating("spawnEnemy", 0f, 0.5f); 
        yield return new WaitForSeconds(7);
        CancelInvoke();
        yield return new WaitForSeconds(3);
        InvokeRepeating("spawnEnemy", 0f, 0.5f);
        yield return new WaitForSeconds(7);
        CancelInvoke(); 
        yield return new WaitForSeconds(13);
        InvokeRepeating("spawnEnemy", 0f, 0.5f);
        yield return new WaitForSeconds(7);
        CancelInvoke();
    }
    IEnumerator startRight()
    {
        yield return new WaitForSeconds(20);
        InvokeRepeating("spawnEnemy", 0f, 0.5f);
        yield return new WaitForSeconds(7);
        CancelInvoke(); 
        yield return new WaitForSeconds(3);
        InvokeRepeating("spawnEnemy", 0f, 0.5f); 
        yield return new WaitForSeconds(7);
        CancelInvoke(); 
        yield return new WaitForSeconds(3);
        InvokeRepeating("spawnEnemy", 0f, 0.5f);
        yield return new WaitForSeconds(7);
        CancelInvoke();
        yield return new WaitForSeconds(13);
        InvokeRepeating("spawnEnemy", 0f, 0.5f);
        yield return new WaitForSeconds(7);
        CancelInvoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void spawnEnemy()
    {
        if (spawnLeft == true)
        {
            GameObject e = EnemyPool.enemyPoolInstance.GetEnemy();
            e.transform.position = transform.position;
            e.transform.rotation = transform.rotation;
            //e.bul.GetComponent<SpriteRenderer>().color = Color.red;
            e.SetActive(true);
        }
        else
        {
            GameObject e = EnemyPool.enemyPoolInstance.GetEnemy();
            e.transform.position = transform.position;
            e.transform.rotation = rotation * Quaternion.Euler(0, 180, 0);
            e.SetActive(true);
        }
    }
}
