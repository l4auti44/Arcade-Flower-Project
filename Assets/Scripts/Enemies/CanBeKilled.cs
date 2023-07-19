using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CanBeKilled : MonoBehaviour
{
    private SpriteRenderer UI_Cursor;
    [SerializeField] private GameObject pinkman;
    private bool killed;
    private int _health;

    private void Start()
    {
        UI_Cursor = getSpriteRenderer("watchout_UI_cursor");
        UI_Cursor.enabled = false;


    }


    private void OnMouseDown()
    {
        if (HaveEnoughtPellets())
        {
            if (gameObject.name == "breadbug_spritesheet_0")
            {
                killed = transform.parent.GetComponent<Breadbug>().Killed();

            }
            else if (gameObject.name == "Beetle")
            {
                killed = transform.parent.GetComponent<AnodesBeetles>().Killed();

            }
            else
            {
                
                killed = gameObject.GetComponentInParent<Enemy>().Killed();
            }




            if (!killed)
            {
                GameObject.Find("GameController").GetComponent<GameManager>().usePellets(_health);
                if (transform.parent.parent != null)
                {
                    GameObject.Instantiate(pinkman, this.transform);

                }

                else if (transform.parent != null)
                {

                    GameObject.Instantiate(pinkman, this.transform);

                }
                else
                {

                    GameObject.Instantiate(pinkman, this.transform);

                }
            }

        }
    }


    private bool HaveEnoughtPellets()
    {
        _health = 1;
        if (gameObject.name != "breadbug_spritesheet_0")
        {
            _health = gameObject.GetComponentInParent<Enemy>().health;
        }

        

        if (GameManager.pellets >= _health)
        {
            return true;
        }
        else
        {
            GameObject.Find("GameController").GetComponent<GameManager>().notEnoughtPelletAnim();
            GameObject.Find("Player").GetComponent<AudioManager>().PlaySound("NotEnoughtPellets");
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
