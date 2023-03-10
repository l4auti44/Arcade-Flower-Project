using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField] private GameObject breadbug;
    private float timePass = 0f;
    private bool spawnedBreedbug = false;
    public bool pelletTaken = false;
    // Start is called before the first frame update
    void Start()
    {
        float randomX = Random.Range(-GameManager.leftRightWall, GameManager.leftRightWall);
        float randomY = Random.Range(-GameManager.topBottom, GameManager.topBottom);
        transform.position = new Vector3(randomX, randomY, 0f);
    }


    private void Update()
    {
        timePass += Time.deltaTime;

        if (timePass >= 5f && !spawnedBreedbug && !pelletTaken)
        {
            GameObject.Instantiate(breadbug, this.transform, true);
            spawnedBreedbug = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("GameController").GetComponent<GameManager>().pickUpPellet();
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        pelletTaken = true;
        if (!spawnedBreedbug)
        {
            Destroy(gameObject);
        }
        
    }
}
