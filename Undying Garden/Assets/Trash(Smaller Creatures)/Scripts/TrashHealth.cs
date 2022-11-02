using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHealth : MonoBehaviour
{
    public GameObject trash;
    public static float health = 100f;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            player.GetComponent<StarterAssets.ThirdPersonController>().RestoreSpeed();
            //Destroy(trash);
        }
    }

    public void takeDamageTrash(float damage)
    {
            health -= damage;
    }
}
