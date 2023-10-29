using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static event System.Action OnBossDeath;
    
    [SerializeField] private int health;
    [SerializeField] private GameObject explotion;

    [SerializeField] private float playerRange;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;

    [SerializeField] private bool shouldShoot;
    [SerializeField] private float fireRate;
    private float shootCounter;
    [SerializeField] private GameObject bossBullet;
    [SerializeField] private Transform firePoint;

    [SerializeField] private Animator animator;
    private bool isHurt = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        if(distanceToPlayer < playerRange)
        {
            Vector3 playerDirection = PlayerController. instance.transform.position - transform.position;

            rb.velocity = playerDirection.normalized * moveSpeed;

            if(shouldShoot)
            {
                shootCounter -= Time.deltaTime;
                
                if(shootCounter <= 0)
                {
                    Instantiate(bossBullet, firePoint.position, firePoint.rotation);
                    shootCounter = fireRate;
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
            }

        }
    }

    public void TakeDamage()
    {
        if(!isHurt)
        {
            health--;
            if(health <= 0)
            {
                Destroy(gameObject);
                Instantiate(explotion, transform.position, transform.rotation);
                AudioController.instance.PlaySFX("enemydeath");
                StartCoroutine(ResetHurtState());
                OnBossDeath?.Invoke();
            }
            else
            {
                AudioController.instance.PlaySFX("enemyshoot");
                isHurt = true;
                animator.SetBool("isHurt",true);
                StartCoroutine(ResetHurtState());
            }
        }
    }

    private IEnumerator ResetHurtState()
    {
        yield return new WaitForSeconds(0.1f);
        isHurt = false;
        animator.SetBool("isHurt", false);
    }
}
