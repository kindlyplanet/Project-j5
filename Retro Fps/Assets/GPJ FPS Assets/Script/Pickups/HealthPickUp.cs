using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] private int healthAmount;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.AddHealth(healthAmount);
            AudioController.instance.PlayHealthPickup();
            Destroy(gameObject);
        }    
    }
}
