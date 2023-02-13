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
    private float timer = 3.6f;
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

        if (_pellet.pelletTaken)
        {
            gameObject.GetComponentInChildren<Animator>().SetBool("pelletTaken", true);
            timer -= Time.deltaTime;
            if (timer <= 0)
                transform.parent.position = Vector2.MoveTowards(transform.parent.position, startGlobalPosition, Time.deltaTime * speed * 2);

        }
        else
        {
            if (!backwards)
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
                //DRAGGING PELLET
                transform.parent.position = Vector2.MoveTowards(transform.parent.position, startGlobalPosition, Time.deltaTime * globalSpeedDragging);
            }



        }

    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
