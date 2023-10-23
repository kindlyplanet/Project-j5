using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] private int ammoAmount;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.currentAmmo += ammoAmount;
            PlayerController.instance.UpdateAmmoUI();
            AudioController.instance.PlaySFX("ammopickup");
            Destroy(gameObject);

        }
    } 
}
