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

    [SerializeField] private BoxCollider2D lightningBoxColl;
    [SerializeField] public int lightningDamage = 1;

    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        beetle2.SetActive(false);
        lightning.SetActive(false);
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
            beetle2.GetComponent<AnodesBeetles>().disableAreaAttack();
            if (beetle1 != null)
            {
                if (!beetle1.GetComponent<AnodesBeetles>().killed)
                {
                    isSecondOneSpawned = true;
                }
            }

            timeToSpawnSecondBeetle = 1000f;
        }



        if (isSecondOneSpawned && beetle1 != null)
        {
            timerForConnect -= Time.deltaTime;
            if (timerForConnect <= 0)
            {
                GetComponent<AudioManager>().PlaySound("Lightning");
                lightning.SetActive(true);
                lightningBoxColl.enabled = true;
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
                transform.position = new Vector3(-GameManager.leftRightWall, randomY, 0f);
                beetle2.transform.position = new Vector3(GameManager.leftRightWall, randomY, 0f);
                leftRight = true;
                break;

            //top
            case 1:
                transform.Rotate(new Vector3(0f, 0f, -90f));
                transform.position = new Vector3(randomX, GameManager.topBottom, 0f);
                beetle2.transform.position = new Vector3(randomX, -GameManager.topBottom - offset, 0f);
                
                break;
            //bottom
            case 2:
                transform.Rotate(new Vector3(0f, 0f, 90f));
                transform.position = new Vector3(randomX, -GameManager.topBottom, 0f);
                beetle2.transform.position = new Vector3(randomX, GameManager.topBottom, 0f);
                break;
            //right
            case 3:
                leftRight = true;
                transform.Rotate(new Vector3(0f, 0f, 180f));
                transform.position = new Vector3(GameManager.leftRightWall, randomY, 0f);
                beetle2.transform.position = new Vector3(-GameManager.leftRightWall, randomY, 0f);
                break;
        }

        //LIGHTNING
        // TODO: MANUALLY HARD CODE THE OFFSET 10 and 4. IT WILL NOT WORK FOR OTHERS LEVELS! MAKE A VARIABLE
        if (leftRight)
        {
            lightningBoxColl.size = new Vector3(lightningBoxColl.size.x * 10f, 1f, 0f);
            lightningBoxColl.offset = new Vector3(GameManager.leftRightWall, 0f, 0f);
            lightning.transform.localPosition = new Vector2(0f, 0f);
            lightningSR.size = new Vector2(0.32f * 10f, lightningSR.size.y);
        }
        else
        {
            lightningBoxColl.size = new Vector3(lightningBoxColl.size.x * 4f, 1f, 0f);
            lightningBoxColl.offset = new Vector3(GameManager.topBottom, 0f, 0f);
            lightningSR.size = new Vector2(0.32f * 4f, lightningSR.size.y);
        }
    }

    public void OneIsKilled (GameObject beetle)
    {
        GetComponent<AudioManager>().audioSource.enabled = false;
        connected = false;
        isSecondOneSpawned = false;
        lightning.SetActive(false);
        lightningBoxColl.enabled = false;
        if (beetle1 != null && beetle2 != null)
        {
            if (beetle == beetle1)
            {
                if (beetle2.activeSelf)
                {
                    _beetle2.enableAreaAttack();
                }
                
            }
            else
            {
                _beetle1.enableAreaAttack();

            }
        }
        else
        {
            GameObject.Find("spawner").GetComponent<spawner>().ResetAnodeBeetle();
            Destroy(this, 4f);
        }
        
    }

}
