using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : Pool<ProjectileSpawner>
{
    [Header("Projectile Parameter",order =0)]
    public float projectileSpeed = 2;


    public void SpawnProjectile()
    {
      GameObject projectile=     GetPooledObject(transform.position);
        projectile.GetComponent<Projectilecomponent>()?.LaunchProjectile(transform.right,projectileSpeed);
    }

}
