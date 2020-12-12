using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : PhysicsObject
{
    // ici en public ce sont les differentes variables que l'on peut modifier dans l'inspector 
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // la fonction en dessous permet le déplacement de votre personnage
    protected override void ComputeVelocity()
    {

        float a = transform.position.x;


        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0) : (move.x < 0));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    // Ajouter ici vos fonctions 

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "coin")
        {
            col.gameObject.SetActive(false);
        }
    }

}
