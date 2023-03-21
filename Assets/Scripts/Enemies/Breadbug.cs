using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Breadbug : MonoBehaviour
{
    private float destroyAfter = 15f, distanceOffset = 1.5f;
    [SerializeField] private float speed = 0.5f, globalSpeedDragging = 1f;
    private Pellet _pellet;
    private bool backwards = false;
    private Vector2 startGlobalPosition;
    private float timer = 3.6f, timer2 = 1.85f;
    private bool killed = false;
    [SerializeField] private GameObject floatingPoints;

    [SerializeField] public int pointsForKill = 50;
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
                transform.localPosition = new Vector3(distanceOffset, 0f, 0f);
                transform.Rotate(0f, 0f, -90f);
            }
            else
            {
                transform.localPosition = new Vector3(-distanceOffset, 0f, 0f);
                transform.Rotate(0f, 0f, 90f);
            }

        }
        else
        {
            if (transform.parent.position.y > 0)
            {
                transform.localPosition = new Vector3(0f, distanceOffset, 0f);
            }
            else
            {
                transform.localPosition = new Vector3(0f, -distanceOffset, 0f);
                transform.Rotate(0f, 0f, 180f);
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
            if (!backwards && !killed)
            {
                
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, Vector2.zero, Time.deltaTime * speed);

                if (transform.localPosition == Vector3.zero)
                {
                    backwards = true;
                    gameObject.GetComponentInChildren<Animator>().SetBool("backwards", true);
                }
            }
            else
            {
                if (!killed)
                {
                    //DRAGGING PELLET
                    transform.parent.position = Vector2.MoveTowards(transform.parent.position, startGlobalPosition, Time.deltaTime * globalSpeedDragging);
                }
                
            }

            if (killed)
            {
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

    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    public bool Killed() 
    {
        if (killed == false)
        {
            gameObject.GetComponentInChildren<Animator>().SetBool("killed", true);
            killed = true;
            GameObject.Find("GameController").GetComponent<GameManager>().addPoints(pointsForKill);
            GameObject.Instantiate(floatingPoints, transform.position, Quaternion.identity, transform);
            return false;
        }
        else
        {
            return true;
        }
        

    }
}
