using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlurrWheelchair : MonoBehaviour
{
    #region inspector variables

    [Header("Speed Settings")]
    //Tune these settings to control the max speed, how long it takes to go from 0 to max, and time to go from max to 0
    [SerializeField] private float maxSpeed = 5.0f;
    [SerializeField] private float accelerationRate = 0.5f;
    [SerializeField] private float decelerationRate = 1.0f;
    [SerializeField] private float moveSpeed;

    [Header("Turn Settings")]
    //Tune these settings to control the max turn speed, how long it takes to go from 0 to max, and time to go from max to 0
    [SerializeField] private float rotMaxSpeed = 45.0f;
    [SerializeField] private float rotAccelerationRate = 0.2f;
    [SerializeField] private float rotDecelerationRate = 0.5f;
    [SerializeField] private float rotSpeed;

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
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [Header("Grounded Attributes")]
    [SerializeField] private bool grounded = false;

    [SerializeField] private bool groundedLeeway = false;
    [SerializeField] private LayerMask groundMask;

    [Header("Selector/Inventory Script")]
    [SerializeField] private selector halt;

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

    private Vector3 moveDir;

    [SerializeField] private float groundDist = 0.02f;

    private Vector3 velocity;

    private float xCamRot;
    private float yCamRot;

    #endregion

    void Start()
    {
        orient = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cc = GetComponent<CharacterController>();

        SimpleControlsMovement();
    }

    float pushPower = 2.0f;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.linearVelocity = pushDir * pushPower;
    }

    // Update is called once per frame
    void Update()
    {
        //  Are we on the ground
        Vector3 center = transform.position + cc.center;
        Vector3 offset = transform.up * Mathf.Max(cc.height - 2f * cc.radius) / 2f;
        grounded = Physics.CapsuleCast(center + offset, center - offset, cc.radius, Vector3.down, groundDist, groundMask);
        groundedLeeway = Physics.CapsuleCast(center + offset, center - offset, cc.radius * 3f, Vector3.down, groundDist, groundMask);
        if (grounded)
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
        
        if (simpleControls)
        {
            SimpleControlsMovement();
        }
        else
        {
            NormalControlsMovement();
        }
        
        CalculateMoveAndTurnSpeed();
    }

    #region movement methods

    private void ManageMovementInput()
    {
        //  Movement stuff, can't move if not on ground because wheelchair
        if (grounded || groundedLeeway)
        {
            isTurningLeft = Input.GetKey(turnLeft);
            isTurningRight = Input.GetKey(turnRight);
        }
        else
        {
            isTurningLeft = false;
            isTurningRight = false;
        }
        
    }

    private void CalculateMoveAndTurnSpeed()
    {
        cc.Move(velocity * Time.deltaTime);

        //calculate and move the chair forward
        
        moveSpeed = Mathf.Clamp(moveSpeed + (isMovingForward ? accelerationRate : -decelerationRate) * Time.deltaTime, 0f, maxSpeed);
        cc.Move((transform.forward * moveSpeed) * Time.deltaTime);

        //calculate and turn the chair
        float targetRotSpeed = 0f;

        if (isTurningLeft && !isTurningRight && !halt.stopped)
        {
            targetRotSpeed = -rotMaxSpeed;
        }
        else if (isTurningRight && !isTurningLeft && !halt.stopped)
        {
            targetRotSpeed = rotMaxSpeed;
        }
        else
        {
            targetRotSpeed = 0f;
        }

        if (rotSpeed < targetRotSpeed)
        {
            rotSpeed = Mathf.Min(rotSpeed + rotAccelerationRate * Time.deltaTime, targetRotSpeed);
        }
        else if (rotSpeed > targetRotSpeed)
        {
            rotSpeed = Mathf.Max(rotSpeed - rotAccelerationRate * Time.deltaTime, targetRotSpeed);
        }

        // Apply rotation
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
    }

    //use this method for normal controls
    private void NormalControlsMovement()
    {
        //includes only the specific logic for this movement mode
        if (isTurningLeft && isTurningRight && !halt.stopped)
        {
            isMovingForward = true;
        }
        else
        {
            isMovingForward = false;
        }
        
    }

    //use this method for simple controls
    private void SimpleControlsMovement()
    {
        //Not made yet
    }

    #endregion
}
