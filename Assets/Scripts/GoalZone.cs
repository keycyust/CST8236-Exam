using UnityEngine;
using System.Collections.Generic;

public class GoalZone : MonoBehaviour
{

    public AudioSource goalAudio;

    public List<ParticleSystem> goalParticleSystems;

    void OnTriggerEnter(Collider collider)
    {
      //  Debug.Log(collider.gameObject.name + " entered the trigger!");
    }

    void OnTriggerStay(Collider collider)
    {
      //  Debug.Log(collider.gameObject.name + " stayed in the trigger!");
    }

    void OnTriggerExit(Collider collider)
    {
       // Debug.Log(collider.gameObject.name + " exited the trigger!");


        if (goalAudio != null)
        {
            goalAudio.Play();
        }
    }
}
