using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    public float velocity = 5f;

    private SpriteRenderer playerSprite;
    private Animator playerAnimator;
    public bool invincible = false;
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
        if (Input.GetKey(KeyCode.W) && gameObject.transform.position.y < GameManager.topBottom - offset)
        {
            transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.A) && gameObject.transform.position.x > -GameManager.leftRightWall + offset)
        {
            transform.Translate(new Vector3(-velocity, 0, 0) * Time.deltaTime);
            playerSprite.flipX = false;
        }
        if (Input.GetKey(KeyCode.D) && gameObject.transform.position.x < GameManager.leftRightWall - offset)
        {
            transform.Translate(new Vector3(velocity, 0, 0) * Time.deltaTime);
            playerSprite.flipX = true;
        }
        if (Input.GetKey(KeyCode.S) && gameObject.transform.position.y > -GameManager.topBottom + (offset * 2f))
        {
            transform.Translate(new Vector3(0, -velocity, 0) * Time.deltaTime);
        }

    }

    public void takeDamage()
    {
        playerAnimator.Play("takingDamage", 0, 0.0f);
        invincible = true;
    }

}
