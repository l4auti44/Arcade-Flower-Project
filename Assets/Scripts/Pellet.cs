using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField] private GameObject breadbug;
    [SerializeField] private float timeForSpawnBreadbug = 5f;
    private bool spawnedBreedbug = false;
    [HideInInspector] public bool pelletTaken = false;
    [SerializeField] public int points = 30;
    [SerializeField] private GameObject floatingPoints;
    // Start is called before the first frame update
    void Start()
    {
        float randomX = Random.Range(-GameManager.leftRightWall, GameManager.leftRightWall);
        float randomY = Random.Range(-GameManager.topBottom, GameManager.topBottom);
        transform.position = new Vector3(randomX, randomY, 0f);
    }


    private void Update()
    {
        timeForSpawnBreadbug -= Time.deltaTime;

        if (timeForSpawnBreadbug <= 0f && !spawnedBreedbug && !pelletTaken)
        {
            GameObject.Instantiate(breadbug, this.transform, true);
            spawnedBreedbug = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var floatingPo = Instantiate(floatingPoints, transform.position, Quaternion.identity, transform);
        floatingPo.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        GetComponent<AudioSource>().Play();
        GameObject.Find("GameController").GetComponent<GameManager>().pickUpPellet(points);
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        pelletTaken = true;
        if (!spawnedBreedbug)
        {
            Destroy(gameObject, 1f);
        }
        
    }
}
