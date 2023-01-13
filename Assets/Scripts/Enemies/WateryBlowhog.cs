using System;
using Unity.VisualScripting;
using UnityEngine;

public class WateryBlowhog : MonoBehaviour
{

    public float startAtackAfter = 2f;
    public float lifeTime = 10f;
    public float damage = 40f;
    private float _startAtackAfter;
    private SpriteRenderer splashRender;
    private BoxCollider2D splashCol;
    private bool shooting = false;

    void Start()
    {
        _startAtackAfter = startAtackAfter;
        
        enableSplash();
        InvokeRepeating("FlipX", 0.5f, 0.5f);
        spawnPosition();
    }

    private void spawnPosition()
    {
        var offset = 0.5f;
        var randomX = UnityEngine.Random.Range(-GameManager.leftRightWall + offset, GameManager.leftRightWall - offset);
        var randomY = UnityEngine.Random.Range(-GameManager.topBottom + offset, GameManager.topBottom - offset);
        

        var randomWall = UnityEngine.Random.Range(0, 4);
        switch (randomWall)
        {
            //top
            case 0:
                transform.position = new Vector3(randomX, GameManager.topBottom, 0f);

                break;
            //bottom
            case 1:
                transform.Rotate(new Vector3(0, 0, 180f));
                transform.position = new Vector3(randomX, -GameManager.topBottom, 0f);
                break; 
            //left
            case 2:
                transform.Rotate(new Vector3(0, 0, 90f));
                transform.position = new Vector3(-GameManager.leftRightWall, randomY, 0f);
                break; 
            //right
            case 3:
                transform.Rotate(new Vector3(0, 0, 270f));
                transform.position = new Vector3(GameManager.leftRightWall, randomY, 0f);
                break;
            
        }

       

    }

    void Update()
    {
        _startAtackAfter -= Time.deltaTime;
        lifeTime -= Time.deltaTime;
        //movingLeftRight();

        if (_startAtackAfter <= 0f)
        {
            enableSplash();
            _startAtackAfter = startAtackAfter;
        }
        if (lifeTime <= 0f)
        {
            GameObject.Destroy(gameObject);
        }

    }

    private void FlipX()
    {
        if (shooting)
        {
            if (splashRender.flipX)
            {
                splashRender.flipX = false;
            }
            else
            {
                splashRender.flipX = true;
            }
        }
    }


    private void enableSplash() {
        splashRender = GameObject.Find("splash").GetComponent<SpriteRenderer>();
        splashCol = gameObject.GetComponent<BoxCollider2D>();
        
        if (splashRender.enabled == true)
        {
            splashCol.enabled = false;
            splashRender.enabled = false;
            shooting = false;
        }
        else
        {
            splashCol.enabled = true;
            splashRender.enabled = true;
            shooting = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playerManager>().takingDamage();
            collision.gameObject.GetComponent<Health>().decreaseHealth(damage);

        }
    }

}
