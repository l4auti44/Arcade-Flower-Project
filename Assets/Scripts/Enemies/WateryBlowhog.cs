using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WateryBlowhog : Enemy
{

    [SerializeField] private float startAtackAfter = 2f;
    [SerializeField] private float atackDuration = 0.8f;
    [SerializeField] private float lifeTime = 10f;
    private float _startAtackAfter, _atackDuration;
    private BoxCollider2D splashCol;
    private ParticleSystem waterParticles;
    private Animator wateryAnimator;
    private float timer1 = 1f;
    private bool flagExit = false, flagdead = false;

    private float randomX, randomY;


    void Start()
    {
        _startAtackAfter = startAtackAfter;
        _atackDuration = atackDuration;


        waterParticles = gameObject.GetComponentInChildren<ParticleSystem>();
        wateryAnimator = gameObject.GetComponentInChildren<Animator>();
       
        splashCol = gameObject.GetComponent<BoxCollider2D>();

        splashCol.enabled = false;
        waterParticles.Stop();

        Dictionary<string, Vector3> positions = spawner.Instance.GetSpawnPositionOnBorderOfArea();
        transform.position = positions["position"];
        transform.Rotate(positions["rotation"]);
        
    }


    void Update()
    {
        _startAtackAfter -= Time.deltaTime;
        lifeTime -= Time.deltaTime;

        if (killed)
        {
            timer1 -= Time.deltaTime;
            if (timer1 <= 0 && !flagdead)
            {
                GetComponent<AudioManager>().PlaySound("Killed");
                wateryAnimator.SetBool("killed", true);
                if (splashCol.enabled == true)
                {
                    enableSplash();
                }
                GameObject.Find("GameController").GetComponent<GameManager>().addPoints(pointsForKill);
                GameObject.Instantiate(floatingPoints, transform.position, Quaternion.identity, transform);
                flagdead = true;
            }
        }

        if (!flagdead)
        {
            if (_startAtackAfter <= 0f)
            {
                enableSplash();
                flagExit = true;
                GetComponent<AudioManager>().PlaySound("Attack");
                _startAtackAfter = 999f;
            }


            if (flagExit)
            {
                _atackDuration -= Time.deltaTime;
                if (_atackDuration <= 0)
                {
                    enableSplash();
                    wateryAnimator.SetBool("exit", true);
                    flagExit = false;
                }

            }
        }

        if (lifeTime <= 0f)
        {
            GameObject.Destroy(gameObject);
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

    override public bool Killed()
    {
        if (killed == false)
        {
            killed = true;
            return false;
        }
        else
        {
            return true;
        }

    }

}
