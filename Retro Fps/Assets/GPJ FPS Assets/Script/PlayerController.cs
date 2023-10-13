using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private float moveSpeed;
    
    [SerializeField] private float mouseSensitivity;
    
    [SerializeField] private Camera viewCam;
    
    [SerializeField] private GameObject bulletImpact;
    [SerializeField] private Animator gunAnim;
    public int currentAmmo;
    
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private GameObject deathScreen;
    private bool hasDied;
   
    [SerializeField] private TextMeshProUGUI healthText , ammoText; 

    [SerializeField] private Animator anim; 

    private Vector2 moveInput;
    private Vector2 mouseInput;

    [SerializeField] private GameObject test;
    [SerializeField] private bool pantallaActiva;
    
    private void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString() + "%";
        ammoText.text = currentAmmo.ToString();
        test.SetActive(false);
        pantallaActiva = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (pantallaActiva == true)
        {
            Time.timeScale = 0f;
        }
        if (pantallaActiva == false)
        {
            Time.timeScale = 1f;
            test.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pantallaActiva = true;
            test.SetActive(true);
        }

        if(!hasDied)
        {
            //Player Movement
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
           
            Vector3 moveHorizontal = transform.up * -moveInput.x;

            Vector3 moveVertical = transform.right * moveInput.y;

            rb.velocity = (moveHorizontal + moveVertical) * moveSpeed;

            //Player View
            mouseInput = new Vector2 (Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"))* mouseSensitivity;
        
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z - mouseInput.x);
        
            viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles + new Vector3(0f,mouseInput.y, 0f));

            //Shooting
        
            if (Input.GetMouseButtonDown(0))
            {
                if(currentAmmo > 0)
                {
                    Ray ray = viewCam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        //Debug.Log("Im looking at " + hit.transform.name);
                        Instantiate(bulletImpact, hit.point, transform.rotation);

                        if (hit.transform.tag == "Enemy")
                        {
                            hit.transform.parent.GetComponent<EnemyController>().TakeDamage();
                        }

                        AudioController.instance.PlayGunshot();
                    }
                    else
                    {
                        Debug.Log("Im looking at nothing!");
                    }
                    currentAmmo--;
                    gunAnim.SetTrigger("Shoot");
                    UpdateAmmoUI();
                }
            
            }

            if(moveInput != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving" , false);
            }    
        }
        
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            deathScreen.SetActive(true);
            hasDied = true;
            currentHealth = 0;
        }
        
        healthText.text = currentHealth.ToString() + "%";

        AudioController.instance.PlayPlayerHurt();

    }

    public void AddHealth(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthText.text = currentHealth.ToString() + "%";

    }

    public void UpdateAmmoUI()
    {
        ammoText.text = currentAmmo.ToString();
    }

    public void ResumeButtom()
    {
        Time.timeScale = 1f;
        pantallaActiva = false;
    }
}
