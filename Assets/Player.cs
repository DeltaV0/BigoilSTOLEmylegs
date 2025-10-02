using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public float movespeed;

    public Transform orient;

    float horizIn;
    float vertiIn;

    Vector3 moveDir;

    public Rigidbody rb;

    public float sensX;
    public float sensY;

    public Transform cam;

    public float xRot;
    public float yRot;
    // Start is called before the first frame update
    void Start()
    {
         Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mousx = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mousy = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRot += mousx;
        xRot -= mousy;

        xRot = Mathf.Clamp(xRot, -90f, 90f);
        cam.rotation = Quaternion.Euler(xRot, yRot, 0);
        orient.rotation = Quaternion.Euler(0, yRot, 0);

        horizIn = Input.GetAxisRaw("Horizontal");
        vertiIn = Input.GetAxisRaw("Vertical");

        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if(flatVel.magnitude > 5){
            Vector3 limitedVel = flatVel.normalized * movespeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    void FixedUpdate()
    {
        moveDir = orient.forward * vertiIn + orient.right * horizIn;
        rb.AddForce(moveDir.normalized * movespeed * 10f, ForceMode.Force);


    }
}
