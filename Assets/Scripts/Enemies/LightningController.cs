using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{

    [SerializeField] private Animator animator;

    public void setTriggers(string name)
    {
        animator.SetTrigger(name);
        Debug.Log("Triggering: " + name);
    }
}
