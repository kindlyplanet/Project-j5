using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : MonoBehaviour
{
    [SerializeField] private GameObject explotion;
    [SerializeField] private float playerRange;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int health;
    [SerializeField] private Animator animator;
    private bool isHurt = false;
    [SerializeField] private int damageExplosion;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        
        if ( distanceToPlayer < playerRange)
        {
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;

            rb.velocity = playerDirection.normalized * moveSpeed;

        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if(distanceToPlayer < 1.0f)
        {
            Explode();
        }
     
    }

    public void Explode()
    {
        PlayerController.instance.TakeDamage(damageExplosion);
        Destroy(gameObject);
        Instantiate(explotion,transform.position,transform.rotation);
    }

    public void TakeDamage()
{
    if (!isHurt)
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(explotion, transform.position, transform.rotation);
            AudioController.instance.PlaySFX("enemydeath");

            StartCoroutine(ResetHurtState());
        }
        else
        {
            isHurt = true;
            animator.SetBool("isHurt", true);
            StartCoroutine(ResetHurtState());
        }
    }
}


    private IEnumerator ResetHurtState()
    {
        yield return new WaitForSeconds(0.1f);
        isHurt = false;
        animator.SetBool("isHurt",false);
    }
}
