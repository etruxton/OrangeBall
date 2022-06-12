using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet1 : MonoBehaviour
{
    [SerializeField]
    public float speed = 5;
    public Rigidbody2D rb;
    int power = 10;
    bool slowFall = false;
    float acceleration = 500f;
    public float speed2 = 50f; //slowFallSpeed
    Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = transform.right * speed;
    }
    // Update is called once per frame
    void Update()
    {
        move();
        if (transform.position.x < 0 || transform.position.x > 550)
        {
            Destroy();
        }
        else if (transform.position.y > 0 || transform.position.y < -750)
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
    private void move()
    {
        if (slowFall == true)
        {
            speed2 = speed2 + (acceleration * Time.deltaTime);
            transform.Translate(moveDirection * speed2 * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed);
        }
    }
    private void OnEnable()
    {
        speed2 = 50f;
        slowFall = false;
        //Invoke("Destroy", 2f);
    }
    void Destroy()
    {
       gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
        slowFall = true;
    }

}