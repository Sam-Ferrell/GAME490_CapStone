using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapperPlant : MonoBehaviour
{
    private Transform trapperPlant;
    private Transform player;

    public float pursueRange = 3f;

    private bool stopThat = false;

    private Animator animator;
    public GameObject Crinoid;

    private Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        animator = Crinoid.GetComponent<Animator>();
        trapperPlant = this.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var playerDistance = trapperPlant.position - player.position;

        if(playerDistance.sqrMagnitude <= pursueRange * pursueRange && Trash3Health.dead == false)
        {
            transform.LookAt(targetPlayer);
            animator.SetTrigger("Grabbing");
            player.GetComponent<StarterAssets.ThirdPersonController>().Trapped();
            player.GetComponent<StarterAssets.ThirdPersonController>().TrapperPlant();
        }

        if(Trash3Health.dead == true && stopThat == false)
        {
            player.GetComponent<StarterAssets.ThirdPersonController>().RestoreSpeed();
            player.GetComponent<StarterAssets.ThirdPersonController>().TrapperPlant();
            stopThat = true;
        }
    }
}
