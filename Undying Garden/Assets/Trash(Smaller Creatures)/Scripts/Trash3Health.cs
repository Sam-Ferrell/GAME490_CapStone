using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash3Health : MonoBehaviour
{
    public GameObject Trash3;
    public static float health = 100f;

    public int Trash3ID;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (Trash3ID == 1)
            {
                Invoke(nameof(canBeHarvested), 3f);
            }

            if (Trash3ID == 2)
            {
                player.GetComponent<StarterAssets.ThirdPersonController>().TrapperPlant();
                player.GetComponent<StarterAssets.ThirdPersonController>().RestoreSpeed();
                Invoke(nameof(canBeHarvested), 3f);
            }
            //Destroy(Trash3);
        }

        if (player.GetComponent<StarterAssets.ThirdPersonController>().harvested == true && !stopHarvesting)
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

    public void takeDamageTrash3(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }

    public void harvestingWhat()
    {
        stopHarvesting = false;
        if (Trash3ID == 1)
        {
            SupplyCount.arrowCount += 5;
        }

        if (Trash3ID == 2)
        {
            SupplyCount.trapCount += 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (harvestable == true)
        {
            if (other.tag == "Player")
            {
                Debug.Log("Player in range");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    player.GetComponent<StarterAssets.ThirdPersonController>().Harvesting();
                }
            }
        }
    }
}
