using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnodeController : MonoBehaviour
{
    [SerializeField] private GameObject beetle1;
    [SerializeField] private GameObject beetle2;
    [SerializeField] private float timeToSpawnSecondBeetle;
    [SerializeField] private GameObject lightning;
    public float timerForConnect = 3f;
    [HideInInspector] public bool isSecondOneSpawned = false;
    [HideInInspector] public bool connected = false;
    private AnodesBeetles _beetle1, _beetle2;

    // Start is called before the first frame update
    private void Awake()
    {
        beetle2.SetActive(false);
        lightning.SetActive(false);
    }
    void Start()
    {
        SpawnPosition();
        _beetle1 = beetle1.GetComponent<AnodesBeetles>();
        _beetle2 = beetle2.GetComponent<AnodesBeetles>();
        
    }

    private void Update()
    {
        timeToSpawnSecondBeetle -= Time.deltaTime;
        if (timeToSpawnSecondBeetle <= 0)
        {
            beetle2.SetActive(true);
            
            isSecondOneSpawned = true;
            
            timeToSpawnSecondBeetle = 1000f;
        }
        if (isSecondOneSpawned)
        {
            timerForConnect -= Time.deltaTime;
            if (timerForConnect <= 0)
            {
                lightning.SetActive(true);
                connected = true;
                timerForConnect = 1000f;
            }
        }
    }


    private void SpawnPosition()
    {
        var offset = 0.5f;
        var randomX = UnityEngine.Random.Range(-GameManager.leftRightWall + offset, GameManager.leftRightWall - offset);
        var randomY = UnityEngine.Random.Range(-GameManager.topBottom + offset, GameManager.topBottom - offset);

        var randomWall = UnityEngine.Random.Range(0, 4);
        var lightningSR = lightning.GetComponent<SpriteRenderer>();

        bool leftRight = false;

        switch (randomWall)
        {
            //left
            case 0:
                transform.position = new Vector3(-GameManager.leftRightWall - offset, randomY, 0f);
                beetle2.transform.position = new Vector3(GameManager.leftRightWall + offset, randomY, 0f);
                leftRight = true;
                break;

            //top
            case 1:
                transform.Rotate(new Vector3(0f, 0f, -90f));
                transform.position = new Vector3(randomX, GameManager.topBottom + offset, 0f);
                beetle2.transform.position = new Vector3(randomX, -GameManager.topBottom - offset, 0f);
                
                break;
            //bottom
            case 2:
                transform.Rotate(new Vector3(0f, 0f, 90f));
                transform.position = new Vector3(randomX, -GameManager.topBottom - offset, 0f);
                beetle2.transform.position = new Vector3(randomX, GameManager.topBottom + offset, 0f);
                break;
            //right
            case 3:
                leftRight = true;
                transform.Rotate(new Vector3(0f, 0f, 180f));
                transform.position = new Vector3(GameManager.leftRightWall + offset, randomY, 0f);
                beetle2.transform.position = new Vector3(-GameManager.leftRightWall - offset, randomY, 0f);
                break;
        }

        //LIGHTNING
        var offsetLight = 0.07f;
        if (leftRight)
        {
            
            var x = Convert.ToSingle(Math.Round((GameManager.leftRightWall / 4) / 0.32f));
            lightningSR.size = new Vector2(0.32f * x + offsetLight, lightningSR.size.y);
        }
        else
        {
            var x = Convert.ToSingle(Math.Round((GameManager.topBottom / 4) / 0.16f));
            lightningSR.size = new Vector2(0.16f * x + offsetLight, lightningSR.size.y);
        }
    }

    public void OneIsKilled (GameObject beetle)
    {
        
        connected = false;
        isSecondOneSpawned = false;
        lightning.SetActive(false);
        Debug.Log("before if null");
        if (beetle1 != null && beetle2 != null)
        {
            if (beetle == beetle1)
            {
                Debug.Log("the one that is killed is beetle1");
                _beetle2.enableAreaAttack();

            }
            else
            {
                Debug.Log("the one that is killed is beetle2");
                _beetle1.enableAreaAttack();

            }
        }
        else
        {
            Debug.Log("destroy controller");
            Destroy(this, 4f);
        }
        
    }
}
