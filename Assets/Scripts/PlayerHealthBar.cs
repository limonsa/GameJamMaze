using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using TMPro;
public class PlayerHealthBar : MonoBehaviour
{
    GameObject holder;
    GameManager gameManager;
    Player player;
    Vector2 healthBarSize;
    float maxHealth;

    public TMP_Text health;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        holder = this.transform.parent.gameObject;
        player = gameManager.player;
        healthBarSize = this.GetComponent<RectTransform>().rect.size;
        maxHealth = 100;
    }



    // Update is called once per frame
    void Update()
    {
        health.text = gameManager.player.health + "/" + maxHealth;
    }
}
