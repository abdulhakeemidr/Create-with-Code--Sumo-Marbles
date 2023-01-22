using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Player"))
        {
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            UIManager.instance.GameOverScreen();
            Time.timeScale = 0;
        }
    }
}