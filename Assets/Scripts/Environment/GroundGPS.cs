using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GroundGPS : MonoBehaviour
{
    private MeshRenderer mr;
    private Vector3 size;
    private List<Vector3> srFixPos; //List of the positions of the Secrets in created in fixed positions
    
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        size = mr.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log("JumpDetecte");
        }
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

    public void createFixedPositionedSecrets()
    {
        float y = 2.1f;
        srFixPos = new List<Vector3>();
        srFixPos.Add(new Vector3(545, y, 434)); //Secret room 1
        srFixPos.Add(new Vector3(571, y, 472)); //Secret room 2 (rolling ball)
        srFixPos.Add(new Vector3(538, y, 536)); //Secret room 3 (fake wall entrance)
        srFixPos.Add(new Vector3(525, y, 556)); //Secret room 4
        srFixPos.Add(new Vector3(553, y, 557)); //Secret room 5 (lava floor entrance)
        srFixPos.Add(new Vector3(540, y, 578)); //Secret room 6
        srFixPos.Add(new Vector3(490, y, 542)); //Secret room 7
        srFixPos.Add(new Vector3(485, y, 570)); //Secret room 8
        srFixPos.Add(new Vector3(391, y, 590)); //Secret room 9 (circular maze's corner)
        srFixPos.Add(new Vector3(445, y, 476)); //Secret room 10 (waterfall)
        srFixPos.Add(new Vector3(507, y, 430)); //Secret room 11
    }

    public List<Vector3> getFixedSecretsPositions()
    {
        return srFixPos;
    }
}
