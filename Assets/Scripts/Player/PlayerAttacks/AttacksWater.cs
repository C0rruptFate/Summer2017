using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksWater : PlayerAttacks {

    [Tooltip("How much does this heal for when your hp is ABOVE the threshold. (multiply by damage done.)")]
    public float baseHealReduction = 0.5f;

    [Tooltip("What percentage of HP do you need to be below to get the bonus healing.")]
    public float healMultiplierThreshold = 0.25f;
    [Tooltip("How much does this heal for when your hp is Below the threshold. (multiply by damage done.)")]
    public float healMultiplier = 1.5f;

    private bool rangedMultiShot = false;

    // Use this for initialization
    public override void Start()
    {
        //Plugs myself into my PlayerHealth and Player movement scripts.
        // [TODO] CHANGE THESE GETCOMPONENTS FOR EACH ELEMENTAL ATTACK SCRIPT.
        GetComponent<PlayerHealth>().playerAttacks = GetComponent<AttacksWater>();
        GetComponent<PlayerMovement>().playerAttacks = GetComponent<AttacksWater>();
        base.Start();
    }

    public override void RangedAttack()
    {
        base.RangedAttack();

        if(!rangedMultiShot)
        {
            rangedMultiShot = true;
            Invoke("RangedAttack", 0.25f);
        }

        //[TODO] Set up special melee attack for each character.
    }

    protected override void SpecialMeleeAttack()
    {
        base.SpecialMeleeAttack();

        //[TODO] Set up special melee attack for each character.
    }

    public override void SpecialPlayerDefend()
    {
        //Spends the mana to use your special ranged attack.
        //playerHealth.SpendMana(specialDefendManaCost);

        //[TODO] Set up the special Defend for each character.

    }

    public override void SetSpecialRangedAttackStats(GameObject projectile)
    {
        base.SetSpecialRangedAttackStats(projectile);
        projectile.GetComponent<PlayerProjectileWaterSpecial>().baseHealReduction = baseHealReduction;
        projectile.GetComponent<PlayerProjectileWaterSpecial>().healMultiplierThreshold = healMultiplierThreshold;
        projectile.GetComponent<PlayerProjectileWaterSpecial>().healMultiplier = healMultiplier;

    }
}
