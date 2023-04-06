using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class Zombie_Controller : MonoBehaviour
{
    GameManager gameManager;
    Animator zAnimations;
    SphereCollider zombieAlert;
    public Zombie zombie;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        zAnimations = this.GetComponent<Animator>();
        //zombieAlert = this.GetComponent<SphereCollider>();

        zombie = new Zombie();
        zombie.SetGeneralValues();
       // zombieAlert.radius = zombie.noticeSphere;
       zombie.isRoaring = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isPaused == false)
        {
            

            if (!zombie.canAttack && !zombie.canMove)
            {
                zombie.canMove = true;
                zAnimations.SetBool("idle", true);
                zAnimations.SetBool("attack", false);
            }

            if (gameManager.player.isAlive == false)
            {
                gameManager.SetEntityIdle(zAnimations, zombie, this.gameObject);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            zAnimations.SetBool("walk", false);
            zAnimations.SetBool("attack", true);
            zombie.canMove = false;
            zombie.canAttack = true;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            zAnimations.SetBool("walk", true);
            zAnimations.SetBool("attack", false);
            zombie.canMove = true;
            zombie.canAttack = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {

    }

    public void FixedUpdate()
    {
        if (zombie.canMove)
        {
            zombie.position = this.transform.position;
            Vector3 distance = gameManager.DetectPlayer(this.transform, zombie.noticeSphere, zAnimations);

            if (zAnimations.GetBool("noticePlayer"))
            {
                //zAnimations.SetBool("isAware", true);
                gameManager.RotateEntity(this.transform, distance, zombie, zAnimations);
                //gAnimations.SetBool("walk", true);
            }

            if (zAnimations.GetBool("isAware"))
            {
                zAnimations.SetBool("idle", false);
                zAnimations.SetBool("walk", true);
                gameManager.MoveEntity(zAnimations, zombie, this.gameObject);
            }
            else
            {
                zAnimations.SetBool("idle", true);
                zAnimations.SetBool("walk", false);
            }

        }
    }
    public void AttackEnd()
    {
        gameManager.DamagePlayer(zombie.attackDMG, zombie);
    }
    
}
