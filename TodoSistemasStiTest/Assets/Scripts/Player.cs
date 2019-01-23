using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float jumpSpeed = 1f;

    private float speedMultiplier;

    private float moveX = 0;
    private float moveY = 0;
    private float lookX = 0;
    private float gravity = -9.8f * 10;
    private CharacterController controller;
    private float lookAngle;
    private Animator anim;

    private Vector3 velocity = Vector3.zero;
    private Vector3 velocityObj;

    public Vector3 Velocity { get; private set; }


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        lookAngle = transform.rotation.eulerAngles.y;
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        moveX = CrossPlatformInputManager.GetAxisRaw("Horizontal") * -1;
        moveY = CrossPlatformInputManager.GetAxisRaw("Vertical") ;
        lookX = CrossPlatformInputManager.GetAxisRaw("Mouse X");

        velocityObj = Velocity;
        PlayerMovement();
        Animating();
    }

    public void PlayerMovement() 
    {

        Vector3 lastPosition = transform.position;

        Quaternion lookRotation = Quaternion.AngleAxis(transform.localEulerAngles.y, Vector3.up);


        velocity = Velocity;

            //Calculates Rotation
            lookAngle += lookX * 10f;
            transform.localRotation = Quaternion.AngleAxis(lookAngle, Vector3.up);

            //Player Input

            speedMultiplier = 1f;

            //Player Velocity

            Vector3 targetVelocity = lookRotation *  (new Vector3(moveX, 0, moveY) * speed);
            targetVelocity.y = 0;
            velocity = Vector3.MoveTowards(velocity, targetVelocity * speedMultiplier, Time.deltaTime * 20f);



        //Jump

        if (controller.isGrounded) 
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {

                print("jumping");
                float jumpVelocity;
                velocity.y = jumpSpeed / 2;
                jumpVelocity = targetVelocity.y;
            }
        }

        else
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        Velocity = (transform.position - lastPosition) / Time.deltaTime; // Calculates velocity

    }

    //Animation

    private void Animating() 
    {
        Vector3 relativeSpeed = Quaternion.Inverse(transform.rotation) * velocityObj; // non depedent axis of the worldspace
        anim.SetFloat("SpeedZ", relativeSpeed.z);
        anim.SetFloat("SpeedX", relativeSpeed.x);
        anim.SetFloat("SpeedY",relativeSpeed.y);
    }
}
