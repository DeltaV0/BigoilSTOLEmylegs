using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Transform orient;

    float horizIn;
    float vertiIn;

    Vector3 moveDir;

    private CharacterController cc;
    public float rotSpeed = 45;
    public float moveSpeed = 2;

    public float sensX;
    public float sensY;

    public Transform cam;

    public float xRot;
    public float yRot;

    public bool grounded = false;
    public LayerMask groundMask;
    public float groundDist = 0.01f;

    public Vector3 velocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //  Are we on the ground
        Vector3 center = transform.position + cc.center;
        Vector3 offset = transform.up * Mathf.Max(cc.height - 2f * cc.radius) / 2f;
        grounded = Physics.CapsuleCast(center + offset, center - offset, cc.radius, Vector3.down, groundDist, groundMask);
        if(grounded)
        {
            //  Even if we're on the ground, move down a little, just to account for discrepancies
            velocity = Vector3.down * -0.1f;
        }
        else
        {
            velocity += Physics.gravity * Time.deltaTime;
        }

        //  Look stuff
        float mousex = Input.GetAxisRaw("Mouse X") * sensX;
        float mousey = Input.GetAxisRaw("Mouse Y") * sensY;

        yRot += mousex;
        xRot -= mousey;

        xRot = Mathf.Clamp(xRot, -90f, 90f);
        yRot = Mathf.Clamp(yRot, -90f, 90f);
        cam.localRotation = Quaternion.Euler(xRot, yRot, 0f);

        //  Movement stuff, can't move if not on ground because wheelchair
        if (grounded)
        {
            horizIn = Input.GetAxisRaw("Horizontal");
            vertiIn = Input.GetAxisRaw("Vertical");


            orient.Rotate(Vector3.up * horizIn * rotSpeed * Time.deltaTime * (vertiIn >= 0f ? 1f : -1f));
            velocity += orient.forward * vertiIn * moveSpeed;
        }
        cc.Move(velocity * Time.deltaTime);
    }
}
