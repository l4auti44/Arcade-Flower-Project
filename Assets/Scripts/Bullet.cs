
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float speed = 3f;
    public float damage = 20f;
    
    private GameObject player;

    private bool look = false;
    private float targetTime = 0.2f;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {

        setRotation();

    }

    private void setRotation()
    {
        var x = transform.position.x; 
        var y = transform.position.y;
        var wallx = GameManager.leftRightWall - 0.1f;
        var wally = GameManager.topBottom - 0.1f;
        offset = Random.Range(-30f, 30f);
        if (x == wallx)
        {
            transform.Rotate(new Vector3(0, 0, 180f + offset));

        }
        else if (x == -wallx)
        {
            transform.Rotate(new Vector3(0, 0, 0f + offset));
        }
        else if (y == wally) 
        {
            transform.Rotate(new Vector3(0, 0, 270f + offset));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 90f + offset));
        }


    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;


        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);


        //Destroy out borders
        if (gameObject.transform.position.y > GameManager.topBottom || gameObject.transform.position.y < -GameManager.topBottom  || gameObject.transform.position.x > GameManager.leftRightWall || gameObject.transform.position.x < -GameManager.leftRightWall)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health>().decreaseHealth(damage);
        GameObject.Destroy(this.gameObject);
    }
}
