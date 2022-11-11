using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHitbox : MonoBehaviour
{
    public GameObject Hitbox;
    public GameObject Targettrash;

    private bool trashHealth;

    // Start is called before the first frame update
    void Start()
    {
        trashHealth = GetComponent<TrashHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Targettrash.transform.position, 20f * Time.deltaTime);

        if (TrashHealth.health <= 0)
        {
            Destroy(Hitbox);
        }
    }
}
