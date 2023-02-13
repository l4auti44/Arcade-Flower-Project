using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnodeBeetle : Enemy
{

    private Transform mirror;
    private Transform principalSprite;
    public float destroyAfter = 5f;
    public float startAtackAfter = 2f;
    private float _destroyAfter, _startAtackAfter, _endAtack = 3.2f;
    private SpriteRenderer lightning;
    private BoxCollider2D coll;
    private Animator childAnimator;
    private bool left = false, atacking = false;
    [SerializeField]private float timerForParticles = 5.4f;
    private ParticleSystem lightningSpark;
    private int flagParticles = 0;

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

        lightningSpark = GetComponentInChildren<ParticleSystem>();
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
                mirror.transform.position = new Vector3(GameManager.leftRightWall + 0.26f, randomY, 0f);
                left = true;
                break;

            //top
            case 1:
                transform.Rotate(new Vector3(0f, 0f, -90f));
                transform.position = new Vector3(randomX, GameManager.topBottom, 0f);
                mirror.transform.position = new Vector3(randomX, -GameManager.topBottom - 0.26f, 0f);

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
            var x = Convert.ToSingle(Math.Round((GameManager.leftRightWall / 4) / 0.16f));
            lightning.size = new Vector2(0.16f * x, lightning.size.y);
        }
        else
        {
            var x = Convert.ToSingle(Math.Round((GameManager.topBottom / 4) / 0.16f));
            lightning.size = new Vector2(0.16f * x, lightning.size.y);
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
            
            atacking = true;
            lightning.enabled = true;
            coll.enabled = true;

        }
        if (atacking)
        {
            _endAtack -= Time.deltaTime;
            if (_endAtack <= 0)
            {
                lightning.enabled = false;
                coll.enabled = false;
                atacking = false;
            }
            
        }

        timerForParticles -= Time.deltaTime;
        if (timerForParticles <= 0f)
        {
            if (flagParticles == 0)
            {
                timerForParticles = 0.5f;
                flagParticles++;
            }
            else
            {
                timerForParticles = 5f;
            }
            lightningSpark.Play();
            
        }

    }

}
