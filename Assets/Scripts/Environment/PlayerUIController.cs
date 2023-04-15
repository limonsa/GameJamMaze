using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    GameManager gameManager;
    TMP_Text timerText;
    GameObject gameObjectAttack;
    Katana animKatana;
    Hammer animHammer;
    BananaGun animBananaGun;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowTimeOfRun(gameManager.mainThreadTime);
    }

    public void ShowTimeOfRun(float _mainThreadTime)
    {
        TMP_Text timerText = GameObject.Find("Time").GetComponent<TMP_Text>();
        TimeSpan time = TimeSpan.FromSeconds(_mainThreadTime);
        GameObject.Find("Time").GetComponent<TMP_Text>().text = time.ToString(@"mm\:ss"); // 03:48

    }

    public void ObtainWeaponX(string weaponName)
    {
        Button btn = GameObject.Find("Button" + weaponName).GetComponent<Button>();
        btn.enabled = true;
        btn.image.overrideSprite = btn.spriteState.disabledSprite;
    }

    public bool UseWeaponX(string weaponName) {
        Button btn = GameObject.Find("Button" + weaponName).GetComponent<Button>();
        //gameObjectAttack = gameManager.weaponController.getPrefab(weaponName);
        Transform pos = GameObject.Find("Player").GetComponent<Transform>();
        Vector3 playerPos = pos.position;
        Vector3 playerDirection = pos.forward;
        Quaternion playerRotation = pos.rotation;
        float spawnDistance = 2;

        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
        bool rta = false;
        if (btn.enabled) {
            btn.enabled = false;
            btn.image.overrideSprite = btn.spriteState.disabledSprite;
            //Instantiate(gameObjectAttack, spawnPos, playerRotation);
            if(weaponName == "Hammer") {
                GameObject.Find("HammerAttackAnim").GetComponent<Transform>().position = spawnPos;
            }else if(weaponName == "Katana") {
                GameObject.Find("KatanaAttackAnim").GetComponent<Transform>().position = spawnPos;
            }
            else if(weaponName == "BananaGun") {
                GameObject.Find("BananaGunAttackAnim").GetComponent<Transform>().position = spawnPos;
            }

            rta = true;
        }
        return rta;
    }

    public bool UseBananaGun()
    {
        return UseWeaponX("BananaGun");
    }

    public bool UseKatana()    {
        return UseWeaponX("Katana");
    }

    public bool UseHammer()
    {
        return UseWeaponX("Hammer");
    }

    public void StopUseWeaponX(string weaponName)
    {
        Vector3 spawnPos = new Vector3(0, 0, 0);
        if (weaponName == "Hammer")
        {
            GameObject.Find("HammerAttackAnim").GetComponent<Transform>().position = spawnPos;
        }
        else if (weaponName == "Katana")
        {
            GameObject.Find("KatanaAttackAnim").GetComponent<Transform>().position = spawnPos;
        }
        else if (weaponName == "BananaGun")
        {
            GameObject.Find("BananaGunAttackAnim").GetComponent<Transform>().position = spawnPos;
        }

    }
}
