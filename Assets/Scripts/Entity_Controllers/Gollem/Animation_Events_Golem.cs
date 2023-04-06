using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
public class Animation_Events_Golem : MonoBehaviour
{
    // Start is called before the first frame update
    Animator gAnimations;
    GameManager gameManager;
    Golem golem;

    void Start()
    {
        gAnimations = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        golem = this.transform.parent.GetComponent<Golem_Controller>().golem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RageEnd()
    {
        Debug.Log("Rage");
        //gAnimations.SetBool("isAware", true);
        golem.isRoaring = false;
        gAnimations.SetBool("idle", true);
    }

    public void AttackEnd()
    {
        Debug.Log("Attack");
        gameManager.DamagePlayer(golem.attackDMG, golem);
    }
}
