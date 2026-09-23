using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCollision_Simple : MonoBehaviour
{
    private AudioSource audioS;
    public AudioClip collisionSound;
    

    void Start()
    {
        audioS = gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioS.pitch = Random.Range(0.2f, 1.8f);
        audioS.PlayOneShot(collisionSound); //----------------------SOM--------------------------
    }

}
