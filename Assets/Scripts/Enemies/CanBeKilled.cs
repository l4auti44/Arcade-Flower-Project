using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CanBeKilled : MonoBehaviour
{
    private SpriteRenderer UI_Cursor;
    [SerializeField] private GameObject pinkman;
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
                Debug.Log("1o");
                GameObject.Instantiate(pinkman, this.transform);

                if (gameObject.name == "breadbug_spritesheet_0")
                {
                    transform.parent.GetComponent<Breadbug>().Killed();
                }

                //Destroy(transform.parent.parent.gameObject);
            }
            else if (transform.parent != null)
            {
                Debug.Log("2o");
                GameObject.Instantiate(pinkman, this.transform);
                // Destroy(transform.parent.gameObject);
            }
            else
            {

                Debug.Log("3o");
                GameObject.Instantiate(pinkman, this.transform);
                // Destroy(gameObject);
            }


        }
        
        
    }

    private bool HaveEnoughtPellets()
    {
        var _health = 1;
        if (gameObject.name != "breadbug_spritesheet_0")
        {
            _health = gameObject.GetComponentInParent<Enemy>().health;
        }

        

        if (GameManager.pellets >= _health)
        {
           
            GameObject.Find("GameController").GetComponent<GameManager>().usePellets(_health);
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
