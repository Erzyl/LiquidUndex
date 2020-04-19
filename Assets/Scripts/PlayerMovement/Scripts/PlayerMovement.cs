using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 4.0f;
    public float runSpeed = 8.0f;
    public float slideSpeed = 10.0f;
    public float crouchSpeed = 2f;
    [SerializeField]
    private float jumpSpeed = 8.0f;
    [SerializeField]
    private float gravity = 20.0f;
    [SerializeField]
    private float antiBumpFactor = .75f;
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    [HideInInspector]
    public Vector3 contactPoint;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public bool playerControl = false;

    public bool grounded = false;
    public Vector3 jump = Vector3.zero;
    public float airControll = 0.3f;

    private RaycastHit hit;
    private Vector3 force;
    private bool forceGravity;
    private float forceTime = 0;

    public AudioClip jumpSound;

    [SerializeField]
    private AudioController footSteps;

    private void Awake(){
        // Saving component references to improve performance.
        controller = GetComponent<CharacterController>();
    }


    private void Update(){
        if (forceTime > 0)
            forceTime -= Time.deltaTime;


        
    }

    private void FixedUpdate(){
        if(forceTime > 0){
            if(forceGravity)
                moveDirection.y -= gravity * Time.deltaTime;
            grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
        }
    }

    public void LateFixedUpdate(){

    }


    public void Move(Vector2 input, bool sprint, bool crouching){
        if(forceTime > 0)
            return;

        float speed = (!sprint) ? walkSpeed : runSpeed;
        if (crouching) speed = crouchSpeed;

        if (grounded){

            moveDirection = new Vector3(input.x, -antiBumpFactor, input.y);
            moveDirection = transform.TransformDirection(moveDirection) * speed; //Local dir to world dir
            UpdateJump();

            PlayFootSteps(speed);

           
        }
        else { //Air controll
            Vector3 moveDirection = new Vector3(input.x* airControll, moveDirection.y, input.y* airControll); //Should not add aircontrol here?
            //Keep air controll 0-1 and then handle the rest as normal? Keep friction
            moveDirection = transform.TransformDirection(moveDirection); //Local dir to world dir

            // Vector3 movDirHolder = moveDirection;
            // movDirHolder.x = md.x;
            // movDirHolder.z += md.z;
            // moveDirection = movDirHolder;

        }

        
        
        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;
        // Move the controller, and set grounded true or false depending on whether we're standing on something
        grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
    }

    public void PlayFootSteps(float speed) {
        if (controller.velocity.magnitude > 0) {
            footSteps.delayBetweenClips = (1 / speed) * 3;
            footSteps.Play();
        }
    }

    //Movement during special phases
    public void Move(Vector3 direction, float speed, float appliedGravity){
        if (forceTime > 0)
            return;
        //When sliding etc
        
        Vector3 move = direction * speed;
        if (appliedGravity > 0){
            moveDirection.x = move.x;
            moveDirection.y -= gravity * Time.deltaTime * appliedGravity;
            moveDirection.z = move.z;
        }
        else
            moveDirection = move;

        //UpdateJump(); //Air control

        grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
    }

    public void Jump(Vector3 dir, float mult){
        AudioSource.PlayClipAtPoint(jumpSound, transform.position, 2f);
        jump = dir * mult; // up * speed
    }

    public void UpdateJump(){
        
        if (jump != Vector3.zero){ //Initiating jump
            Vector3 dir = (jump * jumpSpeed);

            if (dir.x != 0)
                moveDirection.x = dir.x;
            if (dir.y != 0)
                moveDirection.y = dir.y;
            if (dir.z != 0)
                moveDirection.z = dir.z;
        }
        jump = Vector3.zero;
    }

    public void ForceMove(Vector3 direction, float speed, float time, bool applyGravity){
        forceTime = time;
        forceGravity = applyGravity;
        moveDirection = direction * speed;
    }

    void OnControllerColliderHit(ControllerColliderHit hit){
        contactPoint = hit.point;
    }
}
