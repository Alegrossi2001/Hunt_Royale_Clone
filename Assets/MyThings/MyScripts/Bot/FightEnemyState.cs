using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemyState : State
{
    private Bot bot;
    private BotMovement botMovement;
    private ShootMagic shootMagic;
    public FightEnemyState(GameObject gameObject, StateMachine sm) : base(gameObject, sm)
    {

    }

    public override void Enter()
    {
        base.Enter();
        bot = gameObject.GetComponent<Bot>();
        botMovement = gameObject.GetComponent<BotMovement>();
        shootMagic = gameObject.GetComponentInChildren<ShootMagic>();
    }
    public override void Update()
    {
        bot.PrepareToShootTarget(bot.transform, bot.target);
        if(bot.target != null)
        {
            shootMagic.SetParametersToShoot(bot, bot.target.gameObject.GetComponent<Enemy>());
            bot.anim.SetFloat("Move", 0);
            bot.anim.SetLayerWeight(1, 1);
            bot.anim.SetBool("Shoot", true);
            botMovement.FreezeBot(bot.agent);
            if (bot.DistanceFromTarget() < 3)
            {
                sm.CurrentState = new MoveAwayFromEnemy(gameObject, sm);

            }

        }

        else
        {
            sm.CurrentState = new SeekTargetState(gameObject, sm);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
