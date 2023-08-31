using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : FriendlyCharacter
{
    public float displacementDistance = 4f;
    public BotMovement botMovement { get; private set; }
    public int patrolSphere = 10;
    private ShootMagic shootMagic;
    public Transform starObject;
    
    private enum BotState
    {
        SeekEnemy,
        Fight,
        SeekStar
    }

    //Statemachine

    private StateMachine stateMachine;

    private BotState botState = BotState.SeekEnemy;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        botMovement = GetComponentInChildren<BotMovement>();
        shootingManager = GetComponentInChildren<ShootingManager>();
        shootMagic = GetComponentInChildren<ShootMagic>();

        //statemachine
        stateMachine = new StateMachine();
        stateMachine.CurrentState = new SeekTargetState(gameObject, stateMachine);
    }

    private void Start()
    {

        Star.OnStarSpawn += TryChaseStar;
    }

    private void Update()
    {
        stateMachine.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.FixedUpdate();
    }

    private void TryChaseStar(object sender, Star.OnStarSpawnEventArgs e)
    {
        if(Vector3.Distance(this.transform.position, e.starPosition.position) < 10)
        {
            botState = BotState.SeekStar;
        }
    }

    private void SetStarObject(Transform starObject)
    {
        this.starObject= starObject;
    }
}
