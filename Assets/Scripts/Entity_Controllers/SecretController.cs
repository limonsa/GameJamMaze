using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        getGift();
    }

    // Update is called once per frame
    void Update(){
        
    }


    private void getGift() {
        Debug.Log("This Secret Type is: " + this.GetType());
    }

    /*
     * Destroys the object that this script is attached toRuns when 
     * an object collides with the object that this script is attached to
     */
    private void OnCollisionEnter(Collision other) {
        if (gameObject.name.Contains("Secret")){
            getGift();
            Destroy(this.gameObject);
        }
    }

}
