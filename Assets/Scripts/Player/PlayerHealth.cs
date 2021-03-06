using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;                                                
    bool damaged;                                               


    void Awake()
    {
        // Mendapatkan reference komponen
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        currentHealth = startingHealth;
    }


    void Update()
    {
        // Jika terkena damage
        if (damaged)
        {
            // Merubah warna gambar menjadi value dari flashColour
            damageImage.color = flashColour; 
        }
        else
        {
            // Fade out damage image
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Set damage to false
        damaged = false;
    }

    // Fungsi untuk mendapatkan damage
    public void TakeDamage(int amount)
    {
        damaged = true;

        // Mengurangi health
        currentHealth -= amount;

        // Merubah tampilan dari health slider
        healthSlider.value = currentHealth;

        // Memainkan suara ketika terkena damage
        playerAudio.Play();

        // Memanggil method Death() jika darahnya <= 10 dan belum mati
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void AddHealth(int healthBonus)
    {
        currentHealth += healthBonus;
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        healthSlider.value = currentHealth;
    }


    void Death()
    {
        isDead = true;
        playerShooting.DisableEffects();

        // Mentrigger animasi Die
        anim.SetTrigger("Die");

        // Memainkan suara ketika death
        playerAudio.clip = deathClip;
        playerAudio.Play();

        // Memainkan script player movement
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        // Reload ulang scene dengan index 0 di build setting
        SceneManager.LoadScene(0);
    }
}
