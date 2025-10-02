using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlurrWheelchair : MonoBehaviour
{
    #region inspector variables

    [Header("Speed Settings")]
    //Tune these settings to control the max speed, how long it takes to go from 0 to max, and time to go from max to 0
    [SerializeField] private float maxSpeed = 5.0f;
    [SerializeField] private float accelerationTime = 0.5f;
    [SerializeField] private float decelerationTime = 1.0f;
    private float moveSpeed;

    [Header("Turn Settings")]
    //Tune these settings to control the max turn speed, how long it takes to go from 0 to max, and time to go from max to 0
    [SerializeField] private float rotMaxSpeed = 45.0f;
    [SerializeField] private float rotAccelerationTime = 0.2f;
    [SerializeField] private float rotDecelerationTime = 0.5f;
    private float rotSpeed;

    [Header("Control Settings")]
    //Alter which keys control certain actions, fairly self explanitory
    //A turning right and D turning left is more realistic, but less intuitive, for now it is the default but can change in the future
    [SerializeField] private KeyCode turnRight = KeyCode.A;
    [SerializeField] private KeyCode turnLeft = KeyCode.D;
    
    [Header("Controls Only Used In Simple Mode")]
    //The next controls are only used in simple mode, when not in simple mode, only A and D is used, and you cannot move backwards
    [SerializeField] private KeyCode moveForward = KeyCode.W;
    [SerializeField] private KeyCode moveBackward = KeyCode.S;

    [Header("Misc. Settings")]
    //Simple Controls makes turning more intuitive, and moving forward and backward easier
    [SerializeField] private bool simpleControls;

    [Header("Grounded Attributes")]
    [SerializeField] private bool grounded = false;
    [SerializeField] private LayerMask groundMask;

    #endregion

    #region public references

    [SerializeField] private Transform cam;
    

    #endregion

    #region private references

    private Transform orient;
    private CharacterController cc;
    #endregion

    #region private variables

    // these bools are made true when the corresponding keys are held down
    [SerializeField] private bool isTurningRight;
    [SerializeField] private bool isTurningLeft;
    [SerializeField] private bool isMovingForward;

    [SerializeField] private float horizIn;
    [SerializeField] private float vertiIn;

    private Vector3 moveDir;

    private float groundDist = 0.02f;

    private Vector3 velocity;

    #endregion

    public float sensX;
    public float sensY;

    public float xCamRot;
    public float yCamRot;

    

    

    void Start()
    {
        orient = GetComponent<Transform>();
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

        yCamRot += mousex;
        xCamRot -= mousey;

        xCamRot = Mathf.Clamp(xCamRot, -90f, 90f);
        yCamRot = Mathf.Clamp(yCamRot, -90f, 90f);
        cam.localRotation = Quaternion.Euler(xCamRot, yCamRot, 0f);

        ManageMovementInput();
        SimpleControlsMovement();
    }

    #region movement methods

    private void ManageMovementInput()
    {
        isTurningLeft = Input.GetKey(turnLeft);
        isTurningRight = Input.GetKey(turnRight);

        if (isTurningLeft && !isTurningRight)
        {
            horizIn = 1f;
        }
        else if (!isTurningLeft && isTurningRight)
        {
            horizIn = -1f;
        }
        else
        {
            horizIn = 0f;
        }
    }

    private void CalculateMoveAndTurnSpeed()
    {
        moveSpeed = 1;
    }

    //use this method for normal controls
    private void NormalControlsMovement()
    {
        if (isTurningLeft && isTurningRight)
        {
            isMovingForward = true;
        }
    }

    //use this method for simple controls
    private void SimpleControlsMovement()
    {
        //  Movement stuff, can't move if not on ground because wheelchair
        if (grounded)
        {


            orient.Rotate(Vector3.up * horizIn * rotSpeed * Time.deltaTime * (vertiIn >= 0f ? 1f : -1f));
            velocity += orient.forward * vertiIn * moveSpeed;
        }
        cc.Move(velocity * Time.deltaTime);
    }

    #endregion
}
