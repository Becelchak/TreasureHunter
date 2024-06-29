using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    private AudioClip commonSound;
    [SerializeField]
    private AudioClip specialSound;

    private AudioSource audioSource;
    private PlayerController playerController;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Treasure")
        {
            audioSource.clip = specialSound;
            audioSource.Play();
        }
        else if (collision.gameObject.tag == "NonTreasure")
        {
            audioSource.clip = commonSound;
            audioSource.Play();

            playerController.SetAbleDig(null);
        }
        else if(collision.gameObject.tag == "NonTreasure Hole")
        {
            audioSource.Stop();
            playerController.RemoveAbleDig();

            print("AAAA");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (audioSource.isPlaying)
            return;
        if (collision.gameObject.tag == "Treasure")
        {
            audioSource.clip = specialSound;
            audioSource.Play();

        }
        else if (collision.gameObject.tag == "NonTreasure")
        {
            audioSource.clip = commonSound;
            audioSource.Play();

            playerController.SetAbleDig(null);

        }
        else if (collision.gameObject.tag == "NonTreasure Hole")
        {
            audioSource.Stop();
            playerController.RemoveAbleDig();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        audioSource.Stop();
    }
}
