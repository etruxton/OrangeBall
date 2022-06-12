using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFan : MonoBehaviour
{
    public GameObject bul;
    private int counter = 0;
    private Quaternion rotation;
    private Vector2 position;
    private Rigidbody2D rb;
    private bool shoot;
    public bool d;
    private int health;

    private float acceleration;
    private float deceleration;

    public float horizontal = 3f;
    public float vertical = 7f;
    public float speed = 3.7f;

    float startAngle = 160f;
    float endAngle = 210f;
    float bulletsAmount = 3f;


    [SerializeField] public bool shootBullets = true;
    
    void Update()
    {
        move();
    }
    private void Fire()
    {
        d = false;
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount; i++)
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
    private void move()
    {
        if (d == true)
        {
            transform.position = new Vector2(transform.position.x - horizontal, transform.position.y - vertical);
            decelerate();
        }
        else
        {
            if (counter > 60)
            {
                transform.position = new Vector2(transform.position.x - horizontal, transform.position.y + vertical);
                accelerate();
            }
            counter++;
        }
        if(shoot == true)
        {
            Fire();
            shoot = false;
        }

    }
        
    private void OnEnable()
    {
        Invoke("Destroy", 7f);
        //Invoke("dSwitch", 1.5f);
        rotation = transform.rotation;
        position = transform.position;
        rb = GetComponent<Rigidbody2D>();

        acceleration = 0.1f;
        deceleration = 0.1f;
        horizontal = 0.85f;
        vertical = 1.35f;
        shoot = false;
        d = true; 
        counter = 0;
        health = 3;
        GetComponent<SpriteRenderer>().color = Color.magenta;


    }
    public void takeDamage(int damage)
    {
        health -= damage;
        if (health == 0)
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
        GetComponent<SpriteRenderer>().color = Color.magenta;
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void decelerate()
    {
        if (horizontal > (deceleration * 0.015f))
            horizontal = horizontal - (deceleration * 0.015f);
        else
        {
            shoot = true;
            horizontal = 0;
        }

        if (vertical > ((deceleration+0.1f) * 0.015f))
            vertical = vertical - ((deceleration+0.1f) * 0.015f);
        else
            vertical = 0;
    }

    private void accelerate()
    {
        horizontal = horizontal + ((acceleration+ 0.5f)* 0.015f);
        vertical = vertical + ((acceleration + 0.3f) * 0.015f);
    }
}
