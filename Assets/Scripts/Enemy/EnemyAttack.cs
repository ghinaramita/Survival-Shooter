using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
    {
        // Mencari game object dengan tag "Player"
        player = GameObject.FindGameObjectWithTag ("Player");

        // Mendapatkan komponen player health
        playerHealth = player.GetComponent <PlayerHealth> ();

        // Mendapatkan komponen Animator 
        anim = GetComponent <Animator> ();

        // Mendapatkan enemy health
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Callback jika ada suatu object masuk kedalam trigger
    void OnTriggerEnter (Collider other)
    {
        // Set player in range
        if(other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;
        }
    }

    // Callback jika ada object yang keluar dari trigger
    void OnTriggerExit (Collider other)
    {
        // Set player not in range
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        // Mentrigger animasi PlayerDead jika darah player <= 0
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }

    void Attack ()
    {
        // Mengatur ulang timer
        timer = 0f;

        // Taking damage
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
