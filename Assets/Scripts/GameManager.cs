using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TextMeshProUGUI points;
    private float numberPoints;
    private float time = 0f;
    

    //[0] left and right, [1] top and bottom, [2] length
    public float[] bordersList;
    public GameObject wall;

    [HideInInspector]
    public Dictionary<string, float> borders;
    public static float leftRightWall;
    public static float topBottom;

    // Start is called before the first frame update
    void Start()
    {

        borders = new Dictionary<string, float>();
        spawnWalls();
        leftRightWall = borders["leftRight"];
        topBottom = borders["topBottom"];

        points = GameObject.Find("points").GetComponent<TextMeshProUGUI>();
        numberPoints = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.fixedTime % 2 == 0)
        {
            time += 1;
            numberPoints += 10;
            points.text = "Points: " + numberPoints;
        }



    }


    private void spawnWalls()
    {

        //TODO: automatic adjust the length of walls
        if (bordersList.Length == 3)
        {
            borders.Add("leftRight", bordersList[0]);
            borders.Add("topBottom", bordersList[1]);
            borders.Add("length", bordersList[2]);

        }

        foreach (KeyValuePair<string, float> border in borders)
        {

            if (border.Key == "leftRight")
            {
                GameObject.Instantiate(wall, new Vector3(-border.Value, 0, 0), wall.transform.rotation = new Quaternion(0, 0, 90, 90)).transform.localScale = new Vector3(borders["length"] + 0.5f, wall.transform.localScale.y, 0);
                GameObject.Instantiate(wall, new Vector3(border.Value, 0, 0), wall.transform.rotation = new Quaternion(0, 0, 90, 90)).transform.localScale = new Vector3(borders["length"] + 0.5f, wall.transform.localScale.y, 0);
                
            }
            if (border.Key == "topBottom")
            {
                GameObject.Instantiate(wall, new Vector3(0, -border.Value, 0), Quaternion.identity).transform.localScale = new Vector3(borders["length"], wall.transform.localScale.y, 0);
                GameObject.Instantiate(wall, new Vector3(0, border.Value, 0), Quaternion.identity).transform.localScale = new Vector3(borders["length"], wall.transform.localScale.y, 0);
            }
        }

    }
    
}
