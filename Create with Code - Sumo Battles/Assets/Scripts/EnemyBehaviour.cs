using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject PowerupIndicator;
    public bool hasPowerup = false;

    private GameObject player;
    private Rigidbody enemyRb;
    private float PowerupStrength = 10.0f;
    private GameObject cloneIndicator;
    public int PowerupTimer { get; set; } = 7;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        // Gives script control of enemy movement
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // subtracts the players position with the enemys position and normalizes it 
        // to give theenemys movement direction axis
        Vector3 vectorOffset = Vector3.zero;
        if(player != null) vectorOffset = (player.transform.position - transform.position).normalized;
        // Adds a force to pull the enemy to the player's position
        // speed determines the acceleration value
        enemyRb.AddForce(vectorOffset * speed);
        
        // if the enemy has powerup abilities
        if(hasPowerup == true)
        {
            // a Powerup indicator is spawned and set to the enemy's position
            // to signify that the enemy has a powerUp
            cloneIndicator.transform.position = transform.position + new Vector3(0f, -0.4f, 0f);
        }

        // if the enemy falls of the platform (dojo)
        if (transform.position.y < -10)
        {
            // the enemy gameObject is destroyed
            Destroy(gameObject);
            // if the enemy gameObject has a powerUp ability
            if (hasPowerup)
                Destroy(cloneIndicator); // the powerUp indicator gameObject is destroyed too
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Enemy can pick up the powerUp just like the player
        if (other.gameObject.CompareTag("Powerup") && hasPowerup == false)
        {
            Destroy(other.gameObject);
            cloneIndicator = Instantiate(PowerupIndicator, transform.position + new Vector3(0f, -0.4f, 0f), PowerupIndicator.transform.rotation);
            hasPowerup = true;
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        // Powerup abilities lasts the same amount of time for the enemy
        // as the player
        yield return new WaitForSeconds(PowerupTimer);
        hasPowerup = false;
        Destroy(cloneIndicator);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // When enemy picks up a powerUp and collides with the player
        // the enemy can push the player away just as the player would with a powerup
        if (collision.gameObject.CompareTag("Player") && hasPowerup)
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = collision.gameObject.transform.position - gameObject.transform.position;

            playerRb.AddForce(awayFromEnemy * PowerupStrength, ForceMode.Impulse);

            Debug.Log("Collided with: " + collision.gameObject.name + " with Powerup set to " + hasPowerup);
        }
    }
}