using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed;
    public Transform orientsion;
    float horizontal_i;
    float vertical_i;
    Vector3 moveDir;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        MyInput();
    }

    void FixedUpdate()
    {
        moveplayer();
    }
    // Update is called once per frame
    public void MyInput()
    {
        horizontal_i = Input.GetAxisRaw("Horizontal");
        vertical_i = Input.GetAxisRaw("Vertical");

    }

    public void moveplayer()
    {
        moveDir = orientsion.forward * vertical_i + orientsion.right * horizontal_i;
        rb.AddForce(moveDir.normalized * speed * 10f, ForceMode.Force);
    }
}
