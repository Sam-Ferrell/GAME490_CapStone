using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHitbox3 : MonoBehaviour
{
    public GameObject Hitbox;
    public GameObject Targettrash3;

    private bool trashHealth;

    // Start is called before the first frame update
    void Start()
    {
        trashHealth = GetComponent<Trash3Health>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Targettrash3.transform.position, 30f * Time.deltaTime);

        if (Trash3Health.health <= 0)
        {
            Destroy(Hitbox);
        }
    }
}
