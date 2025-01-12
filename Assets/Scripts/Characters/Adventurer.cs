using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Adventurer : MonoBehaviour
{
    [SerializeField] private float runSpeed = 40f;
    [SerializeField] private bool doubleJump = true;
    private int maxJumps = 1;
    private int jumps = 0;
    private bool canTripleJump = false;
    private bool jumperUsed = false;  // Flag to track if the Jumper has been used

    [HideInInspector] public HealthController health;

    private CharacterController character;
    private AttackController attack;
    private Animator animator;

    private float horizontalMove = 0f;
    private bool isJumping = false;
    private bool isCrouching = false;
    private int currentDirection = 0;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        health = GetComponent<HealthController>();
        attack = GetComponent<AttackController>();
        animator = GetComponent<Animator>();

        maxJumps = doubleJump ? 2 : 1;
    }

    private void Update()
    {
        if (health.isDead) return;
        HandleMovementInput();
        HandleActionInput();
    }

    private void FixedUpdate()
    {
        if (attack.isAttacking || health.isDead)
        {
            character.Move(0, false, false);
            return;
        }

        character.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
    }
    
    private void HandleMovementInput()
    {
        if (currentDirection == 0)
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    private void HandleActionInput()
    {
        if (Input.GetButtonDown("Jump"))
            Jump(true);

        if (Input.GetButtonDown("Attack") && !isJumping)
            Attack(true);
        else if (Input.GetButtonUp("Attack"))
            Attack(false);

        if (Input.GetButtonDown("Crouch"))
            Crouch(true);
        else if (Input.GetButtonUp("Crouch"))
            Crouch(false);
    }

    public void Move(int dir)
    {
        if (health.isDead) return;
        currentDirection = dir;
        horizontalMove = dir * runSpeed;
    }

    public void Jump(bool isJumpingInput)
    {
        if (health.isDead) return;

        if (isJumpingInput)
        {
            if (jumps < maxJumps)
            {
                jumps++;
                if (jumps == 1)
                    animator.Play("Jump");
                else
                    character.Jump();
                
                if (jumps == 3 && canTripleJump && !jumperUsed)
                {
                    jumperUsed = true;
                    DisableJumperPowerUp();
                }
            }
        }
        else if (isJumping)
        {
            jumps = 0;
        }

        isJumping = isJumpingInput;
        animator.SetInteger("Jumps", jumps);
    }

    public void Crouch(bool c)
    {
        if (health.isDead) return;
        isCrouching = c;
    }

    public void Attack(bool a)
    {
        if (health.isDead) return;
        SoundManager.Instance.PlayAttackSound();
        attack.Attack(a);
    }

    public void OnLanding() => Jump(false);

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
    
    // Enable triple jump ability when collected
    public void EnableJumperPowerUp()
    {
        if (!jumperUsed) 
        {
            maxJumps = 3; 
            canTripleJump = true;
            LevelManager.Instance.ShowJumperIcon();
        }
    }

    // Reset jumper power-up when player dies or levels up
    public void DisableJumperPowerUp()
    {
        maxJumps = doubleJump ? 2 : 1;  // Reset back to double jump or single jump
        canTripleJump = false;
        LevelManager.Instance.HideJumperIcon();  // Hide Jumper icon from HUD
    }
}
