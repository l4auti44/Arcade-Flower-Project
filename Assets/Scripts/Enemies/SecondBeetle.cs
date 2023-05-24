using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBeetle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        if (transform.parent.eulerAngles == new Vector3(0f, 0f, 270f) || transform.parent.eulerAngles == new Vector3(0f, 0f, 90f))
        {
            
            transform.localPosition = new Vector3(GameManager.topBottom * 2, 0f, 0f);
        }
        else
        {
            transform.localPosition = new Vector3(GameManager.leftRightWall * 2, 0f, 0f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
