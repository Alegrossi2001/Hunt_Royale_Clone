using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAwayFromEnemy : State
{
    private Bot bot;
    private BotMovement botMovement;

    public MoveAwayFromEnemy(GameObject gameObject, StateMachine sm) : base(gameObject, sm)
    {

    }

    public override void Enter()
    {
        base.Enter();
        bot = gameObject.GetComponent<Bot>();
        botMovement = gameObject.GetComponent<BotMovement>();
    }
    public override void Update()
    {
        base.Update();
        if(bot.target != null)
        {
            botMovement.MoveAwayFromEnemy(bot, bot.target);
            bot.anim.SetLayerWeight(1, 0);
            bot.anim.ResetTrigger("Shoot");
            botMovement.RestartBot(bot.agent);
            if (bot.DistanceFromTarget() > 6)
            {
                sm.CurrentState = new FightEnemyState(gameObject, sm);
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
