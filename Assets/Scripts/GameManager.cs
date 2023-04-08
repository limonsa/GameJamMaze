using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Entities;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isPaused = false;

    public List<GameObject> EnemyList = null;
    public Player player;
    public GameObject playerObject = null;
    
    private float weaponAddedForce = 0; //Force added to the attack after findng a Secret weapon


    void Awake()
    {
        DontDestroyOnLoad(this);
        PlayerCreation();
        FindAllEnemies();

       /* if (playerObject != null)
        {
            if (playerObject.transform.childCount > 0)
            {
                if (playerObject.transform.GetChild(0).GetComponent<Camera>())
                {
                    mainCamera = playerObject.transform.GetChild(0).GetComponent<Camera>();
                }
                else
                {
                    Debug.LogWarning("Camera not attached to player Object as the First Child");
                }
            }
            else
            {
                Debug.LogWarning("Camera not attached to player Object as the First Child");
            }
        }*/

    }

    private void FindAllEnemies()
    {
        EnemyList = new List<GameObject>();
        EnemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    private void PlayerCreation()
    {
        Debug.Log("Player Spawn");
        player = new Entities.Player();
        player.SetGeneralValues();
        player.position = playerObject.transform.position;
    }

    public Vector3 DetectPlayer(Transform enemyTransform, float enemyRange, Animator eAnimations)
    {
        if (player.isAlive)
        {
            float distance = Vector3.Distance(player.position, enemyTransform.position);
            if (distance < enemyRange)
            {
                eAnimations.SetBool("noticePlayer", true);
                return player.position;
            }
            else
                return Vector3.zero;
        }
        else
            return Vector3.zero;
    }

    public void DamagePlayer(float enemyDamage, Entity entity)
    {
        if (player.health > 0)
        {
            player.health -= (enemyDamage + weaponAddedForce);
            Debug.Log(player.health);
        }
        if(player.health <= 0)
        {
            player.health = 0;
            player.isAlive = false;
            KillPlayer(entity);
        }
    }

    /*
    public void DamageEnemy()
    {
        if (player.health > 0)
        {
            player.health -= (enemyDamage + weaponAddedForce);
            Debug.Log(player.health);
        }
        if (player.health <= 0)
        {
            player.health = 0;
            player.isAlive = false;
            KillPlayer(entity);
        }
    }
    */

    /*
     * The health of the player can get a greater value when
     * the player collides with a Secret Life Entity
     */
    public void HealPlayer(float healingValue)
    {
        if (player.isAlive) {
            player.health += healingValue;
            //Debug.Log("PLAYER HEALED by " + healingValue + ": New healt = " + player.health);
        }
    }

    /*
     * Gives to the player's attack the added force of the last secret weapon 
     * found by the player
     */
    public void boostAttackPower(float f) {
        weaponAddedForce = f;
    }

    public void MoveEntity(Animator eAnimations, Entity entity, GameObject eObject)
    {
        eObject.transform.Translate(Vector3.forward * entity.moveSpeed * Time.deltaTime);
    }

    public void RotateEntity(Transform eObjectTransform, Vector3 distance, Entity entity, Animator eAnimations)
    {
        Vector3 direction = (distance - eObjectTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        if(entity.name == "Golem")
        {
            //Debug.Log(direction);
            //Debug.Log(lookRotation.eulerAngles);
        }

        RaycastHit hit;
        if (Physics.Raycast(eObjectTransform.position + new Vector3(0, 1, 0), lookRotation*Vector3.forward, out hit, entity.noticeSphere))
        {
            if (hit.transform.tag == "Player" && entity.isRoaring == false)
            {
                eAnimations.SetBool("isAware", true);
                //Debug.Log(entity);
            }
            else
            {
                Debug.DrawRay(eObjectTransform.position + new Vector3(0, 1, 0), lookRotation * Vector3.forward * entity.noticeSphere, Color.red);
                eAnimations.SetBool("isAware", false);

            }
        }
        else
        {
            Debug.DrawRay(eObjectTransform.position + new Vector3(0, 1, 0), lookRotation * Vector3.forward * entity.noticeSphere, Color.white);
            eAnimations.SetBool("isAware", false);
        }

        eObjectTransform.rotation = Quaternion.Slerp(eObjectTransform.rotation, lookRotation, Time.deltaTime * 5f * entity.moveSpeed);

    }   

    public void SetEntityIdle(Animator eAnimations, Entity entity, GameObject eObject)
    {
        Debug.Log("Setting  " + entity.name + "  Idle");
        foreach (AnimatorControllerParameter parameter in eAnimations.parameters)
        {
           eAnimations.SetBool(parameter.name, false);
        }
        eAnimations.SetBool("idle", true);
    }

    private void KillPlayer(Entity entity)
    {
       
       // Destroy(playerObject);
        entity.canAttack = false;
    }
    // Update is called once per frame

    public float mainThreadTime = 0;

    void Update()
    {
        mainThreadTime += Time.deltaTime;
        //player.position = playerObject.transform.position;
    }



    public void ShowTimeOfRun()
    {

    }

    public void RestartLevel()
    {

    }

    private void FixedUpdate()
    {
       
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Application");
    }
}
