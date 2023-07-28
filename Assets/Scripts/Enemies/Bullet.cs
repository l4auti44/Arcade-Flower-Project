
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Bullet : Enemy
{
    public float speed = 3f;

    
    private float offset;
    private int randomDirection;
    private float xWalls, yWalls;
    private float positionSpawnX, positionSpawnY;

    
    void Start()
    {
        var _spawner = GameObject.Find("spawner").GetComponent<spawner>();
        xWalls = GameManager.leftRightWall + 3f;
        yWalls = GameManager.topBottom + 3f;

        spawnPosition();

        if (spawner.bulletAmount <= 7)
        {
            
            setRotation(false);
        }
        else
        {
            setRotation(true);
            spawner.bulletAmount = 0;

        }
        

    }

    private void setRotation(bool toPlayer)
    {
        var x = transform.position.x; 
        var y = transform.position.y;
        offset = Random.Range(-30f, 30f);
        if (toPlayer)
        {
            Vector3 diff = GameObject.Find("Player").transform.position - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

            this.GetComponent<SpriteRenderer>().color = Color.blue;

        }
        else
        {
            if (x == xWalls)
            {
                transform.Rotate(new Vector3(0, 0, 180f + offset));

            }
            else if (x == -xWalls)
            {
                transform.Rotate(new Vector3(0, 0, 0f + offset));
            }
            else if (y == yWalls)
            {
                transform.Rotate(new Vector3(0, 0, 270f + offset));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 90f + offset));
            }
        }

        
        
        
        


    }

    void Update()
    {
        
       
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        
        
        //Destroy out borders
        if (gameObject.transform.position.y > yWalls + 0.1f || gameObject.transform.position.y < -yWalls - 0.1f
            || gameObject.transform.position.x > xWalls + 0.1f || gameObject.transform.position.x < -xWalls - 0.1f)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void spawnPosition()
    {
        randomDirection = Random.Range(0, 4);

        positionSpawnX = Random.Range(-xWalls, xWalls);
        positionSpawnY = Random.Range(-yWalls, yWalls);

        //rigth
        if (randomDirection == 0)
        {
            transform.position = new Vector3(xWalls, positionSpawnY, 0);
        }
        //bottom
        else if (randomDirection == 1)
        {
            transform.position = new Vector3(positionSpawnX, -yWalls, 0);
        }
        //top
        else if (randomDirection == 2)
        {
            transform.position = new Vector3(positionSpawnX, yWalls, 0);
        }
        //left
        else
        {
            transform.position = new Vector3(positionSpawnX, yWalls, 0);
        }
    }


}
