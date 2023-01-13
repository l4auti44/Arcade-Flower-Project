using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    public float velocity = 5f;

    private SpriteRenderer playerSprite;
    private Animator playerAnimator;
    private bool invincible = false;
    private float timeInvincible = 2f;
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

        if (Input.GetKey(KeyCode.W) && gameObject.transform.position.y < GameManager.topBottom - offset)
        {
            transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);
            
        }
        if (Input.GetKey(KeyCode.A) && gameObject.transform.position.x > -GameManager.leftRightWall + offset)
        {
            transform.Translate(new Vector3(-velocity,0, 0) * Time.deltaTime);
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

        if (invincible)
        {
            timeInvincible -= Time.deltaTime;
            if (timeInvincible <= 0)
            {
                playerColl.enabled = true;
                timeInvincible = 2f;
                invincible = false;
            }
        }


    }

    public void takingDamage() 
    {
        playerAnimator.Play("takingDamage", 0, 0.0f);
        playerColl.enabled = false;
        invincible = true;
    }

}
