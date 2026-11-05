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
    [SerializeField] Vector2 HitSpeed = new Vector2(10f, 20f);

    bool isAlive = true;
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = playerRigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        //FlipSprite();
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
        if (value.isPressed)
        {
            playerRigidbody.linearVelocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerRigidbody.linearVelocity.y);
        playerRigidbody.linearVelocity = playerVelocity;
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
        if (playerCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ball")))
        {
            isAlive = false;
            playerRigidbody.linearVelocity = HitSpeed;
        }
    }
}
