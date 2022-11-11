using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject trash;
    public GameObject[] trashSpawns;

    public GameObject trash2;
    public GameObject[] trashSpawns2;

    public GameObject trash3;
    public GameObject[] trashSpawns3;

    public static bool trashCanSpawn = false;
    public static bool trashCanSpawn2 = false;
    public static bool trashCanSpawn3 = false;


    // Start is called before the first frame update
    void Start()
    {
        trashSpawns = GameObject.FindGameObjectsWithTag("Trash Spawn");
        trashSpawns2 = GameObject.FindGameObjectsWithTag("Trash Spawn2");
        trashSpawns3 = GameObject.FindGameObjectsWithTag("Trash Spawn3");

        spawnTrash();
        spawnTrash2();
        spawnTrash3();
    }

    private void Update()
    {
        if(trashCanSpawn)
        {
            trashCanSpawn = !trashCanSpawn;
            spawnTrash();
        }

        if (trashCanSpawn2)
        {
            trashCanSpawn2 = !trashCanSpawn2;
            spawnTrash2();
        }

        if (trashCanSpawn3)
        {
            trashCanSpawn3 = !trashCanSpawn3;
            Invoke(nameof(spawnTrash3), 3f);
        }
    }

    public void spawnTrash()
    {
        GameObject spawnChoice = trashSpawns[Random.Range(0, trashSpawns.Length)];
        Transform spawnPoint = spawnChoice.transform;
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(trash, spawnPoint.position, spawnRotation);
    }

    public void spawnTrash2()
    {
        GameObject spawnChoice = trashSpawns2[Random.Range(0, trashSpawns2.Length)];
        Transform spawnPoint = spawnChoice.transform;
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(trash2, spawnPoint.position, spawnRotation);
    }

    public void spawnTrash3()
    {
        GameObject spawnChoice = trashSpawns3[Random.Range(0, trashSpawns3.Length)];
        Transform spawnPoint = spawnChoice.transform;
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(trash3, spawnPoint.position, spawnRotation);
    }
}
