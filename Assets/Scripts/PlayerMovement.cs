using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    float gravityScaleAtStart;
    CapsuleCollider2D playerCapsuleCollider;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] int maxJumps = 2; //count for how many jumps one can do before needing to refresh
    [SerializeField] Vector2 HitSpeed = new Vector2(10f, 20f);
    int jumps = 0;

    bool isAlive = true;
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = playerRigidbody.gravityScale;
        jumps = maxJumps;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        FlipSprite();
        Death();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (jumps == 0){
            return;
        }
        if (value.isPressed)
        {
            jumps -= 1;
            playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, 0f);
            playerRigidbody.linearVelocity += new Vector2(playerRigidbody.linearVelocity.x, jumpSpeed);
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerRigidbody.linearVelocity.y);
        playerRigidbody.linearVelocity = playerVelocity;
    }

    void OnAttack(InputValue value)
    {
        //Instantiate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor")){
            jumps = maxJumps;
        }
    }

    void FlipSprite()
    {
        if (Mathf.Abs(moveInput.x) > Mathf.Epsilon)
        {
            if(playerAnimator.GetBool("isBounceJumping") == true)
            {
                transform.localScale = new Vector2(-(Mathf.Sign(playerRigidbody.linearVelocity.x)), 1f);
            }
            else
            {
                transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.linearVelocity.x), 1f);
            }
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
    }
    void Death()
    {
        if (playerCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("")))
        {
            isAlive = false;
            playerRigidbody.linearVelocity = HitSpeed;
        }
    }
}
