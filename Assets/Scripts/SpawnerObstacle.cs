using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObstacle : MonoBehaviour
{
    public GameObject obstaclePrefabs;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        print(obstaclePrefabs.transform.position.x);    
    }

    void SpawnObstacle()
    {
        if(playerControllerScript.isGameOver == false) {
            Instantiate(obstaclePrefabs, spawnPos, obstaclePrefabs.transform.rotation);
        }
    }
}
