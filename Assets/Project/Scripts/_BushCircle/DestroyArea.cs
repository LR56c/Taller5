using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var projectile = other.gameObject.GetComponent<ProjectileBase>();
        
        if(projectile.IsLaunched)
        {
            Destroy(other.gameObject);
            source.PlayOneShot(source.clip);
        }
    }
}
