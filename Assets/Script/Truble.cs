using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truble : MonoBehaviour
{
    [SerializeField]
    private string Type;

    private AudioSource audioSource;
    private Animator animator;
    private GameManager gameManager;
    private PlayerController playerController;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void DoTruble()
    {
        switch (Type)
        {
            case "Bomb":
                audioSource.Play();
                animator.SetTrigger("Explose");
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<PolygonCollider2D>().enabled = false;

                gameManager.RemoveTime(5);
                break;
            case "Skull":
                audioSource.Play();
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<EdgeCollider2D>().enabled = false;

                animator.SetTrigger("Curse");

                playerController.DamageStamina(50);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            DoTruble();

    }
}
