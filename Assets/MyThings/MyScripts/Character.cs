using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
   //AI
    public Transform target;
    public Animator anim;
    public NavMeshAgent agent;
    private float currentWeight;

    public float DistanceFromTarget()
    {
        return Vector3.Distance(this.transform.position, this.target.transform.position);
    }

    public void FadeInAttackAnimation()
    {
        currentWeight = Mathf.Lerp(currentWeight, 1.0f, Time.deltaTime);
        anim.SetLayerWeight(1, currentWeight);
    }

    public void FadeOutAttackAnimation()
    {
        currentWeight = Mathf.Lerp(currentWeight, 0.0f, Time.deltaTime);
        anim.SetLayerWeight(1, currentWeight);
    }
}
