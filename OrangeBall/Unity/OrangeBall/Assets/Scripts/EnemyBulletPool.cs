using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    public static EnemyBulletPool bulletPoolInstance;

    [SerializeField]
    private GameObject pooledBullet;
    private bool notEnoughBulletsInPool = true;

    private List<GameObject> bullets;
    // Start is called before the first frame update
    private void Awake()
    {
        bulletPoolInstance = this;
    }
    void Start()
    {
        bullets = new List<GameObject>();
        //Invoke("starterBullets", 2f);
    }
    public GameObject GetBullet()
    {
        if(bullets.Count > 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if(!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }
        if(notEnoughBulletsInPool)
        {
            GameObject bul = Instantiate(pooledBullet);
            bul.SetActive(false);
            bullets.Add(bul);
            return bul;
        }
        return null;
    }
    public void starterBullets()
    {
         GameObject bul = Instantiate(pooledBullet);
         bul.SetActive(false);
         bullets.Add(bul);
    }
}
