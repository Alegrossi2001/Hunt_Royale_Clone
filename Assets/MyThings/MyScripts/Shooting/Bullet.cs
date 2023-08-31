using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static Bullet Create(Vector3 position, Character shooter, Enemy target)
    {
        Transform pfMagicProjectile = Resources.Load<Transform>("magicProjectile");
        Transform magicTransform = Instantiate(pfMagicProjectile, position, Quaternion.identity);

        Bullet magicProjectile = magicTransform.GetComponent<Bullet>();
        magicProjectile.SetShooter(shooter);
        magicProjectile.SetTarget(target);
        return magicProjectile;
    }

    private void SetShooter(Character shooter)
    {
        this.shooter = shooter;
    }

    private void SetTarget(Enemy target)
    {
        this.target = target;
        Debug.Log(this.target);
    }

    private Character shooter;
    private Enemy target;
    private float bulletSpeed = 20f;
    private int gunDamage = 30;
    [SerializeField] private GameObject particleEffectOnExplosion;
    [SerializeField] private int damage;
    private float searchRadius = 5f;
    //bounce
    private Enemy currentTarget;
    [SerializeField] private LayerMask enemyLayer;
    private bool isBouncing;
    // Update is called once per frame
    void Update()
    {
            Vector3 moveDir = Vector3.zero;
            if (target != null)
            {
                moveDir = (target.transform.position - transform.position).normalized;
            }

            transform.position += moveDir * bulletSpeed * Time.deltaTime;

        
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemyToAttack = other.gameObject.GetComponent<Enemy>();
        if (enemyToAttack != null)
        {
            currentTarget = enemyToAttack;
            enemyToAttack.gameObject.GetComponent<HealthSystem>().Damage(gunDamage);
            Destroy(gameObject);
        }

        Destroy(gameObject, 2);
    }
}
