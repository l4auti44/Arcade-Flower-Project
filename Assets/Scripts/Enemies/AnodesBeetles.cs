using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;

public class AnodesBeetles : Enemy
{

    private CircleCollider2D areaAttack;
    private SpriteRenderer areaAttackSprite;
    private Animator animator;

    public float startSlplashAttack = 2f;
    private AnodeController controller;
    private bool flag1 = false;

    [SerializeField]private float timerDeath = 1f;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        areaAttackSprite = getSpriteRenderer("areaAttack");
        areaAttack = GetComponent<CircleCollider2D>();
        controller = GetComponentInParent<AnodeController>();

    }


    void Update()
    {
        if (!killed)
        {

            switch (controller.isSecondOneSpawned)
            {
                case (false):
                    startSlplashAttack -= Time.deltaTime;
                    
                    if (startSlplashAttack <= 0)
                    {
                        enableAreaAttack();
                        startSlplashAttack = 1000f;
                    }
                    break;

                case (true):
                    startSlplashAttack = 2f;
                    
                    if (controller.connected)
                    {
                        GetComponent<AudioManager>().audioSource.loop = false;
                        animator.SetBool("areaAttack", false);
                        animator.SetBool("connect", true);
                        
                        EnableConnectedLighthing();
                        
                    }
                    break;
            }
        }
        else
        {
            if (!flag1)
            {
                timerDeath -= Time.deltaTime;
                if (timerDeath <= 0)
                {
                    GetComponent<AudioManager>().audioSource.loop = false;
                    GetComponent<AudioManager>().PlaySound("Killed");
                    controller.OneIsKilled(this.gameObject);
                    flag1 = true;

                    Destroy(gameObject, 2f);
                    timerDeath = 1000f;
                }
                


            }
            
            
        }
    }

    public void disableAreaAttack()
    {


        areaAttackSprite.enabled = false;
        areaAttack.enabled = false;
        animator.SetBool("areaAttack", false);
        



    }
    public void enableAreaAttack() {


        GetComponent<AudioManager>().PlaySound("AreaAttack");
        
        
        areaAttackSprite.enabled = true;
        areaAttack.enabled = true;
        animator.SetBool("areaAttack", true);
        animator.SetBool("connect", false);
        
        

    }

    public void EnableConnectedLighthing()
    {
        if (areaAttack.enabled == true && areaAttackSprite.enabled == true)
        {
            areaAttackSprite.enabled = false;
            areaAttack.enabled = false;
        }

    }

}
