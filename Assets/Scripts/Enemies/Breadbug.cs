using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Breadbug : MonoBehaviour
{
    private float destroyAfter = 15f;
    public float distanceOffset = 1.5f;
    [SerializeField] private float speed = 0.5f, globalSpeedDragging = 1f;
    private Pellet _pellet;
    private bool backwards = false;
    private Vector2 startGlobalPosition;
    private float timer = 3.6f, timer2 = 2.85f, timer3 = 1f;
    private bool killed = false;
    [SerializeField] private GameObject floatingPoints;

    [SerializeField] public int pointsForKill = 50;
    private bool flagMusic = false, flagDead = false;
    private Vector3 targetPosition;
    [SerializeField] private float pelletGrabbingOffset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPosition();
        _pellet = transform.parent.gameObject.GetComponent<Pellet>();
    }

    private void SpawnPosition()
    {
        var distanceX = GameManager.leftRightWall - Mathf.Abs(transform.parent.position.x);
        var distanceY = GameManager.topBottom - Mathf.Abs(transform.parent.position.y);
        if (distanceX < distanceY)
        {
            if (transform.parent.position.x > 0)
            {
                //left
                transform.localPosition = new Vector3(distanceOffset, 0f, 0f);
                transform.Rotate(0f, 0f, -90f);
                targetPosition = Vector3.zero - new Vector3(-pelletGrabbingOffset, 0f, 0f);
            }
            else
            {
                //Right
                transform.localPosition = new Vector3(-distanceOffset, 0f, 0f);
                transform.Rotate(0f, 0f, 90f);
                targetPosition = Vector3.zero - new Vector3(pelletGrabbingOffset, 0f, 0f);
            }
            

        }
        else
        {
            if (transform.parent.position.y > 0)
            {
                //top
                transform.localPosition = new Vector3(0f, distanceOffset, 0f);
                targetPosition = Vector3.zero - new Vector3(0f, -pelletGrabbingOffset, 0f);
            }
            else
            {
                //bottom
                transform.localPosition = new Vector3(0f, -distanceOffset, 0f);
                transform.Rotate(0f, 0f, 180f);
                targetPosition = Vector3.zero - new Vector3(0f, pelletGrabbingOffset, 0f);
            }
            
        }

        startGlobalPosition = new Vector2(transform.position.x, transform.position.y);

    }

    // Update is called once per frame
    void Update()
    {

        destroyAfter -= Time.deltaTime;
        if (destroyAfter <= 0)
        {
            GameObject.Destroy(transform.parent.gameObject);
        }

        if (_pellet.pelletTaken && !killed)
        {
            gameObject.GetComponentInChildren<Animator>().SetBool("pelletTaken", true);
            timer -= Time.deltaTime;
            if (timer <= 0)
          
                transform.parent.position = Vector2.MoveTowards(transform.parent.position, startGlobalPosition, Time.deltaTime * speed * 5);

        }
        else
        {
            if (!backwards && !flagDead)
            {
                
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPosition, Time.deltaTime * speed);

                if (transform.localPosition == targetPosition)
                {
                    backwards = true;
                    gameObject.GetComponentInChildren<Animator>().SetBool("backwards", true);
                }
            }
            else
            {
                if (!flagDead)
                {
                    //DRAGGING PELLET
                    transform.parent.position = Vector2.MoveTowards(transform.parent.position, startGlobalPosition, Time.deltaTime * globalSpeedDragging);
                }
                
            }

            if (killed)
            {

                timer3 -= Time.deltaTime;
                if (timer3 <= 0 && !flagDead)
                {
                    GameObject.Instantiate(floatingPoints, transform.position, Quaternion.identity, transform);
                    GetComponent<AudioManager>().PlaySound("Killed");
                    gameObject.GetComponentInChildren<Animator>().SetBool("killed", true);
                    GameObject.Find("GameController").GetComponent<GameManager>().addPoints(pointsForKill);
                    flagDead = true;
                }

                timer2 -= Time.deltaTime;
                if (timer2 <= 0)
                {
                    if (transform.eulerAngles.z == 0)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 30), Time.deltaTime * speed * 5);
                    }
                    else if (transform.eulerAngles.z == -180 || transform.eulerAngles.z == 180)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -30), Time.deltaTime * speed * 5);
                    }
                    else if (transform.eulerAngles.z == 90)

                    {
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(-30, transform.position.y), Time.deltaTime * speed * 5);
                    }
                    else
                    {
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(30, transform.position.y), Time.deltaTime * speed * 5);
                    }
                }

            }
            

        }
        if (backwards && !flagDead && _pellet.pelletTaken)
        {
            if (!flagMusic)
            {
                GetComponent<AudioManager>().PlaySound("Rejection");
                flagMusic = true;
            }
        }

    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    public bool Killed() 
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
