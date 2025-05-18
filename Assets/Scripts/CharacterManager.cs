using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum CharacterAction
{
    None,
    Eating
}

public class CharacterManager : Singleton<CharacterManager>
{
    [Header("References")]
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource walkSound;
    [SerializeField]
    private AudioSource eatSound;

    [Header("CharacterSettings")]
    public float speed = 10f;
    public float gravity = -9f;

    [Header("DevVariables")]
    [SerializeField]
    private float sprint;
    [DoNotSerialize]
    public bool isSprinting;

    [Header("Misc")]
    private Vector3 velocity;
    [SerializeField]
    private CharacterAction action;

    private void Start()
    {
        //Maybe move this later incase something increases the speed
        sprint = speed * 2;
        walkSound.enabled = false;
        eatSound.enabled = false;
    }

    private void Update()
    {
        if (_GM.gameState != GameState.Playing)
        {
            walkSound.enabled = false;
            eatSound.enabled = false;
            return;
        }
            
        if (action != CharacterAction.None) return;

        // Get movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float imaSpeedu = speed;

        if (Input.GetButtonDown("Interact"))
        {
            StartCoroutine(EatingStop(x, z));
        }

        if (x != 0 || z != 0)
        {
            walkSound.enabled = true;
            animator.SetBool("isMoving", true);
        }
        else { animator.SetBool("isMoving", false); walkSound.enabled = false; }


        if (Input.GetButton("Sprint"))
        {
            isSprinting = true;
            imaSpeedu = sprint;
            animator.SetBool("isSprinting", true);
        }
        else
        {
            animator.SetBool("isSprinting", false);
            isSprinting = false;
        }

   
        // Determine direction of movement
        Vector3 move = transform.forward * z;

        // Move the character
        controller.Move(move * imaSpeedu * Time.deltaTime);

        transform.Rotate(0f, x * imaSpeedu * 5 * Time.deltaTime, 0f);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Move with gravity applied
        controller.Move(velocity * Time.deltaTime);
    }

    public IEnumerator EatingStop(float x, float z)
    {
        x = 0 ; z = 0;
        action = CharacterAction.Eating;
        animator.SetTrigger("Eat");
        eatSound.enabled = true;
        yield return new WaitForSeconds(0.5f);
        action = CharacterAction.None;
        eatSound.enabled = false;
    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }

}
