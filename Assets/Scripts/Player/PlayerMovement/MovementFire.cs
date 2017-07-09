using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFire : PlayerMovement
{
    public override void Update()
    {
        //Determins what direction the player is facing
        PlayerFacing();
        AnimationMachine();
        //ScreenCollisions();
        //get player horizontal input
        horizontalDir = Input.GetAxis(horizontalMovement);
        verticalDir = Input.GetAxis(verticalMovement);

        //allows the player to move if they arn't holding the block button.
        if (!playerAttacks.blocking)
        {
            MovingPlayer();
        }

        //Player jump

        //DISABLE this if we want to player to need to push jump to bounce off of enemies/players.

        if ((enemyBelow || playerBelow) && bounceJumpsUsed < bounceJumpsAllowed && !grounded && playerHealth.allowedToInputAttacks)
        {
            playerAttacks.JumpAttack();
            Instantiate(jumpEffect, whatsBelowMeChecker.position, whatsBelowMeChecker.rotation);
        }

        if (Input.GetButtonDown(jumpMovement))
        {
            if (grounded)
            {
                groundJumpForce.y = shortJumpForce;
                rb.AddForce(groundJumpForce, ForceMode2D.Impulse);
                //Debug.Log("player Attacks.blocking: " + playerAttacks.blocking);
                if (playerAttacks.blocking)
                {
                    playerAttacks.blocking = false;
                    playerAttacks.blockNextFire = Time.time + playerAttacks.blockFireRate;
                }
                //Debug.Log("player Attacks.blocking: " + playerAttacks.blocking);
                PlayerJump();
            }
            else if (inWater)
            {
                Debug.Log("Water Jump");
                //Jump code for when in water
                // Reset our velocity
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                // Arial Jump
                //Debug.Log("Air Jump used" + rb.velocity.y);
                Vector2 waterJump = new Vector2();
                waterJump.y = arialJumpForce * 5;
                rb.AddForce(waterJump, ForceMode2D.Impulse);
            }
            else
            {
                PlayerJump();
            }
        }

        if (Input.GetButton(jumpMovement) && groundJumpInitiated && (maxJumpTimer - currentJumpTimer == fullJumpLimit))
        {
            // do full jump
            groundJumpForce.y = fullJumpForce;
            //Debug.Log("full jump " + (maxJumpTimer - currentJumpTimer));
            rb.AddForce(groundJumpForce, ForceMode2D.Impulse);
            groundJumpInitiated = false;
        }
    }
}
