using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{

    public AudioClip musicFx;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //Fin del Juego

            GameManager.Instance.OnPlayerDeath.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager2.current.PlaySound(musicFx);
            GameManager.Instance.AddjustScore(1);
        }
    }

 

    
}
