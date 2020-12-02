using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayershmupBulletShooter : Pool<PlayershmupBulletShooter>
{
    public float launchSpeed=2;
    public void ShootBulletUp()
    {
       GameObject bullet= GetPooledObject(transform.position);
        bullet.GetComponent<Projectilecomponent>().LaunchProjectile(transform.up, launchSpeed);
    }
}
