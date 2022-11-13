using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlacer : MonoBehaviour
{
    private Transform player;
    public GameObject trapSpawnLocation;
    public GameObject trap;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(SupplyCount.trapCount > 0)
            {
                player.GetComponent<StarterAssets.ThirdPersonController>().PlaceTrap();
            }
        }

        if(player.GetComponent<StarterAssets.ThirdPersonController>().trapSpawn == true)
        {
            SpawnTrap();
            player.GetComponent<StarterAssets.ThirdPersonController>().trapSpawn = false;
        }
    }

    public void SpawnTrap()
    {
        GameObject spawnChoice = trapSpawnLocation;
        Transform spawnPoint = spawnChoice.transform;
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(trap, spawnPoint.position, spawnRotation);
    }
}
