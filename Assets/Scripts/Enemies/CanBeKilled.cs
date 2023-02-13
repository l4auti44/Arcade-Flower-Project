using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CanBeKilled : MonoBehaviour
{
    private SpriteRenderer UI_Cursor;
    private void Start()
    {
        UI_Cursor = getSpriteRenderer("watchout_UI_cursor");
        UI_Cursor.enabled = false;
        
    }


    private void OnMouseDown()
    {
        if (HaveEnoughtPellets()) 
        {
            
            if (transform.parent.parent != null)
            {
                Destroy(transform.parent.parent.gameObject);
            }
            else if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }


        }
        
        
    }

    private bool HaveEnoughtPellets()
    {
        var _healt = gameObject.GetComponentInParent<Enemy>().health;

        if (GameManager.pellets >= _healt)
        {
           
            GameObject.Find("GameController").GetComponent<GameManager>().usePellets(_healt);
            return true;
        }
        else
        {
            Debug.Log("Not enougth pellets");
        }
        return false;
        
    }

    private void OnMouseEnter()
    {
        UI_Cursor.enabled = true;
    }
    private void OnMouseExit()
    {
        UI_Cursor.enabled = false;
    }


    private SpriteRenderer getSpriteRenderer(String name)
    {
        SpriteRenderer[] childrens = this.GetComponentsInChildren<SpriteRenderer>();
        foreach (var child in childrens)
        {
            if (child.name == name)
            {
                return child;
            }

        }
        return null;
    }
}
