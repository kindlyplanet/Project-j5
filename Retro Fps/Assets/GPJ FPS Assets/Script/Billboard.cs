using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private SpriteRenderer theSr;
    
    // Start is called before the first frame update
    void Start()
    {
        theSr = GetComponent<SpriteRenderer>();
        theSr.flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerController.instance.transform.position, -Vector3.forward);
    }
}
