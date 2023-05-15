using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_cam : MonoBehaviour
{
    public float sensy;
    public float sensx;

    public Transform orintension;

    float x_rot;
    float y_rot;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensx;
        float mouseY = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensy;

        x_rot += mouseX;
        y_rot -= mouseY;
        //90 and -90 limits the camra up and down range so we cant just loop around with the arrow
        x_rot = Mathf.Clamp(x_rot, -90f, 90f);

        //we dont want to spin the z axis
        transform.rotation = Quaternion.Euler(x_rot, y_rot, 0);
        //we dont want to spin the x and z axis(x because we will fly)
        orintension.rotation = Quaternion.Euler(0, y_rot, 0);
    }
}
