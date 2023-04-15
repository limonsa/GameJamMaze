//using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.ProBuilder;

public class SecretsManager : MonoBehaviour
{
    GameManager gameManager;
    GroundGPS gps;
    private List<GameObject> availableSecrets = null;
    //public List<GameObject> secretsList = null;
    List<Vector3> secretRoomLocations = null;
    List<Vector3> memeLocations = null;

    // Start is called before the first frame update
    void Awake(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gps = GameObject.Find("Ground").GetComponent<GroundGPS>();
        availableSecrets = new List<GameObject>(Resources.LoadAll<GameObject>("Secrets"));

        spawnFixedSecrets();
        spawnMemes();

    }

    /*
     * Creates and place secrets inside the X secret rooms
     * alternating the secrets Entypes along the secret Entities
     */
    private void spawnFixedSecrets() {
        gps.createSecretsFixedPositions();
        secretRoomLocations = gps.getFixedSecretsPositions();
        for (int i = 0, j = 0; i < secretRoomLocations.Count; i++, j++)
        {
            Instantiate(availableSecrets[j], secretRoomLocations[i], Quaternion.identity);
            //Instantiate(availableSecrets[i], new Vector3(578, 2.1f, 409), Quaternion.identity);
            if (j == availableSecrets.Count - 1)
            {
                j = 0;
            }
        }

    }

    /*
     * Creates and place secrets inside the 4 memes
     * alternating the secrets Entypes along the secret Entities
     */
    private void spawnMemes()
    {
        int i = 0;
        List<GameObject> memes;
        gps.createMemesPositions();
        memeLocations = gps.getMemesPositions();
        memes = new List<GameObject>(Resources.LoadAll<GameObject>("Memes"));
        Instantiate(memes[0], memeLocations[i], Quaternion.Euler(0, 30, 0));
        i++;
        Instantiate(memes[0], memeLocations[i], Quaternion.Euler(0, 90, 0));
        i++;
        Instantiate(memes[0], memeLocations[i], Quaternion.Euler(0, 90, 0));
        i++;
        //Instantiate(memes[0], memeLocations[i], Quaternion.identity);
        Instantiate(memes[0], memeLocations[i], Quaternion.Euler(0, 90, 0));
        i++;

    }

    public List<GameObject> FindAllWeapons()
    {
        List<GameObject> temp = null;
        temp.AddRange(GameObject.FindGameObjectsWithTag("BananaGun"));
        temp.AddRange(GameObject.FindGameObjectsWithTag("Hammmer"));
        temp.AddRange(GameObject.FindGameObjectsWithTag("Katana"));
        return temp;

    }

    public GameObject[] FindAllLifes()
    {
        return GameObject.FindGameObjectsWithTag("Life");
    }

    public GameObject[] FindAllMemes()
    {
        return GameObject.FindGameObjectsWithTag("Meme");
    }

    public List<GameObject> FindAllSecrets(){
        List<GameObject> temp = null;
        temp = FindAllWeapons();
        temp.AddRange(FindAllLifes());
        temp.AddRange(FindAllMemes());
        return temp;
    }

}

