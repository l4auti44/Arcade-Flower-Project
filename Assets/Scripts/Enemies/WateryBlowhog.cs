using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WateryBlowhog : Enemy
{

    public float startAtackAfter = 2f;
    public float lifeTime = 10f;
    private float _startAtackAfter;
    private BoxCollider2D splashCol;
    private ParticleSystem waterParticles;
    private Animator wateryAnimator;

    private int flagExit = 0;

    void Start()
    {
        _startAtackAfter = startAtackAfter;

        

        waterParticles = gameObject.GetComponentInChildren<ParticleSystem>();
        wateryAnimator = gameObject.GetComponentInChildren<Animator>();
       
        splashCol = gameObject.GetComponent<BoxCollider2D>();

        splashCol.enabled = false;
        waterParticles.Stop();

       
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




    private void enableSplash() {
        
        
        if (splashCol.enabled == true)
        {
            waterParticles.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
            splashCol.enabled = false;

            
        }
        else
        {
            waterParticles.Play();
            splashCol.enabled = true;

            
        }
    }


}
