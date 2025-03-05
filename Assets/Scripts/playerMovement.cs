using UnityEditor.Tilemaps;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float Move;

    public float speed;
    public float runSpeed;

    public float jump;

    bool onGround;

    private Animator anim;

    private bool isFacingRight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");

        float currentSpeed = Input.GetButton("Run") ? runSpeed : speed;

        rb.linearVelocity = new Vector2(Move * currentSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jump * 10));
        }

        if((Move != 0) && onGround)
        {
            anim.SetBool("isWalking", true);
        }
        else if ((Move == 0) && onGround)
        {
            anim.SetBool("isWalking", false);
        }
        else if ((Move != 0) && !onGround)
        {
            anim.SetBool("isJumping", true);
        }

            //anim.SetBool("isJumping", );

        if (!isFacingRight && Move > 0)
        {
            Flip();
        }
        else if(isFacingRight && Move < 0)
        {
            Flip();
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            //Vector3 normal = other.GetContact(0).normal;
            //if(normal == Vector3.up)
            {
                onGround = true;
                anim.SetBool("isJumping", false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = false;
            anim.SetBool("isJumping", true);
        }
    }
}
