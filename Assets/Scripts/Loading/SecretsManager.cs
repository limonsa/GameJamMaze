//using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class SecretsManager : MonoBehaviour
{
    GameManager gameManager;
    GroundGPS gps;
    private List<GameObject> availableSecrets = null;
    public List<GameObject> secretsList = null;

    //Animator zAnimations; //must be the animation of the secret

    // Start is called before the first frame update
    void Awake(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gps = GameObject.Find("Ground").GetComponent<GroundGPS>();
        availableSecrets = new List<GameObject>(Resources.LoadAll<GameObject>("Secrets"));
        //Debug.Log("FIRST SECRET ARE AVAILABLE? ... >>>>>>>>>>>>>>>>>>>>>>>>>> " + availableSecrets.Count);
        for(int i=0; i < availableSecrets.Count; i++) {
            /*int randomx = Random.Range(-10, 10);
            int randomz = Random.Range(, 25);*/
            Instantiate(availableSecrets[i], gps.getRandomPosition() , Quaternion.identity);
            //Instantiate(availableSecrets[i], new Vector3(578, 2.1f, 409), Quaternion.identity);
        }
        

        //secretsList = new List<GameObject>();

        //secretsList.Add(new BananaGun());

        //zombie.SetGeneralValues();
        /*
        spawnWeapons();
        spawnMemes();
        spawnLives();
        //zombieAlert = this.GetComponent<SphereCollider>();

        zombie = new Zombie();
        zombie.SetGeneralValues();
        // zombieAlert.radius = zombie.noticeSphere;
        zombie.isRoaring = false;*/

    }

    private void spawnWeapons() {
        Vector3 randomSpawnPosition = new Vector3(578, 2.1f , 409);
        Instantiate(secretsList[0], randomSpawnPosition, Quaternion.identity);

    }

    private void spawnMemes()
    {
    }

    private void spawnLives()
    {
    }

    private void FindAllEnemies(){
        secretsList = new List<GameObject>();
        secretsList.AddRange(GameObject.FindGameObjectsWithTag("Secret"));
    }

}

