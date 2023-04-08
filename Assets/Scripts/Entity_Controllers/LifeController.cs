using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

[RequireComponent(typeof(AudioSource))]
public class LifeController : MonoBehaviour
{
    GameManager gameManager;
    Life tempLife;
    public bool rotate; // do you want it to rotate?
    public float rotationSpeed;
    public AudioClip collectSound;
    public GameObject collectEffect;

    // Start is called before the first frame update
    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rotate = true;
        rotationSpeed = 5f;
        tempLife = new Life();
        tempLife.SetGeneralValues();

    }

    // Update is called once per frame
    void Update() {
        if (rotate)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    /*
     * Adds health to the player 
     */
    private void getGift() {
                // DamagePower is design to be a negative damage meant to heal the player
        gameManager.HealPlayer(-tempLife.damagePower);
        Destroy(gameObject);
        if (collectSound)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        if (collectEffect)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
    }

    /*
     * Destroys the object that this script is attached toRuns when 
     * an object collides with the object that this script is attached to
     */
    private void OnTriggerEnter(Collider other) { 
        if (other.gameObject.name == "Player") {
            //Debug.Log("COLLITION BETWEEN PLAYER AND LIFE DETECTED");
            getGift();
        }
    }
}
