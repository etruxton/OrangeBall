using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public float speed = 500;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        Enemy1 enemy = hit.GetComponent<Enemy1>();
        SlowFallEnemy slowFallEnemy = hit.GetComponent<SlowFallEnemy>();
        EnemyFan enemyFan = hit.GetComponent<EnemyFan>();
        if (enemy != null)
        {
            enemy.Destroy();
            Destroy();
        }
        else if (slowFallEnemy != null)
        {
            slowFallEnemy.takeDamage(1);
            Destroy();
        }
        else if (enemyFan != null)
        {
            enemyFan.takeDamage(1);
            Destroy();
        }
    }
    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.up * speed * Time.deltaTime;
        //DestroyBullet();
        if (transform.position.x < -50 || transform.position.x > 575)
        {
            Destroy();
        }
        else if (transform.position.y > 0 || transform.position.y < -775)
        {
            Destroy();
        }

    }
    private void OnEnable()
    {
        rb.velocity = transform.right * speed;
        Invoke("Destroy", 4f);
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

}
