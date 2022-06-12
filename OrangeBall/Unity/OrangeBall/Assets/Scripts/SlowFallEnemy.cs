using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFallEnemy : MonoBehaviour
{
    public float speed = 3f;
    float deceleration = 1f;
    float startAngle = 0f;
    float endAngle = 360f;
    Rigidbody2D rb;
    int power = 10;
    int bulletsAmount = 10;
    int health;
    // Update is called once per frame
    void Update()
    {
        if(speed > (deceleration * Time.deltaTime))
            speed = speed - (deceleration * Time.deltaTime);
        else
        {
            speed = 0;
            fire();
            Destroy();
        }
        
        move();
        //checkForOutOfBounds();

    }
    void checkForOutOfBounds()
    {
        if (transform.position.x < -50 || transform.position.x > 575)
        {
            Destroy();
        }
        else if (transform.position.y > 0 || transform.position.y < -775)
        {
            Destroy();
        }
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        Player player = hit.GetComponent<Player>();
        if (player != null)
        {
            player.takeDamage(power);
            Destroy();
        }
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        if(health == 0)
        {
            Destroy();
        }
        //Debug.Log("Hit! Current health: " + health);
        else
            StartCoroutine(colorChange());
    }
    IEnumerator colorChange()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        int i = 0;
        while (i < 3)
        {
            i++;
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }
    private void move()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - speed);
    }
    private void fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for(int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = EnemyBulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            //bul.GetComponent<SpriteRenderer>().color = Color.red;
            bul.SetActive(true);
            bul.GetComponent<EnemyBullet1>().SetMoveDirection(bulDir); 

            angle += angleStep;

        }
        
    }
    private void OnEnable()
    {
        speed = 3f;
        deceleration = Random.Range(2.0f, 4.0f);
        health = 3;
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    

}