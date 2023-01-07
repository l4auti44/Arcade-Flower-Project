using System;
using Unity.VisualScripting;
using UnityEngine;

public class WateryBlowhog : MonoBehaviour
{

    public float speed = 0.5f;
    public float shootTime = 4f;
    public float damage = 40f;
    private float _shootTime;
    private bool movingRight = true;
    private SpriteRenderer splashRender;
    private BoxCollider2D splashCol;

    // Start is called before the first frame update
    void Start()
    {
        _shootTime = shootTime;
        splashRender = GameObject.Find("splash").GetComponent<SpriteRenderer>();
        splashCol = gameObject.GetComponent<BoxCollider2D>();
        adjustCol();
        enableSplash();

        transform.position = new Vector3(0f, GameManager.topBottom + transform.localScale.y / 2f, 0f);
    }

    private void adjustCol()
    {
        splashCol.offset = GameObject.Find("splash").transform.localPosition;
        splashCol.size = GameObject.Find("splash").transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        _shootTime -= Time.deltaTime;
        movingLeftRight();

        if (_shootTime <= 0f)
        {
            enableSplash();
            _shootTime = shootTime;
        }

    }

    private void movingLeftRight()
    {
        if (transform.position.x >= GameManager.leftRightWall)
        {
            movingRight = false;
        }

        if (transform.position.x <= -GameManager.leftRightWall)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            transform.Translate(new Vector3(GameManager.leftRightWall, 0f, 0f) * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(-GameManager.leftRightWall, 0f, 0f) * speed * Time.deltaTime);
        }
    }

    private void enableSplash() {
        if (splashRender.enabled == true)
        {
            splashCol.enabled = false;
            splashRender.enabled = false;
        }
        else
        {
            splashCol.enabled = true;
            splashRender.enabled = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().decreaseHealth(damage);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

    }


}
