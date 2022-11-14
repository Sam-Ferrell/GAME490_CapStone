using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash3Health : MonoBehaviour
{
    public GameObject Trash3;
    public static float health = 100f;

    public static bool dead = false;

    public int Trash3ID = 2;

    public Collider harvestCollider;
    public bool harvestable = false;
    private bool stopHarvesting = false;

    private Transform player;

    private Animator animator;
    public GameObject Crinoid;

    // Start is called before the first frame update
    void Start()
    {
        harvestCollider = GetComponent<SphereCollider>();
        harvestCollider.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Trash3ID = 2;
        dead = false;
        health = 100f;
        animator = Crinoid.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !dead)
        {
            animator.SetTrigger("Dead");
            if (Trash3ID == 1)
            {
                Invoke(nameof(canBeHarvested), 3f);
            }

            if (Trash3ID == 2)
            {
                Invoke(nameof(canBeHarvested), 2f);
            }
            dead = !dead;
            //Destroy(Trash3);
        }

        if (player.GetComponent<StarterAssets.ThirdPersonController>().harvested3 == true && !stopHarvesting)
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
        if (Trash3ID == 1)
        {
            SupplyCount.arrowCount += 5;
        }

        if (Trash3ID == 2)
        {
            SupplyCount.trapCount += 1;
        }
        spawnAgain();
        stopHarvesting = false;
        Destroy(Trash3);
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
                    player.GetComponent<StarterAssets.ThirdPersonController>().Harvesting3();
                }
            }
        }
    }

    private void spawnAgain()
    {
        TrashSpawner.trashCanSpawn3 = true;
    }
}
