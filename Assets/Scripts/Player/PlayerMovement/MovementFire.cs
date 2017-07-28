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

        ////////////////////////////////////////////////////////////
        //if (verticalDir == -1 && onADropAblePlatform)
        //{
        //    //Find colliders that arn't triggers.
        //    foreach (BoxCollider2D bc in colliders)
        //    {
        //        if (!bc.isTrigger)
        //        {
        //            bc.enabled = false;
        //            nextEnableColliders = Time.time + enableCollidersWait;
        //        }

        //        //if (!bc.isTrigger)
        //        //{
        //        //    bc.enabled = true;
        //        //}
        //    }
        //}

        //if (Time.time >= nextEnableColliders)
        //{
        //    foreach (BoxCollider2D bc in colliders)
        //    {
        //        if (!bc.isTrigger)
        //        {
        //            bc.enabled = true;
        //        }
        //    }
        //}
        ////////////////////////////////////////////////////////////

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
            if (inWater)
            {
                //Debug.Log("Water Jump");
                //Jump code for when in water
                // Reset our velocity
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                // Arial Jump
                //Debug.Log("Air Jump used" + rb.velocity.y);
                Vector2 waterJump = new Vector2();
                waterJump.y = arialJumpForce * waterJumpForceMultiplier;
                rb.AddForce(waterJump, ForceMode2D.Impulse);
            }
            else if (grounded)
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

        //water failsafe
        if (!inWater && GetComponent<Rigidbody2D>().mass == inWaterMass)
        {
            GetComponent<Rigidbody2D>().mass = outOfWaterMass;
        }

        if (verticalDir == -1 && inWater)
        {
            rb.mass = inWaterMassGoingDown;
        }
        else if (verticalDir == 0 && inWater)
        {
            rb.mass = inWaterMass;
        }
        else if (verticalDir == -1)
        {
            rb.mass = outOfWaterMassGoingDown;
        }
        else if (verticalDir == 0)
        {
            rb.mass = outOfWaterMass;
        }
    }
}
