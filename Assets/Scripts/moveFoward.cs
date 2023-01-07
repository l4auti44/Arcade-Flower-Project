
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFoward : MonoBehaviour
{

    public float speed = 5f;
    public float rotationZ = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime) ;
        //transform.rotation = new Quaternion(0f, 0f, rotationZ, transform.rotation.w);
        Debug.Log(transform.rotation.w);
    }
}
