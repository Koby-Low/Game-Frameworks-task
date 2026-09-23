using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    private AudioSource audioS;
    public int numberOfSounds = 3;
    public AudioClip[] randomSound;
    public float minimumTime=1;
    public float maximumTime=5;


    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        float wait_time = Random.Range(minimumTime, maximumTime);
        yield return new WaitForSeconds(wait_time); //esperar X segundos

        int r = Random.Range(0, numberOfSounds); // tocar som X
        audioS.PlayOneShot(randomSound[r]); //----------------------SOMs--------------------------
        StartCoroutine(Wait());
    }

}
