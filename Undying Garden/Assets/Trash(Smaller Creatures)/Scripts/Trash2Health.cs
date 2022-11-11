using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash2Health : MonoBehaviour
{
    public GameObject Trash2;
    public static float health = 100f;

    public static bool dead = false;

    public int Trash2ID = 1;

    public Collider harvestCollider;
    public bool harvestable = false;
    private bool stopHarvesting = false;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        harvestCollider = GetComponent<SphereCollider>();
        harvestCollider.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Trash2ID = 1;
        dead = false;
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !dead)
        {
            if (Trash2ID == 1)
            {
                Invoke(nameof(canBeHarvested), 3f);
            }

            if (Trash2ID == 2)
            {
                player.GetComponent<StarterAssets.ThirdPersonController>().TrapperPlant();
                player.GetComponent<StarterAssets.ThirdPersonController>().RestoreSpeed();
                Invoke(nameof(canBeHarvested), 3f);
            }
            dead = !dead;
            //Destroy(Trash2);
        }

        if (player.GetComponent<StarterAssets.ThirdPersonController>().harvested2 == true && !stopHarvesting)
        {
            Invoke(nameof(harvestingWhat), 3f);
            stopHarvesting = true;
        }
    }

    private void canBeHarvested()
    {
        harvestable = true;
        harvestCollider.enabled = true;
    }

    public void takeDamageTrash2(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }

    public void harvestingWhat()
    {

        if (Trash2ID == 1)
        {
            SupplyCount.arrowCount += 5;
        }

        if (Trash2ID == 2)
        {
            SupplyCount.trapCount += 1;
        }
        spawnAgain();
        stopHarvesting = false;
        Destroy(Trash2);
    }

    private void OnTriggerStay(Collider other)
    {
        if (harvestable == true)
        {
            if (other.tag == "Player")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    player.GetComponent<StarterAssets.ThirdPersonController>().Harvesting2();
                }
            }
        }
    }

    private void spawnAgain()
    {
        TrashSpawner.trashCanSpawn2 = true;
    }
}
