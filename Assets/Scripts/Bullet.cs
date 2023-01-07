using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public float speed = 3f;
    public float damage = 20f;
    
    private GameObject player;

    private bool look = false;
    private float targetTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player");
        //Vector3 dir = player.transform.position - transform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        setDirection();

    }

    private void setDirection()
    {
        var x = transform.position.x; 
        var y = transform.position.y;
        var wallx = GameManager.leftRightWall - 0.1f;
        var wally = GameManager.topBottom - 0.1f;
        if (x == wallx)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 180f, transform.rotation.w);

        }
        else if (x == -wallx)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0f, transform.rotation.w);
        }
        else if (y == wally) 
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 270f, transform.rotation.w);
        }
        else
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 90f, transform.rotation.w);
        }


    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
        //lookAt 2d (rotation)
        //if (targetTime >= 0.0f)
        //{
        //Vector3 dir = player.transform.position - transform.position;
        // float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //}


        //movement
        //transform.Translate(new Vector3(1,1,0) * Time.deltaTime * speed);

        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);



        //Destroy out borders
        if (gameObject.transform.position.y > GameManager.topBottom || gameObject.transform.position.y < -GameManager.topBottom  || gameObject.transform.position.x > GameManager.leftRightWall || gameObject.transform.position.x < -GameManager.leftRightWall)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health>().decreaseHealth(damage);
        GameObject.Destroy(this.gameObject);
    }
}
