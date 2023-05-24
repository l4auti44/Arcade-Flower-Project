using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class AnodesBeetles : Enemy
{

    private CircleCollider2D areaAttack;
    private SpriteRenderer areaAttackSprite;

    private float startSlplashAttack = 2f;

    [SerializeField] private float timeToSpawnSecondBeetle = 3f;
    //[SerializeField] private GameObject secondBeetle;
    private bool isSecondOne = false;


    void Start()
    {

        areaAttackSprite = getSpriteRenderer("areaAttack");
        areaAttack = GetComponent<CircleCollider2D>();

        //checkIfSecondBeetle();


        spawnPosition();
        
        //enableAreaAttack();
    }


    void Update()
    {
        switch (isSecondOne)
        {
            case true:
                break;
            

            case false:
                startSlplashAttack -= Time.deltaTime;
                timeToSpawnSecondBeetle -= Time.deltaTime;
                if (startSlplashAttack <= 0)
                {
                    enableAreaAttack();
                    startSlplashAttack = 100f;
                    //Destroy(gameObject, 10f);

                }
                /*
                if (timeToSpawnSecondBeetle <= 0)
                {
                    Instantiate(secondBeetle, transform);
                    gameObject.GetComponentInChildren<Animator>().SetBool("connect", true);
                    enableAreaAttack();
                    timeToSpawnSecondBeetle = 100f;
                }
                */
                break;
        }

        if (killed)
        {
            Destroy(gameObject, 2f);
        }
        
        
    }

    private void spawnPosition()
    {
        var offset = 0.5f;
        var randomX = UnityEngine.Random.Range(-GameManager.leftRightWall + offset, GameManager.leftRightWall - offset);
        var randomY = UnityEngine.Random.Range(-GameManager.topBottom + offset, GameManager.topBottom - offset);

        var randomWall = UnityEngine.Random.Range(0, 4);


        switch (randomWall)
        {
            //left
            case 0:
                transform.position = new Vector3(-GameManager.leftRightWall - offset, randomY, 0f);
                
                break;

            //top
            case 1:
                transform.Rotate(new Vector3(0f, 0f, -90f));
                transform.position = new Vector3(randomX, GameManager.topBottom + offset, 0f);

                break;
            //bottom
            case 2:
                transform.Rotate(new Vector3(0f, 0f, 90f));
                transform.position = new Vector3(randomX, -GameManager.topBottom - offset, 0f);

                break;
            //right
            case 3:
                transform.Rotate(new Vector3(0f, 0f, 180f));
                transform.position = new Vector3(GameManager.leftRightWall + offset, randomY, 0f);

                break;
        }
    }
    private void enableAreaAttack() {

        if (areaAttackSprite.enabled == true)
        {
            areaAttackSprite.enabled = false;
            areaAttack.enabled = false;
        }
        else
        {
            areaAttackSprite.enabled = true;
            areaAttack.enabled = true;
        }
        

    }
    /*
    private void secondSpawnPosition()
    {
        if (transform.parent.eulerAngles == new Vector3(0f, 0f, 270f) || transform.parent.eulerAngles == new Vector3(0f, 0f, 90f))
        {

            transform.localPosition = new Vector3(GameManager.topBottom * 2, 0f, 0f);
        }
        else
        {
            transform.localPosition = new Vector3(GameManager.leftRightWall * 2, 0f, 0f);
        }
    }
    */
    private void checkIfSecondBeetle()
    {
        var thereIsAnotherOne = GameObject.Find("AnodesBeetle(Clone)");

        if (thereIsAnotherOne != null)
        {
            Debug.Log("there is a second one");
            isSecondOne= true;
        }
    }
}
