using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBouncer : MonoBehaviour
{
    Rigidbody2D rb;
    public string bounceTargetTag;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag(bounceTargetTag))
        {

            Vector2 reflectedVelocity = Vector2.Reflect(rb.velocity, collision.GetContact(0).normal);

            rb.velocity = reflectedVelocity;

        }
    }
}
