using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TextMeshProUGUI points;
    private float numberPoints;
    private float time = 1f;
    

    public float left_right, top_bottom;
    
    public static float leftRightWall;
    public static float topBottom;
    

    // Start is called before the first frame update
    void Start()
    {

        leftRightWall = left_right;
        topBottom = top_bottom;
        points = GameObject.Find("points").GetComponent<TextMeshProUGUI>();
        numberPoints = 0;
        points.text = "Points: " + numberPoints;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            numberPoints += 10;
            points.text = "Points: " + numberPoints;
            time += 1;
        }
        
        



    }

    
}
