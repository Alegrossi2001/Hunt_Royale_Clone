using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekTargetState : State
{
    private Bot bot;
    private BotMovement botMovement;
    private ShootingManager shootingManager;

    public SeekTargetState(GameObject gameObject, StateMachine sm) : base(gameObject, sm)
    {

    }

    public override void Enter()
    {
        base.Enter();
        bot = gameObject.GetComponent<Bot>();
        botMovement = gameObject.GetComponent<BotMovement>();
        shootingManager = gameObject.GetComponentInChildren<ShootingManager>();
    }
    public override void Update()
    {
        base.Update();
        botMovement.Patrol(bot);
        botMovement.RestartBot(bot.agent);
        bot.target = shootingManager.DetectClosestEnemy(bot, false);
        if (bot.target != null)
        {
            sm.CurrentState = new FightEnemyState(gameObject, sm);
        }

    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(bot.target == null)
        {
            bot.starObject = shootingManager.DetectClosestStar(bot);
            if(bot.starObject != null )
            {
                sm.CurrentState = new GetStarState(gameObject, sm);
            }
        }
    }
}
