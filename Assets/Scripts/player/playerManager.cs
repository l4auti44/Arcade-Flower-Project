using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    public float velocity = 5f;

    private SpriteRenderer playerSprite;
    private Animator playerAnimator;
    [HideInInspector] public bool invincible = false;
    [SerializeField] private float timeInvincible = 1.5f;
    private float offset = 0.3f;
    

    private BoxCollider2D playerColl;
    void Start()
    {
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerAnimator= gameObject.GetComponent<Animator>();
        playerColl = gameObject.GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        Move();


        if (invincible)
        {
            timeInvincible -= Time.deltaTime;
            if (timeInvincible <= 0)
            {
                timeInvincible = 2f;
                invincible = false;
            }
        }

    }

    private void Move()
    {
        float horizontal = 0f;
        float vertical = 0f;
        

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && gameObject.transform.position.y < GameManager.topBottom - offset)
        {
            vertical = velocity;

        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && gameObject.transform.position.x > -GameManager.leftRightWall + offset)
        {
            horizontal = -velocity;
            playerSprite.flipX = false;
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && gameObject.transform.position.x < GameManager.leftRightWall - offset)
        {
            horizontal = velocity;
            playerSprite.flipX = true;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && gameObject.transform.position.y > -GameManager.topBottom + (offset * 2f))
        {
            vertical = -velocity;
        }

        Vector2 direction = new Vector2(horizontal, vertical);
        if (direction.magnitude > 0.001)
        {
            direction.Normalize();
 }
        else
        {
            return;
        }
        transform.Translate(direction * velocity * Time.deltaTime, Space.World);
        
    }

    public void takeDamage()
    {
        GetComponent<AudioManager>().PlaySound("Damage");
        playerAnimator.Play("takingDamage", 0, 0.0f);
        invincible = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            if (!invincible)
            {
                takeDamage();
                var damage = collision.gameObject.GetComponent<Enemy>().damage;
                gameObject.GetComponent<Health>().decreaseHealth(damage);
            }

        }
        if (collision.gameObject.tag == "Bullet")
        {

            if (!invincible)
            {
                takeDamage();
                var damage = collision.gameObject.GetComponent<Bullet>().damage;
                gameObject.GetComponent<Health>().decreaseHealth(damage);
                Destroy(collision.gameObject);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            if (!invincible)
            {
                takeDamage();
                var damage = collision.gameObject.GetComponent<Enemy>().damage;
                gameObject.GetComponent<Health>().decreaseHealth(damage);
            }

        }
        if (collision.gameObject.tag == "Bullet")
        {

            if (!invincible)
            {
                takeDamage();
                var damage = collision.gameObject.GetComponent<Bullet>().damage;
                gameObject.GetComponent<Health>().decreaseHealth(damage);
                Destroy(collision.gameObject);
            }

        }

    
    }

}
