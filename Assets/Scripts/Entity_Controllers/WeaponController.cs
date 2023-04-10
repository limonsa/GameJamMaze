using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponController : MonoBehaviour
{
    GameManager gameManager;
    BananaGun tempBananaGun; 
    Hammer tempHammer;
    Katana tempKatana;
    int weaponType = 0; // 1 = BananaGun, 2 = Hammer, 3 = Katana
    public bool rotate; // do you want it to rotate?
    public float rotationSpeed;
    public AudioClip collectSound;
    public GameObject collectEffect;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rotate = true;
        rotationSpeed = 5f;
        if (gameObject.name.Contains("BananaGun")) {
            tempBananaGun = new BananaGun();
            weaponType = 1;
        }else if (gameObject.name.Contains("Hammer")) {
            tempHammer = new Hammer();
            weaponType = 2;
        }
        else if (gameObject.name.Contains("Katana")) {
            tempKatana = new Katana();
            weaponType = 3;
        }
        //Debug.Log("START WeaponController for: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    /*
     * Adds health to the player 
     */
    private void getGift()
    {
        float extraForce = 0;
        // DamagePower is design to be a negative damage meant to heal the player
        //gameManager.HealPlayer(-tempLife.damagePower);
        Destroy(gameObject);
        if (collectSound)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        if (collectEffect)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }

        switch (weaponType) {
            case 1:
                extraForce = tempBananaGun.damagePower;
                break;
            case 2:
                extraForce = tempHammer.damagePower;
                break;
            case 3:
                extraForce = tempKatana.damagePower;
                break;
        }
        gameManager.boostAttackPower(extraForce);
    }

    /*
     * Destroys the object that this script is attached toRuns when 
     * an object collides with the object that this script is attached to
     */
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("COLLITION BETWEEN PLAYER AND WEAPON");
            getGift();
        }
    }
}

