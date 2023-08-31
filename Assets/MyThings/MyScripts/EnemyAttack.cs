using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Collider swordCollider;

    public void EnableSwordCollider()
    {
        swordCollider.enabled = true;
    }

    public void DisableSwordCollider()
    {
        swordCollider.enabled = false;
    }

}
