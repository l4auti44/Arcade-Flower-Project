using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breadbug : MonoBehaviour
{
    private float offset = 5f, destroyAfter = 5f, speed = 2f;
    private Pellet _pellet;
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
                transform.position = new Vector3(transform.parent.position.x + offset, transform.parent.position.y, 0f);
                transform.Rotate(0f, 0f, -90f);
            }
            else
            {
                transform.position = new Vector3(transform.parent.position.x - offset, transform.parent.position.y, 0f);
                transform.Rotate(0f, 0f, 90f);
            }

        }
        else
        {
            if (transform.parent.position.y > 0)
            {
                transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + offset, 0f);
            }
            else
            {
                transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y - offset, 0f);
                transform.Rotate(0f, 0f, 180f);
            }

        }
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
        }

        
    }
}
