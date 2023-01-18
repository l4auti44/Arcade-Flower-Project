using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnodeBeetle : Enemy
{

    private Transform mirror;
    private Transform principalSprite;
    public float destroyAfter = 5f;
    public float startAtackAfter = 2f;
    private float _destroyAfter, _startAtackAfter;
    private SpriteRenderer lightning;
    private BoxCollider2D coll;
    private bool atacking = false;
    private Animator childAnimator;
    private bool left = false;

    // Start is called before the first frame update
    void Start()
    {
        //timers
        _destroyAfter = destroyAfter;
        _startAtackAfter = startAtackAfter;

        childAnimator = GetComponentInChildren<Animator>();
        getChild();
        spawnPosition();
        setBoxColl();
        InvokeRepeating("FlipY", 0.5f, 0.5f);
    }


    private void FlipY()
    {
        if (atacking)
        {
            if (lightning.flipY)
            {
                lightning.flipY = false;
            }
            else
            {
                lightning.flipY = true;
            }
        }
    }

    private void getChild()
    {
        Transform[] transforms= GetComponentsInChildren<Transform>();
        foreach (var trans in transforms)
        {
            if (trans.name == "right_bottom")
            {
                mirror = trans;
            }
            if (trans.name == "left_top")
            {
                principalSprite = trans;
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
                left = true;
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

        var distanceSprites = Vector2.Distance(principalSprite.position, mirror.position);
        coll.size = new Vector2(distanceSprites, 1f);
        coll.offset = new Vector2(mirror.localPosition.x / 2f, 0f);


        //lightning
        lightning = getSpriteRenderer("lightning");
        lightning.enabled = false;

        if (left)
        {
            //lightning.transform.localScale = new Vector3(distanceSprites + 5.4f, lightning.transform.localScale.y - 2.75f, lightning.transform.localScale.z);
            lightning.size = new Vector2(2.45f, 0.2f);
        }
        else
        {
            //lightning.transform.localScale = new Vector3(distanceSprites + 2.3f, lightning.transform.localScale.y, lightning.transform.localScale.z);
            lightning.size = new Vector2(1.05f, 0.2f);
        }
        
        lightning.transform.localPosition = new Vector3(principalSprite.localPosition.x, 0f, 0f);

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
            childAnimator.SetBool("atacking", true);
            atacking = true;
            lightning.enabled = true;
            coll.enabled = true;

        }

    }

}