using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    GameManager gameManager;
    public Transform cameraPosition;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(!gameManager.isPaused)
        this.transform.position = this.transform.parent.transform.position;   
    }
}
