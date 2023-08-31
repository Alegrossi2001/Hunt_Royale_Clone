using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStarState : State
{
    private Bot bot;
    private BotMovement botMovement;
    private ShootingManager shootingManager;

    public GetStarState(GameObject gameObject, StateMachine sm) : base(gameObject, sm)
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
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        if(bot.starObject != null)
        {
            botMovement.ChaseStar(bot, bot.starObject);
        }
        else
        {
            sm.CurrentState = new SeekTargetState(gameObject, sm);
        }
    }
}
