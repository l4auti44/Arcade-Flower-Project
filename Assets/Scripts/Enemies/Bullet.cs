
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float speed = 3f;
    public float damage = 20f;
    
    private float offset;
    private int randomDirection;
    private float xWalls, yWalls;
    private float positionSpawnX, positionSpawnY;

    
    void Start()
    {
        xWalls = GameManager.leftRightWall + 3f;
        yWalls = GameManager.topBottom + 3f;

        spawnPosition();
        setRotation();

    }

    private void setRotation()
    {
        var x = transform.position.x; 
        var y = transform.position.y;
        

        offset = Random.Range(-30f, 30f);
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

    void Update()
    {
        
       
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        
        


        //Destroy out borders
        if (gameObject.transform.position.y > yWalls + 0.1f || gameObject.transform.position.y < -yWalls - 0.1f
            || gameObject.transform.position.x > xWalls + 0.1f || gameObject.transform.position.x < -xWalls - 0.1f)
        {
            Debug.Log("destroy Bullet");
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




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().decreaseHealth(damage);
            GameObject.Destroy(this.gameObject);
        }

    }
}
