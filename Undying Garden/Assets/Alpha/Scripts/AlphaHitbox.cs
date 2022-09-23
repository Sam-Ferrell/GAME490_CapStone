using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaHitbox : MonoBehaviour
{
    public GameObject TargetAlpha;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetAlpha.transform.position, 20f * Time.deltaTime);
    }
}
