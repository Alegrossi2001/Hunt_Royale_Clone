using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMagic : MonoBehaviour
{
    [SerializeField] private Transform gunPosition;

    private Character shooter;
    private Enemy target;

    public void SetParametersToShoot(Character shooter, Enemy target)
    {
        this.shooter = shooter;
        this.target = target;
    }

    public void Shoot()
    {
        Bullet.Create(gunPosition.position, shooter, target);
    }
}
