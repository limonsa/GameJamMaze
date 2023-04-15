using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Entities;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isPaused = false;

    public List<GameObject> EnemyList = null;
    public Player player;


    public GameObject playerObject = null;
    public PlayerUIController playerUIController = null;
    public WeaponController weaponController = null;
    public float mainThreadTime = 0;

    public GameObject endGameMenu = null;
    public GameObject timer = null;
    private GameObject pauseMenu = null;

    private float weaponAddedForce = 0; //Force added to the attack after findng a Secret weapon

    private void Start()
    {
        playerUIController = GameObject.Find("PlayerUI").GetComponent<PlayerUIController>();
        weaponController = GetComponent<WeaponController>();
        DontDestroyOnLoad(this);
        PlayerCreation();
        FindAllEnemies();
    }

    void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
        {
            Destroy(this.gameObject);
        }

        LoadObjects();
        SceneManager.sceneLoaded += OnLoad;
        DontDestroyOnLoad(this);
        PlayerCreation();
        //FindAllEnemies();

    }

    private void LoadObjects()
    {
        mainThreadTime = 0;
        if (playerObject == null)
        {
            if (GameObject.Find("Player"))
            {
                playerObject = GameObject.Find("Player");
            }else { 
                Debug.Log("No Plyaer Present in this scene");
            }
        }

        if (timer == null)
        {
            if (GameObject.Find("Time"))
                timer = GameObject.Find("Time");
            else
                Debug.Log("No Timer in Area");
        }

        if (endGameMenu == null)
        {
            if (GameObject.Find("EndGameMenu"))
                endGameMenu = GameObject.Find("EndGameMenu").transform.GetChild(0).gameObject;
            else
                Debug.Log("No EndGameMenu in Area");
        }
        if (pauseMenu == null)
        {
            if (GameObject.Find("PauseGameMenu"))
                pauseMenu = GameObject.Find("PauseGameMenu").transform.GetChild(0).gameObject;
            else
                Debug.Log("No Pause Game Menu in Area");
        }
    }

    public void OnLoad(Scene scene, LoadSceneMode mode)
    {
        LoadObjects();
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
        if (playerObject != null)
        {
            player.position = playerObject.transform.position;
        }
    }

    public Vector3 DetectPlayer(Transform enemyTransform, Entity entity, Animator eAnimations)
    {
        if (player.isAlive)
        {
            float distance = Vector3.Distance(player.position, enemyTransform.position);
            if (distance < entity.noticeSphere)
            {
                eAnimations.SetBool("noticePlayer", true);
                return player.position;
            }
            else
            {
                if (!eAnimations.GetBool("idle"))
                {
                    SetIdle();
                }
                else if(eAnimations.GetBool("noticePlayer"))
                    SetIdle();
                return Vector3.zero;
            }
        }
        else
        {
            if(!eAnimations.GetBool("idle"))
            {
                SetIdle();
            }
            return Vector3.zero;
        }

        void SetIdle()
        {
            SetEntityIdle(eAnimations, entity, enemyTransform.gameObject);
        }
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

    
    public void DamageEnemy(Entity entity, GameObject entityObject)
    {
        if (entity.health > 0)
        {
            entity.health -= (player.attackDMG + weaponAddedForce);
            Debug.Log(entity.health);
        }
        if (entity.health <= 0)
        {
            entity.health = 0;
            entity.isAlive = false;
            Debug.Log(entity + "has been destroyed");
            Destroy(entityObject);
        }
    }
    

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
                
            }
            else
            {
                Debug.DrawRay(eObjectTransform.position + new Vector3(0, 1, 0), lookRotation * Vector3.forward * entity.noticeSphere, Color.red);
                eAnimations.SetBool("isAware", false);
                Debug.Log(hit.transform.tag);

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
        isPaused = true;
        EndGameMenu("You have Died");
        
    }
    // Update is called once per frame
    void Update()
    {
        

        if (!isPaused)
        {
            mainThreadTime += Time.deltaTime;
            if (playerObject != null)
            {
                player.position = playerObject.transform.position;
                ShowTimeOfRun();
                //playerUIController.ShowTimeOfRun(mainThreadTime);
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseMenu();
        }

    }

    public void PauseMenu()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }
    public void ShowTimeOfRun()
    {
        string timeString = mainThreadTime.ToString() + "0000";
        timer.GetComponent<TextMeshProUGUI>().text = timeString.Substring(0,4);
    }

    public void RestartLevel()
    {
        isPaused = false;
        PlayerCreation();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGameMenu(string input)
    {
        endGameMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = input;
        isPaused = true;
        endGameMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void ResumeGame()
    {
        Debug.Log("Resume");
        isPaused = false;
        pauseMenu.SetActive(false); 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void StartGame()
    {
        isPaused = false;
        SceneManager.LoadScene("Maze");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Application");
    }
}
