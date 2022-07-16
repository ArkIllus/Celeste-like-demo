using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [Header("移动参数")]
    public float speed = 8f;
    public float crouchSpeedMulti = 0.33f;

    [Header("状态")]
    public bool isCrouch;

    private float xVelocity;

    [Header("碰撞体尺寸")]
    //TODO:改为动画中改变参数
    private Vector2 colliderStandSize;
    private Vector2 colliderStandOffset;
    private Vector2 colliderCrouchSize;
    private Vector2 colliderCrouchOffset;
    public float crouchSizeMul;
    public float crouchOffsetMul;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        colliderStandSize = coll.size;
        colliderStandOffset = coll.offset;
        colliderCrouchSize = new Vector2(coll.size.x, coll.size.y * crouchSizeMul);
        colliderCrouchOffset = new Vector2(coll.offset.x, coll.offset.y * crouchOffsetMul);
    }

    private void FixedUpdate()
    {
        GroundMovement();
    }

    private void Update()
    {
        
    }

    private void GroundMovement()
    {
        if (Input.GetButton("Crouch"))
        {
            Crouch();
        }
        else if (isCrouch)
        {
            StandUp();
        }

        xVelocity = Input.GetAxis("Horizontal"); //-1 1 //不按=0
        if (isCrouch)
        {
            xVelocity *= crouchSpeedMulti;
        }

        rb.velocity = new Vector2(xVelocity * speed,rb.velocity.y);
        
        FlipDirection();
    }

    private void FlipDirection()
    {
        if (xVelocity < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if (xVelocity < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void Crouch()
    {
        isCrouch = true;
        coll.size = colliderCrouchSize;
        coll.offset = colliderCrouchOffset;
    }

    private void StandUp()
    {
        isCrouch = false;
        coll.size = colliderStandSize;
        coll.offset = colliderStandOffset;
    }
}
