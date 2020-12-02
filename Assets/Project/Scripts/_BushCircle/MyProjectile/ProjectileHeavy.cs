using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHeavy : ProjectileBase
{
    protected override void TowerCollisionCallback(ETowersType towerType, TowerObject towerObject, Collision2D other)
    {
        if(towerType == ETowersType.BOUNCE_TOWER)
        {
            MakeItBounce(other);
        }
    }

    private void MakeItBounce(Collision2D other)
    {
        Vector3 reflectedVector = Vector3.Reflect(_rb.velocity, other.GetContact(0).normal);
        _rb.velocity = reflectedVector;

        Quaternion rot = Quaternion.LookRotation(Vector3.forward, reflectedVector);
        transform.rotation = rot;
    }
}
