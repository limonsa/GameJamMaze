using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EndGameButtons : MonoBehaviour
{
    public GameManager gameManager;

    public Button restart;
    public Button exit;
    public Button mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        restart.onClick.AddListener(gameManager.RestartLevel);
        exit.onClick.AddListener(gameManager.Quit);
        mainMenu.onClick.AddListener(gameManager.MainMenu);
    }



}
