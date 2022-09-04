using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public float gravity = -19.81f;
    public float jumpHeight = 3f;
    public float stamina = 100f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundmask;
    Animator animator;
    Vector3 velocity;
    bool isGrounded;
    bool isMoved = false;
    public float turntime = 0.1f;
    float turnSmooth;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float x_old = 0f;
        float z_old = 0f;
        if (x != x_old || z != z_old)
        {
            x_old = x;
            z_old = z;
            isMoved = true;
        }
        else if (x == x_old && z == z_old)
        {
            isMoved = false;
        }
        Vector3 direction = new Vector3(x, 0f, z).normalized;
        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmooth, turntime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            controller.Move(direction * speed * Time.deltaTime);
        }
        if(!isMoved)
            animator.SetBool("IsWalking", false);
        else if (isMoved) {
            animator.SetBool("IsWalking", true);
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
       
    }
}