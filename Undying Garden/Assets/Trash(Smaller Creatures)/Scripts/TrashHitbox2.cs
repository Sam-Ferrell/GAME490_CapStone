using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHitbox2 : MonoBehaviour
{
    public GameObject Hitbox;
    public GameObject Targettrash2;

    private bool trashHealth;

    // Start is called before the first frame update
    void Start()
    {
        trashHealth = GetComponent<Trash2Health>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Targettrash2.transform.position, 20f * Time.deltaTime);

        if (Trash2Health.health <= 0)
        {
            Destroy(Hitbox);
        }
    }
}
