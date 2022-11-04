using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHealth : MonoBehaviour
{
    public GameObject trash;
    public static float health = 100f;

    public int trashID;

    public Collider harvestCollider;
    public bool harvestable = false;

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
        if(health <= 0)
        {
            if(trashID == 1)
            {
                Invoke(nameof(canBeHarvested), 3f);
            }

            if(trashID == 2)
            {
                player.GetComponent<StarterAssets.ThirdPersonController>().TrapperPlant();
                player.GetComponent<StarterAssets.ThirdPersonController>().RestoreSpeed();
                Invoke(nameof(canBeHarvested), 3f);
            }
            //Destroy(trash);
        }
    }

    private void canBeHarvested()
    {
        harvestable = true;
        harvestCollider.enabled = true;
    }

    public void takeDamageTrash(float damage)
    {
            health -= damage;
    }

    private void OnTriggerStay(Collider other)
    {
        if(harvestable == true)
        {
            if(other.tag == "Player")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    player.GetComponent<StarterAssets.ThirdPersonController>().Harvesting();
                }
            }
        }
    }
}
