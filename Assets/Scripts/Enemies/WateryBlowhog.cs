using System;
using Unity.VisualScripting;
using UnityEngine;

public class WateryBlowhog : MonoBehaviour
{

    public float shootTime = 2f;
    public float lifeTime = 10f;
    public float damage = 40f;
    private float _shootTime;
    private SpriteRenderer splashRender;
    private BoxCollider2D splashCol;
    private bool shooting = false;

    void Start()
    {
        _shootTime = shootTime;
        splashRender = GameObject.Find("splash").GetComponent<SpriteRenderer>();
        splashCol = gameObject.GetComponent<BoxCollider2D>();
        enableSplash();
        InvokeRepeating("FlipX", 0.5f, 0.5f);

        
        spawnPosition();
    }

    private void spawnPosition()
    {
        var randomX = UnityEngine.Random.Range(-GameManager.leftRightWall + 0.5f, GameManager.leftRightWall - 0.5f);
        var randomY = UnityEngine.Random.Range(-GameManager.topBottom + 0.5f, GameManager.topBottom - 0.5f);
        //transform.position = new Vector3(randomX, GameManager.topBottom + 1.5f, 0f);
        var randomWall = UnityEngine.Random.Range(0, 4);

        //top
        if (randomWall == 0)
        {
            //transform.Rotate(new Vector3(0, 0, 270f));
            transform.position = new Vector3(randomX, GameManager.topBottom + 1.5f, 0f);

        }
        //bottom
        else if (randomWall == 1)
        {
            transform.Rotate(new Vector3(0, 0, 180f));
            transform.position = new Vector3(randomX, -GameManager.topBottom - 1.5f, 0f);
        }
        //left
        else if (randomWall == 2)
        {
            transform.Rotate(new Vector3(0, 0, 90f));
            transform.position = new Vector3(-GameManager.leftRightWall - 1.5f, randomY, 0f);
        }
        //right
        else
        {
            transform.Rotate(new Vector3(0, 0, 270f));
            transform.position = new Vector3(GameManager.leftRightWall + 1.5f, randomY, 0f);
        }


    }

    void Update()
    {
        _shootTime -= Time.deltaTime;
        lifeTime -= Time.deltaTime;
        //movingLeftRight();

        if (_shootTime <= 0f)
        {
            enableSplash();
            _shootTime = shootTime;
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
            collision.gameObject.GetComponent<Health>().decreaseHealth(damage);

        }
    }

}
