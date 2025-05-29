using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    private Vector2 lookDirection = Vector2.right;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Update()
    {
        MovementHandler();
        RotatePlayer();
    }

    private void MovementHandler()
    {
        Vector2 vector = InputManager.Instance.GetMovementVector();
        if (vector == Vector2.zero)
        {
            anim.SetBool("IsMoveing", false);
        }
        else
        {
            anim.SetBool("IsMoveing", true);
        }
        rb.velocity = vector * PlayerStats.Instance.GetCurrentMoveSpeed();
    }

    private void RotatePlayer()
    {
        if (PlayerCombat.Instance.HasTarget())
        {
            if (PlayerCombat.Instance.GetTargetEnemy().position.x < gameObject.transform.position.x)
            {
                Player.Instance.GetPlayerVisual().localScale = new Vector2(-1, Player.Instance.GetPlayerVisual().localScale.y);
            }
            else
            {
                Player.Instance.GetPlayerVisual().localScale = new Vector2(1, Player.Instance.GetPlayerVisual().localScale.y);
            }
        }
        else
        {
            Vector2 direction = rb.velocity.normalized;

            if (direction == Vector2.zero)
                return;

            lookDirection = direction;

            if (direction.x > 0)
            {
                Player.Instance.GetPlayerVisual().localScale = new Vector2(1, Player.Instance.GetPlayerVisual().localScale.y);
            }
            if (direction.x < 0)
            {
                Player.Instance.GetPlayerVisual().localScale = new Vector2(-1, Player.Instance.GetPlayerVisual().localScale.y);
            }
        }
    }
    public Vector2 GetLookDirection()
    {
        return lookDirection;
    }
}
