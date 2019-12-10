using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class Mover : MonoBehaviour
{
    /* This script uses CharacterController to move the player 
     * using WASD and pressing Space to jump. Run and jump speed
     * are adjustable as well as gravity. */

    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float jumpHeight = 1f;
    public float gravity = -12f;
    
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    public AudioClip walkSound;
    AudioSource audioSource;

    // public Vector3 moveDirect = Vector3.zero;

    Animator animator;

    CharacterController charControl;

    Transform cameraT;

    

    private void Start()
    {
        animator = GetComponent<Animator>();

        charControl = GetComponent<CharacterController>();
        cameraT = Camera.main.transform;
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("PlaySound", 0.0f, 0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        // if the mouse is hovering over a UI object, the player should stand still
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }


        // gathers horizontal and vertical data for movement
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector2 inputDir = input.normalized;

        // if space is pressed, then jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // if input is not equal to zero, then move and rotate the player using euler angles and Atan2 logic
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector2.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        // if left shift is pressed, get into run speed
        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;

        // smooths out animation
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        // get Y velocity;
        velocityY -= Time.deltaTime * gravity;

        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        charControl.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(charControl.velocity.x, charControl.velocity.z).magnitude;

        if (charControl.isGrounded)
        {
            velocityY = 0;
        }

        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

    }

    // method for jumping when player is grounded
    void Jump()
    {
        if (charControl.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
        }
    }

    void PlaySound()
    {
        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            audioSource.PlayOneShot(walkSound);
        }
    }

}
