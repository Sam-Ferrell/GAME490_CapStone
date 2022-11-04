using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapperPlant : MonoBehaviour
{
    private Transform trapperPlant;
    private Transform player;

    public float pursueRange = 10f;

    // Start is called before the first frame update
    void Start()
    {
        trapperPlant = this.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var playerDistance = trapperPlant.position - player.position;

        if(playerDistance.sqrMagnitude <= pursueRange * pursueRange)
        {
            player.GetComponent<StarterAssets.ThirdPersonController>().Trapped();
            player.GetComponent<StarterAssets.ThirdPersonController>().TrapperPlant();
        }
    }
}
