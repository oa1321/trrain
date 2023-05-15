using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_camra : MonoBehaviour
{
    public Transform cam_pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam_pos.position;
    }
}
