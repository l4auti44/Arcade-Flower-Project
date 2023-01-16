using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WateryBlowhog : Enemy
{

    public float startAtackAfter = 2f;
    public float lifeTime = 10f;
    private float _startAtackAfter;
    private SpriteRenderer splashRender;
    private BoxCollider2D splashCol;
    private bool shooting = false;
    private ParticleSystem waterParticles;
    private Animator wateryAnimator;

    private int flagExit = 0;

    void Start()
    {
        _startAtackAfter = startAtackAfter;

        splashRender = getSpriteRenderer("splash");

        waterParticles = gameObject.GetComponentInChildren<ParticleSystem>();
        wateryAnimator = gameObject.GetComponentInChildren<Animator>();

        splashCol = gameObject.GetComponent<BoxCollider2D>();
        waterParticles.Stop();
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
            flagExit += 1;
            enableSplash();
            _startAtackAfter = startAtackAfter;
        }
        if (lifeTime <= 0f)
        {
            GameObject.Destroy(gameObject);
        }

        if (flagExit == 2)
        {
            wateryAnimator.SetBool("exit", true);
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
            waterParticles.Stop();
        }
        else
        {
            splashCol.enabled = true;
            splashRender.enabled = true;
            shooting = true;
            waterParticles.Play();
        }
    }

    private List<GameObject> getChildren() { 
        List<GameObject> children = new List<GameObject>();

        foreach (GameObject child in this.transform)
        {
            children.Add(child);
        }
        return children;
    }

}
