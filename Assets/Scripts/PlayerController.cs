using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        PlayerInput();
        AdjustPlayerFacingDirection(); //boleh diletak di sini untuk lebih responsif
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection(); // opsyenal
        Move();
    }
   private void PlayerInput()
{
    movement = playerControls.Movement.Move.ReadValue<Vector2>();
    bool isMoving = movement.sqrMagnitude > 0.01f;

    myAnimator.SetBool("IsMoving", isMoving);

    if (isMoving)
    {
        // Update arah semasa (untuk Blend Tree)
        myAnimator.SetFloat("MoveX", movement.x);
        myAnimator.SetFloat("MoveY", movement.y);

        // Simpan arah terakhir
        myAnimator.SetFloat("LastMoveX", movement.x > 0 ? 1 : (movement.x < 0 ? -1 : 0));
        myAnimator.SetFloat("LastMoveY", movement.y > 0 ? 1 : (movement.y < 0 ? -1 : 0));
    }
    else
    {
        // Bila idle, tetapkan 0 pada MoveX & MoveY (supaya Blend Tree idle aktif)
        myAnimator.SetFloat("MoveX", 0);
        myAnimator.SetFloat("MoveY", 0);
    }
}

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
       
    }
}