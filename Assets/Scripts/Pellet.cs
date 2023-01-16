using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float randomX = Random.Range(-GameManager.leftRightWall, GameManager.leftRightWall);
        float randomY = Random.Range(-GameManager.topBottom, GameManager.topBottom);
        transform.position = new Vector3(randomX, randomY, 0f);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.pickUpPellet();
        Destroy(gameObject);
    }
}
