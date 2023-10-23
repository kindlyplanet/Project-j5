using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private GameObject explosion;
    
    [SerializeField] private float playerRange;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;

    [SerializeField] private bool shouldShoot; 
    [SerializeField] private float fireRate;
    private float shootCounter;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;

    [SerializeField] private Animator e_Anim;
    private bool isHurt = false;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange)
        {
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;

            rb.velocity = playerDirection.normalized * moveSpeed;

            if(shouldShoot)
            {
                shootCounter -= Time.deltaTime;
                if(shootCounter <= 0)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    shootCounter = fireRate;
                }
            } 
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void TakeDamage()
    {
        
        if (!isHurt)
        {
            health--;
            if (health <= 0 )
            {
                Destroy(gameObject);
                Instantiate(explosion, transform.position, transform.rotation);
                AudioController.instance.PlaySFX("enemydeath");

                StartCoroutine(ResetHurtState());
            }
            else
            {
                AudioController.instance.PlaySFX("enemyshoot");
                isHurt = true;
                e_Anim.SetBool("isHurt",true);
                StartCoroutine(ResetHurtState());
            }
        }
        
    }

    private IEnumerator ResetHurtState()
    {
        yield return new WaitForSeconds(0.1f);
        isHurt = false;
        e_Anim.SetBool("isHurt",false);
    }

  
}
