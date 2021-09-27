using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public Vector3 spawnPoint;
    public int maxX = 10;
    public int minX = 0;
    public int maxZ = 10;
    public int minZ = 0;
    public int timeTilNextSpawn = 5;
    int x = 0;
    int z = 0;
    float timer = 0;

    void Start()
    {
        timer = 0;
        spawnPoint.x = x;
        spawnPoint.z = z;

        if (minX == 0)
        {
            minX = -maxX;
        }

        if (minZ == 0)
        {
            minZ = -maxZ;
        }
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        Spawn();
    }

    void Spawn()
    {
        if (GameController._bossIsAlive)
        {
            if (timer >= timeTilNextSpawn)
            {
                x = Random.Range(minX, maxX);
                z = Random.Range(minZ, maxZ);
                spawnPoint.x = x;
                spawnPoint.z = z;
                Instantiate(spawnObject, spawnPoint, Quaternion.identity);
                Debug.Log("Spawn Object");
                timer = 0;
            }
        }
    }
}
