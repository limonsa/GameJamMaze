using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class MainScreenButtons : MonoBehaviour
{ 
    GameManager gameManager;

    public Button StartGame;
    public Button Settings;
    //public Button Credits;
    public Button End;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartGame.onClick.AddListener(gameManager.StartGame);
        End.onClick.AddListener(gameManager.Quit);
        //Credits.onClick.AddListener(gameManager.MainMenu);
    }

}


