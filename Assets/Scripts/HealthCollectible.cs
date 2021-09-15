using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    void OnTriggerEnter2D(Collider2D other)
    {
         RubyController controller = other.GetComponent<RubyController>();

         if (controller != null){
             if(controller.health < controller.maxHealth){
                controller.ChangeHealth(1);
                Destroy(gameObject);
                controller.playSound(collectedClip);
             }  
             else{
                  Destroy(gameObject);
                  controller.playSound(collectedClip);
            } 
         }
    }
}
