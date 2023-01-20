using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float healtPoint;
    [SerializeField] private float maxHealthPoint = 5f;
    public float damage = 5f;



    void Start()
    {
        healtPoint = maxHealthPoint;
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
}
