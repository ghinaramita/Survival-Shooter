using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float speedMultiplier = 2.0f;
    private PlayerMovement playerMovement;

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
        playerMovement = gameObjectCollectingPowerUp.GetComponent<PlayerMovement>();

        if (gameObjectCollectingPowerUp.tag == "Player")
        {
            playerMovement.AddSpeed(speedMultiplier);

            Debug.Log("Power Up collected, issuing payload for: " + gameObject.name);

            Destroy(gameObject);
        }
    }
}
