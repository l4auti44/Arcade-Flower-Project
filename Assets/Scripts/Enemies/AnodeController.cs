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

    [SerializeField] private Sprite[] lightnings;
    private SpriteRenderer lightningSR;
    private Dictionary<string, Vector3> positions;


    void Start()
    {
        beetle2.SetActive(false);

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
                if (positions["positionOnArea"] == Vector3.left || positions["positionOnArea"] == Vector3.right)
                {
                    lightning.GetComponent<Animator>().Play("flipY");
                }
                else
                {
                    lightning.GetComponent<Animator>().Play("flipX");
                }
                timerForConnect = 1000f;
 
            }
        }

        

      
        
    }


    private void SpawnPosition()
    {
        float offsetFromBorderTop = 1.22f;
        positions = spawner.Instance.GetSpawnPositionOnBorderOfArea();
        transform.position = positions["position"];
        transform.Rotate(positions["rotation"]);


        //LIGHTNING
        // TODO: MANUALLY HARD CODE THE OFFSET 10 and 4. IT WILL NOT WORK FOR OTHERS LEVELS! MAKE A VARIABLE
        lightningSR = lightning.GetComponent<SpriteRenderer>();




        if (positions["positionOnArea"] == Vector3.left || positions["positionOnArea"] == Vector3.right)
        {


            if (positions["positionOnArea"] == Vector3.left)
            {
                beetle2.transform.position = new Vector3(GameManager.leftRightWall + (offsetFromBorderTop * 2), positions["position"].y, 0f);
                lightning.transform.Rotate(new Vector3(0f, 0f, -90f));
                transform.position -= new Vector3(offsetFromBorderTop, 0f, 0f);

            }
            else
            {
                beetle2.transform.position = new Vector3(-GameManager.leftRightWall - (offsetFromBorderTop * 2), positions["position"].y, 0f);

                transform.position += new Vector3(offsetFromBorderTop, 0f, 0f);
                lightning.transform.Rotate(new Vector3(0, 0, -90));
            }
            lightningBoxColl.size = new Vector2(1f, lightningBoxColl.size.y * 10f);
            lightningBoxColl.offset = new Vector2(0f, -GameManager.leftRightWall - 1.3f);
            lightning.transform.localPosition = new Vector2(0, -0.5f);
            lightningSR.sprite = lightnings[0]; //left


         
        }
        else //BOTTOM TOP
        {


            if (positions["positionOnArea"] == Vector3.up)
            {

                beetle2.transform.position = new Vector3(positions["position"].x, -GameManager.topBottom - (offsetFromBorderTop * 2), 0f);
                transform.position += new Vector3(0f, offsetFromBorderTop, 0f);
                lightning.transform.Rotate(new Vector3(0, 0, 0));
            }
            else
            {
                beetle2.transform.position = new Vector3(positions["position"].x, GameManager.topBottom + (offsetFromBorderTop * 2), 0f);
                transform.position -= new Vector3(0f, offsetFromBorderTop, 0f);

            }
            lightningBoxColl.size = new Vector2(1f, lightningBoxColl.size.y * 4f);
            lightningBoxColl.offset = new Vector2(0f, -GameManager.topBottom - 1.3f);
            lightning.transform.localPosition = new Vector2(0, -0.5f);
            lightningSR.sprite = lightnings[1]; //Top



        }

        lightning.SetActive(false);

    }

    public void OneIsKilled(GameObject beetle)
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
