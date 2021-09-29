using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int healthBonus = 10;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        transform.Translate(0, 0.5f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        PowerUpCollected(other.gameObject);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);
    }

    void PowerUpCollected(GameObject gameObjectCollectingPowerUp)
    {
        playerHealth = gameObjectCollectingPowerUp.GetComponent<PlayerHealth>();

        if (gameObjectCollectingPowerUp.tag == "Player")
        {
            playerHealth.AddHealth(healthBonus);

            Debug.Log("Power Up collected, issuing payload for: " + gameObject.name);

            Destroy(gameObject);
        }        
    }
}
