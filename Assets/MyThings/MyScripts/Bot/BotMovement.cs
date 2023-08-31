using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    private Transform centrePoint;
    private int waitTime;
    private float timer;
    private bool IsChasingaStar;
    private Transform starObject;

    private void Awake()
    {
        centrePoint = this.transform;
        waitTime = UnityEngine.Random.Range(3, 7);

    }

    public void Patrol(Bot bot)
    {
        timer += Time.deltaTime;
        if (bot.agent.remainingDistance <= bot.agent.stoppingDistance) //done with path
        {
            
            bot.anim.SetFloat("Move", 0, 0.1f, Time.deltaTime);
            Vector3 point;
            if (RandomPoint(centrePoint.position, bot.patrolSphere, out point)) //pass in our centre point and radius of area
            {
                if (timer >= waitTime) //head to new destination
                {
                    bot.agent.SetDestination(point);
                    bot.anim.SetFloat("Move", 1.0f);
                    timer = 0;
                }
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range; //random point in a sphere 
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public void FreezeBot(UnityEngine.AI.NavMeshAgent agent)
    {
        agent.isStopped = true;
    }

    public void RestartBot(UnityEngine.AI.NavMeshAgent agent)
    {
        agent.isStopped = false;
    }

    public void ChaseStar(Bot bot, Transform starPosition)
    {
        RestartBot(bot.agent);
        bot.agent.SetDestination(starPosition.position);
        bot.anim.SetFloat("Move", 1.0f);
        if(Vector3.Distance(bot.transform.position, starPosition.position) < 0.3f)
        {
            bot.anim.SetFloat("Move", 0.0f);
        }
    }

    public void SetStarObject(Transform starObject)
    {
        this.starObject = starObject;
    }

    public void MoveAwayFromEnemy(Bot bot, Transform enemyPosition)
    {
        Vector3 directionToEnemy = bot.transform.position - enemyPosition.position;
        Vector3 newPosition = bot.transform.position + directionToEnemy;
        bot.agent.SetDestination(newPosition);
        bot.anim.SetFloat("Move", 1, 0.1f, Time.deltaTime);
    }
}
