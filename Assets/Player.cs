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
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float mousex = Input.GetAxisRaw("Mouse X") * sensX;
        float mousey = Input.GetAxisRaw("Mouse Y") * sensY;

        yRot += mousex;
        xRot -= mousey;

        xRot = Mathf.Clamp(xRot, -90f, 90f);
        yRot = Mathf.Clamp(yRot, -90f, 90f);
        cam.localRotation = Quaternion.Euler(xRot, yRot, 0f);

        horizIn = Input.GetAxisRaw("Horizontal");
        vertiIn = Input.GetAxisRaw("Vertical");

        
        orient.Rotate(Vector3.up * horizIn * rotSpeed * Time.deltaTime * (vertiIn >= 0f ? 1f : -1f));
        cc.Move(orient.forward * vertiIn * moveSpeed * Time.deltaTime);
    }
}
