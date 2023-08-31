using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    private float fightingDistance = 4f;
    private float seekStarDistance = 10f;
    private Transform target;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask starLayer;
    public float GetFightingDistance()
    {
        return fightingDistance;
    }

    public Transform DetectClosestEnemy(Character character, bool isFriendly)
    {
        Collider[] colliders = Physics.OverlapSphere(character.transform.position, fightingDistance, enemyLayer);
        if (colliders.Length == 0)
        {
            target = null;
            return target;
        }
        //calculate distance from target, if it exists
        float currentDistanceToTarget = (target != null) ? Vector3.Distance(character.transform.position, target.transform.position) : Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            
            Character enemy = null;

            if (isFriendly)
            {
                enemy = collider.GetComponent<FriendlyCharacter>();
            }
            else if (!isFriendly)
            {
                enemy = collider.GetComponent<Enemy>();
            }

            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(character.transform.position, enemy.transform.position);
                if (target == null || distanceToEnemy < currentDistanceToTarget)
                {
                    target = enemy.transform;
                    return target;

                }
                else
                {
                    //attack closest target
                    if (Vector3.Distance(character.transform.position, enemy.transform.position) < Vector3.Distance(character.transform.position, target.transform.position))
                    {
                        target = enemy.transform;
                        return target;
                    }
                }
            }
        }
        if (currentDistanceToTarget > fightingDistance)
        {
            target = null;
        }
        return target;

    }

    public Transform DetectClosestStar(Character character)
    {
        Collider[] colliders = Physics.OverlapSphere(character.transform.position, seekStarDistance, starLayer);
        if (colliders.Length == 0)
        {
            target = null;
            return target;
        }
        //calculate distance from target, if it exists
        float currentDistanceToTarget = (target != null) ? Vector3.Distance(character.transform.position, target.transform.position) : Mathf.Infinity;

        foreach (Collider collider in colliders)
        {

            Star star = collider.GetComponent<Star>();

            if (star != null)
            {
                float distanceToStar = Vector3.Distance(character.transform.position, star.transform.position);
                if (target == null || distanceToStar < currentDistanceToTarget)
                {
                    target = star.transform;
                    return target;

                }
                else
                {
                    //attack closest target
                    if (Vector3.Distance(character.transform.position, star.transform.position) < Vector3.Distance(character.transform.position, target.transform.position))
                    {
                        target = star.transform;
                        return target;
                    }
                }
            }
        }
        if (currentDistanceToTarget > fightingDistance)
        {
            target = null;
        }
        return target;
    }

}
