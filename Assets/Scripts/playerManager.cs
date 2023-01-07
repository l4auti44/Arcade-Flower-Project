using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    public float velocity = 5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey(KeyCode.W) && gameObject.transform.position.y < GameManager.leftRightWall - transform.localScale.x)
        {
            this.transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) && gameObject.transform.position.x > -GameManager.leftRightWall + transform.localScale.x)
        {
            this.transform.Translate(new Vector3(-velocity,0, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) && gameObject.transform.position.x < GameManager.topBottom - transform.localScale.x)
        {
            this.transform.Translate(new Vector3(velocity, 0, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) && gameObject.transform.position.y > -GameManager.topBottom + transform.localScale.x)
        {
            this.transform.Translate(new Vector3(0, -velocity, 0) * Time.deltaTime);
        }


    }

}
