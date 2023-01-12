using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    public float velocity = 5f;

    private SpriteRenderer playerSprite;
    
    
    void Start()
    {
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.W) && gameObject.transform.position.y < GameManager.topBottom)
        {
            transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);
            
        }
        if (Input.GetKey(KeyCode.A) && gameObject.transform.position.x > -GameManager.leftRightWall)
        {
            transform.Translate(new Vector3(-velocity,0, 0) * Time.deltaTime);
            playerSprite.flipX = false;
            
        }
        if (Input.GetKey(KeyCode.D) && gameObject.transform.position.x < GameManager.leftRightWall)
        {
            transform.Translate(new Vector3(velocity, 0, 0) * Time.deltaTime);
            playerSprite.flipX = true;
        }
        if (Input.GetKey(KeyCode.S) && gameObject.transform.position.y > -GameManager.topBottom + 0.3f)
        {
            transform.Translate(new Vector3(0, -velocity, 0) * Time.deltaTime);
        }


    }

}
