using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnodeBeetle : Enemy
{

    private Transform mirror;
    public float destroyAfter = 5f;
    public float startAtackAfter = 2f;
    private float _destroyAfter, _startAtackAfter;
    private SpriteRenderer lightning;
    private BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        //timers
        _destroyAfter = destroyAfter;
        _startAtackAfter = startAtackAfter;

        getMirror();
        spawnPosition();
        setBoxColl();
    }


    private void getMirror()
    {
        Transform[] transforms= GetComponentsInChildren<Transform>();
        foreach (var trans in transforms)
        {
            if (trans.name == "right_bottom")
            {
                mirror = trans;
            }
        }
    }

    private void spawnPosition()
    {
        var offset = 0.5f;
        var randomX = UnityEngine.Random.Range(-GameManager.leftRightWall + offset, GameManager.leftRightWall - offset);
        var randomY = UnityEngine.Random.Range(-GameManager.topBottom + offset, GameManager.topBottom - offset);

        var randomWall = UnityEngine.Random.Range(0, 2);

        switch (randomWall)
        {
            //left
            case 0:
                transform.position = new Vector3(-GameManager.leftRightWall, randomY, 0f);
                mirror.transform.position = new Vector3(GameManager.leftRightWall, randomY, 0f);

                break;

            //top
            case 1:
                transform.Rotate(new Vector3(0f, 0f, -90f));
                transform.position = new Vector3(randomX, GameManager.topBottom, 0f);
                mirror.transform.position = new Vector3(randomX, -GameManager.topBottom, 0f);

                break; 
            
        }
        
    }

    private void setBoxColl()
    {
        //boxColl
        coll = gameObject.GetComponent<BoxCollider2D>();
        coll.enabled = false;
        

        coll.size = new Vector2(mirror.localPosition.x, 1f);
        coll.offset = new Vector2(mirror.localPosition.x / 2f, 0f);


        //lightning
        lightning = getSpriteRenderer("lightning");
        lightning.enabled = false;

        lightning.transform.localScale = new Vector3(mirror.localPosition.x, lightning.transform.localScale.y, lightning.transform.localScale.z);
        lightning.transform.localPosition = new Vector3(mirror.localPosition.x / 2f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        _destroyAfter -= Time.deltaTime;
        _startAtackAfter-= Time.deltaTime;

        if (_destroyAfter <=0)
        {
            GameObject.Destroy(this.gameObject);
        }

        if (_startAtackAfter <= 0)
        {
            lightning.enabled = true;
            coll.enabled = true;

        }

    }

}
