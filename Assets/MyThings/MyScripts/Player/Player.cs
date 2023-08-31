using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FriendlyCharacter
{
    private PlayerMovement playerMovement;
    private ShootMagic shootMagic;
    private HealthSystem healthSystem;
    private bool isDead;

    public static EventHandler OnPlayerDeath;

    private void Awake()
    {

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthAmountMax(health, true);

        anim = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        shootingManager = GetComponentInChildren<ShootingManager>();
        shootMagic = GetComponentInChildren<ShootMagic>();
        
    }

    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnDied += HealthSystem_OnDeath;
    }


    private void Update()
    {
        if(isDead) return;

        target = shootingManager.DetectClosestEnemy(this, false);
        if (!playerMovement.IsMoving())
        {
            if (target != null)
            {
                PrepareToShootTarget(this.transform, target);
                shootMagic.SetParametersToShoot(this, target.gameObject.GetComponent<Enemy>());
                FadeInAttackAnimation();
                anim.SetBool("Shoot", true);
            }
        }
        else
        {
            FadeOutAttackAnimation();
            anim.SetBool("Shoot", false);
        }
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        //play damaged sound here
    }

    private void HealthSystem_OnDeath(object sender, System.EventArgs e)
    {
        //play death animation
        anim.SetBool("Shoot", false);
        anim.SetLayerWeight(1, 0);
        anim.SetBool("Death", true);
        //disable movement
        playerMovement.enabled = false;
        //Disable target seeking
        isDead = true;
        //trigger game over
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);

    }


}
