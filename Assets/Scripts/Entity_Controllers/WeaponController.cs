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
    public GameObject prefabKatana;
    public GameObject prefabHammer;
    public GameObject prefabBananaGun;

    public AudioClip collectSound;
    public GameObject collectEffect;

    [Header("KeyBinds")]
    public KeyCode katanaKey = KeyCode.R;      // 'R' key to grab the katana weapon
    public KeyCode hammerKey = KeyCode.T;      // 'T' key to grab the hammer weapon
    public KeyCode bananaGunKey = KeyCode.Y;   // 'Y' key to grab the BananaGun weapon
    public KeyCode weaponAttack = KeyCode.Mouse0;   // With left mouse button the attack is made with the grabed weapon


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        prefabKatana = Resources.Load("Secrets/Katana") as GameObject;
        prefabHammer = Resources.Load("Secrets/Hammer") as GameObject;
        prefabBananaGun = Resources.Load("Secrets/BananGun") as GameObject;

        rotate = true;
        rotationSpeed = 10f;
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
        if (rotate) {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.R)) {
            gameManager.playerUIController.UseKatana();

            /*if (Input.GetKey(weaponAttack)) {
                gameManager.playerUIController.ShowAttack();
                Debug.Log("SHOW KATANA ATTACK");
            }*/
        }else if (Input.GetKey(KeyCode.T)) {
            gameManager.playerUIController.UseHammer();
            
        }else if (Input.GetKey(KeyCode.Y)){
            gameManager.playerUIController.UseBananaGun();
            
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            gameManager.playerUIController.StopUseWeaponX("Katana");
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            gameManager.playerUIController.StopUseWeaponX("Hammer");
        }
        else if (Input.GetKeyUp(KeyCode.Y))
        {
            gameManager.playerUIController.StopUseWeaponX("BananaGun");

        }
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
                gameManager.playerUIController.ObtainWeaponX("BananaGun");
                break;
            case 2:
                extraForce = tempHammer.damagePower;
                gameManager.playerUIController.ObtainWeaponX("Hammer");
                break;
            case 3:
                extraForce = tempKatana.damagePower;
                gameManager.playerUIController.ObtainWeaponX("Katana");
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
        if (other.gameObject.name.Contains("Player"))
        {
            //Debug.Log("COLLITION BETWEEN PLAYER AND WEAPON");
            getGift();
        }else if (other.gameObject.tag == "Zombie") //public void DamageEnemy(Entity entity, GameObject entityObject)
        {
            Debug.Log("hit enemy");
            gameManager.DamageEnemy(other.gameObject.GetComponent<Zombie>(), other.gameObject);

        }
        else if (other.gameObject.tag == "Golem")
        {
            Debug.Log("hit enemy");
            gameManager.DamageEnemy(other.gameObject.GetComponent<Golem>(), other.gameObject);
            // example
            //gameManager.DamageEnemy(other.gameObject.GetComponent<GolemContoller>().golem, other.gameObject);
        }
    }

    public GameObject getPrefab(string _name)
    {
        GameObject gob = null;
        switch (_name)
        {
            case "Katana":
                gob = prefabKatana;
                break;
            case "Hammer":
                gob = prefabHammer;
                break;
            case "BananGun":
                gob = prefabBananaGun;
                break;
        }
        return gob;
    }
}

