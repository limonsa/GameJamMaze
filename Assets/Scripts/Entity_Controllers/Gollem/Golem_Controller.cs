using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class Golem_Controller : MonoBehaviour
{
    GameManager gameManager;
    Animator gAnimations;
    //SphereCollider zombieAlert;
    public Golem golem;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gAnimations = this.transform.GetChild(0).GetComponent<Animator>();
        //zombieAlert = this.GetComponent<SphereCollider>();

        golem = new Golem();
        golem.SetGeneralValues();
        // zombieAlert.radius = zombie.noticeSphere;


    }

    // Update is called once per frame
    void Update()
    {
            if (!golem.canAttack && !golem.canMove)
            {
                golem.canMove = true;
                gameManager.SetEntityIdle(gAnimations, golem, this.gameObject);
            }

            if (gameManager.player.isAlive == false)
            {
                gameManager.SetEntityIdle(gAnimations, golem, this.gameObject);
            }
            else if (gAnimations.GetBool("walk"))
            {
             //   this.transform.Translate(Vector3.forward * golem.moveSpeed * Time.deltaTime);
            }

        
    }

    private void FixedUpdate()
    {

        if (golem.canMove && !gameManager.isPaused)
        {
            golem.position = this.transform.position;
            Vector3 distance = gameManager.DetectPlayer(this.transform, golem, gAnimations);


            if (gAnimations.GetBool("noticePlayer"))
            {
                gameManager.RotateEntity(this.transform, distance, golem, gAnimations);
                
            }

            if(!gAnimations.GetBool("noticePlayer"))
            {
                golem.isRoaring = true;
            }


            if (gAnimations.GetBool("isAware") && golem.isRoaring == false)
            {
                gAnimations.SetBool("idle", false);
                gAnimations.SetBool("walk", true);
                gameManager.MoveEntity(gAnimations, golem, this.gameObject);
            }
            else
            {
                //golem.isRoaring = true;
                gAnimations.SetBool("idle", true);
                gAnimations.SetBool("walk", false);
            }

        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);

        if (collision.transform.tag == "Player")
        {
            Debug.Log("Golem Found Player");
            gAnimations.SetBool("walk", false);
            gAnimations.SetBool("attack", true);
            golem.canMove = false;
            golem.canAttack = true;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            gAnimations.SetBool("walk", true);
            gAnimations.SetBool("attack", false);
            golem.canMove = true;
            golem.canAttack = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {

    }

   


    public void AttackEnd()
    {
        gameManager.DamagePlayer(golem.attackDMG, golem);
    }

    
}
