using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemy;

    private float targetTime = 1f;
    private int randomDirection;
    private float positionSpawnX, positionSpawnY;
    private float xWalls, yWalls;

    // Start is called before the first frame update
    void Start()
    {
        xWalls = GameManager.leftRightWall - 0.1f;
        yWalls = GameManager.topBottom - 0.1f;
  
    }

    private void FixedUpdate()
    {
        targetTime -= Time.deltaTime;
        
        if (targetTime <= 0.0f)
        {

            randomDirection = Random.Range(0, 4);

            positionSpawnX = Random.Range(-xWalls, xWalls);
            positionSpawnY = Random.Range(-yWalls, yWalls);

            //rigth
            if (randomDirection == 0)
            {
                GameObject.Instantiate(enemy, new Vector3(xWalls, positionSpawnY, 0), this.transform.rotation);
            }
            //bottom
            else if (randomDirection == 1)
            {

                GameObject.Instantiate(enemy, new Vector3(positionSpawnX, -yWalls, 0), this.transform.rotation);
            }
            //top
            else if (randomDirection == 2)
            {

                GameObject.Instantiate(enemy, new Vector3(positionSpawnX, yWalls, 0), this.transform.rotation);
            }
            //left
            else
            {

                GameObject.Instantiate(enemy, new Vector3(-xWalls, positionSpawnY, 0), this.transform.rotation);
            }
            
            targetTime = 1f;
        }

    }
}
