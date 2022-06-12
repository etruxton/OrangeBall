using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public GameObject bul;
    private Quaternion rotation;
    private Vector2 position;
    private int counter;
    private Rigidbody2D rb;
    private float horizontalMovement;
    private float verticalMovement;

    [SerializeField]public bool shootBullets = true;
    [SerializeField]public float speed = 7;
    [SerializeField]public float timer = 0;
    

    // Update is called once per frame
    void Update()
    {
        move();
        if (transform.position.x < -50 || transform.position.x > 575)
        {
            Destroy();
        }
        else if (transform.position.y > 0 || transform.position.y < -775)
        {
            Destroy();
        }
    }
    private void Fire()
    {
        bul = EnemyBulletPool.bulletPoolInstance.GetBullet();
        bul.transform.position = transform.position;
        bul.transform.rotation = rotation * Quaternion.Euler(0, 0, 270);
        //bul.GetComponent<SpriteRenderer>().color = Color.red;
        bul.SetActive(true);
    }
    private void move()
    {
        //transform.position = new Vector2(position.x - 2 , position.y -speed);
        //position = transform.position;
        transform.Translate(new Vector2(-horizontalMovement, -verticalMovement) * speed * Time.deltaTime);
        counter++;
        if (counter % 5 == 0)
        {
            verticalMovement++;
        }
    }
    private void OnEnable()
    {
        Invoke("Destroy", 2f);
        counter = 0;
        horizontalMovement = 50;
        verticalMovement = 10;
        rotation = transform.rotation;
        position = transform.position;
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("Fire", 0f, Random.Range(0.2f, 0.8f));

    }
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        counter = 0;
        CancelInvoke();
    }
}
