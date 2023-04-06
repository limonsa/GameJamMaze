using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGPS : MonoBehaviour
{
    private MeshRenderer mr;
    private Vector3 size;
    // Start is called before the first frame update
    void Start()
    {

        mr = GetComponent<MeshRenderer>();
        size = mr.bounds.size;
        Debug.Log("GROUNDS DIMENSIONS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> " + size.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getWidthX() {
        return size.x;
    }

    public float getWidthY()
    {
        return size.y;
    }

    public Vector3 getPosition() {
        return transform.position;
    }

    public Vector3 getRandomPosition() {
        int minPosX, maxPosX, minPosZ, maxPosZ;
        float randomx, randomz;
        minPosX = (int)Math.Round(transform.position.x - (size.x / 2));
        maxPosX = (int)Math.Round(transform.position.x + (size.x / 2));
        minPosZ = (int)Math.Round(transform.position.z - (size.z / 2));
        maxPosZ = (int)Math.Round(transform.position.z + (size.z / 2));
        randomx = UnityEngine.Random.Range(minPosX, maxPosX);
        randomz = UnityEngine.Random.Range(minPosZ, maxPosZ);

        return new Vector3(randomx, transform.position.y, randomz);
    }
}
