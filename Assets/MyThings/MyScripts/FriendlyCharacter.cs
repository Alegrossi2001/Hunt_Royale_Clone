using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyCharacter : Character
{
    //shooting
    internal ShootingManager shootingManager;
    public int health;

    public void PrepareToShootTarget(Transform character, Transform target)
    {
        character.LookAt(target);
    }
}
