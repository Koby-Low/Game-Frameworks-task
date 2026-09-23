using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger_Simple : MonoBehaviour
{
    private AudioSource audioS;
    private GameObject player;
    public AudioClip triggerSound;
    public bool triggerEnter = true;
    public bool triggerExit = false;
    public bool destroySelf = false;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioS = player.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerEnter)
        {
            if (other.CompareTag("Player"))
            {
                audioS.PlayOneShot(triggerSound); //----------------------SOM--------------------------

                if (destroySelf)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggerExit)
        {
            if (other.CompareTag("Player"))
            {
                audioS.PlayOneShot(triggerSound); //----------------------SOM--------------------------
            }
        }
    }


}
