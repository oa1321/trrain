using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class move : MonoBehaviour
{
    public string next_scene;
    public float speed;
    public float jumpForce;
    public float jumpCooldown;
    public float airmultiplier;
    bool readyTojump = true;
    public KeyCode jumpKey = KeyCode.Space;

    public KeyCode runKey;
    public float run;

    public Transform orientsion;
    float horizontal_i;
    float vertical_i;
    Vector3 moveDir;
    Rigidbody rb;

    float move_speed;
    float move_speed_mult = 1;

    public float ground_drag;
    public float playerHeight;
    public LayerMask whatisground;
    bool grounded;

    bool running = true;

    public float max_angle;

    // Start is called before the first frame update
    void Start()
    {
        move_speed = speed;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        //that calc in the end is because the hegoght is 2* the distance form the midlle to the groud and the 0.2 is for adding move space for errors
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatisground);
        MyInput();
        speed_check();
        if (grounded)
        {
            rb.drag = ground_drag;
        }
        else
        {
            //no drag in the air
            rb.drag = 0;
        }
    }
    void OnTriggerExit(Collider other)
    {
        move_speed_mult = 1;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        //i have only two so did the names and i wanted grass to sloe you 3/4 and grass 1/2
        if (other.tag == "grass")
        {
            move_speed_mult = 0.75f;
            move_speed *= move_speed_mult;
        }
        if (other.tag == "sand")
        {
            move_speed_mult = 0.5f;
            move_speed *= move_speed_mult;
        }
        //portal is defrant it loads a new scene
        if(other.tag == "portal")
        {
            SceneManager.LoadScene(next_scene);
        }
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

        if(Input.GetKey(jumpKey) && readyTojump && grounded)
        {
            readyTojump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if(Input.GetKey(runKey) && running){
            move_speed = run * move_speed_mult;
            running = false;
        }
        else if(!running)
        {
            running = true;
            move_speed = speed * move_speed_mult;
        }
       
    }
    
    public void moveplayer()
    {
        moveDir = orientsion.forward * vertical_i + orientsion.right * horizontal_i;
        if (grounded)
        {
            //10 is trail and error 
            rb.AddForce(moveDir.normalized * move_speed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDir.normalized * move_speed * 10f *airmultiplier, ForceMode.Force);
        }
        
    }
    public void speed_check()
    {
        //0f in both cases is we dont want this speed diraction to inturpt us 
        Vector3 flatval = new Vector3(rb.velocity.x, 0f, rb.velocity.x);
        if (flatval.magnitude > move_speed)
        {
            Vector3 limited = flatval.normalized * move_speed;
            rb.velocity = new Vector3(limited.x, 0f, limited.x);
        }
    }

    public void Jump()
    { //0f in both cases is we dont want this speed diraction to inturpt us 
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    private void ResetJump()
    {
        readyTojump = true;
    }
}
