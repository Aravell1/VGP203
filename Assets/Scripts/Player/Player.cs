using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    CharacterController controller;
    Animator anim;
    Vector3 moveDirection;

    [Header("Player Settings")]
    [Space(2)]
    [Tooltip("Speed value between 1 and 6")]
    [Range(1.0f, 12.0f)]
    public float speed;
    public float storedSpeed;
    public float jumpSpeed;
    public float rotationSpeed;
    public float gravity;

    [Header("Cameras")]
    public Camera fpsCam;
    public Camera mainCam;
    
    [Header("Pickup Settings")]
    public float jumpModeTimer = 5;
    public float jumpMultiplier = 1.5f;
    public GameObject gunContainer;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.minMoveDistance = 0.0f;
        anim = GetComponentInChildren<Animator>();

        if (speed <= 0)
            speed = 8.0f;
        if (jumpSpeed <= 0)
            jumpSpeed = 6.0f;
        if (rotationSpeed <= 0)
            rotationSpeed = 10.0f;
        if (gravity <= 0)
            gravity = 9.81f;

        storedSpeed = speed;

        Debug.Log("Left Click or Hold to shoot, Right Click to toggle aim");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject)
        {
            AnimatorClipInfo[] curAnim = anim.GetCurrentAnimatorClipInfo(0);

            if (controller.isGrounded)
            {
                if (curAnim.Length > 0)
                {
                    if (curAnim[0].clip.name != "Cross Punch" && curAnim[0].clip.name != "Mma Kick" && curAnim[0].clip.name != "Death")
                        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    else
                        moveDirection = Vector3.zero;                
                }

                anim.SetFloat("Horizontal", moveDirection.x);
                anim.SetFloat("Vertical", moveDirection.z);

                moveDirection *= speed;
                moveDirection = transform.TransformDirection(moveDirection);

                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                    anim.SetTrigger("Jump");
                }
            }

            moveDirection.y -= gravity * Time.deltaTime;

            controller.Move(moveDirection * Time.deltaTime);

            /*if (!GameManager.Instance.gunEquipped && !GameManager.Instance.pause && curAnim[0].clip.name != "Hit To Body" && Input.GetButtonDown("Fire1"))
                anim.SetTrigger("Attack");
            if (!GameManager.Instance.gunEquipped && !GameManager.Instance.pause && curAnim[0].clip.name != "Hit To Body" && Input.GetButtonDown("Fire2"))
                anim.SetTrigger("Kick");*/
        }
    }
}
