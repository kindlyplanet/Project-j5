using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
   [SerializeField] private int damageAmount;
   [SerializeField] private float bulletSpeed;
   [SerializeField] private Rigidbody2D rb;

   private Vector3 direction;

    private void Start() 
    {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
        direction *= bulletSpeed;    
    }

    private void Update() 
    {
        rb.velocity = direction * bulletSpeed;    
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.TakeDamage(damageAmount);

            Destroy(gameObject);
        }
    }
}
