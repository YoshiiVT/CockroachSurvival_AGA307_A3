using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator animator;

    [Header("CharacterSettings")]
    public float speed = 10f;
    public float gravity = -9f;

    [Header("DevVariables")]
    [SerializeField]
    private float sprint;
    [SerializeField]
    private bool isMoving;
    [DoNotSerialize]
    public bool isSprinting;

    [Header("Misc")]
    private Vector3 velocity;

    private void Start()
    {
        //Maybe move this later incase something increases the speed
        sprint = speed * 2;
    }

    private void Update()
    {
        // Get movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float imaSpeedu = speed;

        if (x != 0 || z != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else animator.SetBool("isMoving", false);


        if (Input.GetButton("Sprint"))
        {
            imaSpeedu = sprint;
            animator.SetBool("isSprinting", true);
        }
        else animator.SetBool("isSprinting", false);

        // Determine direction of movement
        Vector3 move = transform.forward * z;

        // Move the character
        controller.Move(move * imaSpeedu * Time.deltaTime);

        transform.Rotate(0f, x * imaSpeedu * 4 * Time.deltaTime, 0f);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Move with gravity applied
        controller.Move(velocity * Time.deltaTime);
    }
}
