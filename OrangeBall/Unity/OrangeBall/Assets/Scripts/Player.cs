using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    Thread receiveThread; 
    UdpClient client; 
    int port;
    int xPy;
    int yPy;
    int health;
    //public GameObject bullet;
    public GameObject bul;
    private Quaternion rotation;
    private int timer;
    [SerializeField] public bool shootBullets = true;
    [SerializeField] public bool pythonControls = true;
    [SerializeField] public bool printLocation = true;
    [SerializeField] public bool displayTimer = true;
    void Start()
    {
        timer = 0;
        health = 500;
        port = 5065;  
        transform.position = new Vector2(0, 0);
        rotation = transform.rotation;
        if(pythonControls == true)
            InitUDP();
        InvokeRepeating("Fire", 0f, 0.1f);
        if(displayTimer == true)
            InvokeRepeating("Timer", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (pythonControls == true)
            transform.position = new Vector2(xPy, -yPy);

    }
    private void Timer()
    {
        timer++;
        Debug.Log("Seconds Elasped: " + timer);
    }
        
    private void OnMouseDrag()
    {
        if (pythonControls != true)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(newPosition.x, newPosition.y, 0);
        }
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Hit! Current health: " + health);
        StartCoroutine(colorChange());
        //GetComponent<SpriteRenderer>().color = Color.red;
    }
    IEnumerator colorChange()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        int i = 0;
        while (i < 10)
        {
            i++;
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        while (i < 20 && i >= 10)
        {
            i++;
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = Color.red;
        while (i < 30 && i >= 20)
        {
            i++;
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void Fire()
    {
        bul = PlayerBulletPool.bulletPoolInstance.GetBullet();
        bul.transform.position = transform.position;
        bul.transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
        //bul.GetComponent<SpriteRenderer>().color = Color.red;
        bul.SetActive(true);
    }
    private void InitUDP()
    {
        
        try
        {
            print("UDP Initialized");

            receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        catch (Exception e)
        {
            print(e.ToString());
            receiveThread.Abort();

            receiveThread.Start();
        }
    }
    // 4. Receive Data
    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port); 
                byte[] data = client.Receive(ref anyIP); 

                string text = Encoding.UTF8.GetString(data); 
                if (printLocation == true)
                    print(">> " + text);
                string[] split = text.Split(',');
                string[] splitLeft = split[0].Split('(');
                string[] splitRight = split[1].Split(')');
                //print(splitLeft[1]);
                //print(splitRight[0]);

                xPy = Int16.Parse(splitLeft[1]);
                yPy = Int16.Parse(splitRight[0]);

            }
            catch (Exception e)
            {
                print(e.ToString());
            }
        }
    }
}
