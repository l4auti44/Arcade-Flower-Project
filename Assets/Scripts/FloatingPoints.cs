using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FloatingPoints : MonoBehaviour
{
    private TextMeshPro text;
    [SerializeField] private Color color;
    // Start is called before the first frame update
    void Start()
    {
        var points = 8;
        if (gameObject.GetComponentInParent<Enemy>() != null)
        {
            points = gameObject.GetComponentInParent<Enemy>().pointsForKill;
        }
        else
        {
            points = gameObject.GetComponentInParent<Breadbug>().pointsForKill;
        }
        
       
        text = gameObject.GetComponent<TextMeshPro>();
        //text.col = color;
        text.text = points.ToString();
        Destroy(gameObject, 1);
        transform.localPosition += new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
