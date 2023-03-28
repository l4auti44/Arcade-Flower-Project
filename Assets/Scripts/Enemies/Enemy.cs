using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    public float damage = 5f;
    public int health = 1;

    public bool killed = false;
    public int pointsForKill = 5;
    [SerializeField] private GameObject floatingPoints;
    void Start()
    {

    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (!collision.GetComponent<playerManager>().invincible)
            {
                collision.gameObject.GetComponent<playerManager>().takeDamage();
                collision.gameObject.GetComponent<Health>().decreaseHealth(damage);
            }

        }
    }

    protected virtual SpriteRenderer getSpriteRenderer(String name)
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

    protected virtual BoxCollider2D getBoxCollider2d(String name)
    {
        BoxCollider2D[] childrens = this.GetComponentsInChildren<BoxCollider2D>();
        foreach (var child in childrens)
        {
            if (child.name == name)
            {
                return child;
            }

        }
        return null;
    }

    public virtual bool Killed()
    {

        if (killed == false)
        {
            
            GameObject.Find("GameController").GetComponent<GameManager>().addPoints(pointsForKill);
            GameObject.Instantiate(floatingPoints, transform.position, Quaternion.identity, transform);
            killed = true;
            return false;
        }
        else
        {
            return true;
        }

    }
}
