using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private HealthSystem healthSystem;
    private int health;
    private ShootingManager shootingManager;
    [SerializeField] private float attackDistance;
    [SerializeField] private Sword sword;
    private int xpDropped;
    private Color colorOfDrop;

    public static EventHandler OnDeath;

    //spawn conditions
    private SpawnPoint spawnPoint;

    private enum EnemyDifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
    [SerializeField] private EnemyDifficultyLevel enemyDifficultyLevel;

    private void Awake()
    {

        switch (enemyDifficultyLevel)
        {
            case EnemyDifficultyLevel.Easy: 
                health = UnityEngine.Random.Range(50, 150);
                sword.damage = UnityEngine.Random.Range(5, 20);
                xpDropped = UnityEngine.Random.Range(5, 10);
                colorOfDrop = Color.green;
                break;
            case EnemyDifficultyLevel.Medium:
                health = UnityEngine.Random.Range(160, 300);
                sword.damage = UnityEngine.Random.Range(21, 40);
                xpDropped = UnityEngine.Random.Range(30, 50);
                colorOfDrop = Color.blue;
                
                break;
            case EnemyDifficultyLevel.Hard: health = 
                    UnityEngine.Random.Range(301, 500);
                    sword.damage = UnityEngine.Random.Range(41, 60);
                xpDropped = UnityEngine.Random.Range(70, 90);
                colorOfDrop = Color.red;
                break;
            default:
                break;
        }

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthAmountMax(health, true);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        shootingManager = GetComponentInChildren<ShootingManager>();
    }

    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void Update()
    {
        
        if (target != null)
        {
            MoveToTarget();
            if (Vector3.Distance(this.transform.position, target.transform.position) < attackDistance)
            {
                FadeInAttackAnimation();
            }
            else
            {
                FadeOutAttackAnimation();
            }
        }
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.transform.position);
        anim.SetFloat("Move", 1.0f);
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        Debug.Log("Spawnpoint is " + spawnPoint);
        target = shootingManager.DetectClosestEnemy(this, true);
        //spawnPoint.RemoveSpawnPoint();
        //play damage sound here
    }

    public int GetHealthAmount()
    {
        return health;
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        EnemyDeath();
    }

    private void EnemyDeath()
    {
        //play death sound here
        Star.SpawnStar(xpDropped, this.transform, colorOfDrop);
        OnDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    public void SetSpawnPoint(SpawnPoint spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }
}
