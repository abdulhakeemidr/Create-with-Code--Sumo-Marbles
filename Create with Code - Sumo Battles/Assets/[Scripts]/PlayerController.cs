using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick leftJoystick;
    public Joystick rightJoystick;
    public float speed = 10.0f;
    public float maxSpeed = 10.0f;
    public float SpeedDisplay;
    public bool hasPowerup = false;
    public GameObject PowerupIndicator;

    public int PowerupTimer { get; set; } = 7;
    private float PowerupStrength = 10.0f;
    private GameObject focalPoint;
    private Rigidbody playerRb;
    private GameObject cloneIndicator;

    // Start is called before the first frame update
    void Start()
    {
        focalPoint = GameObject.Find("Focal Point");
        // Gets the Rigidbody to control player movement
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the player has the PowerUp ability
        if (hasPowerup == true)
            // the PowerUp indicator gameObject follows the player to signify that it has a powerUp
            cloneIndicator.transform.position = transform.position + new Vector3(0f, -0.4f, 0f);
    }

    void FixedUpdate() {
        
        // Gets the vertical axis "W & S" for forward input
        float forwardInput = Input.GetAxis("Vertical") + leftJoystick.Vertical;
        float sideInput = leftJoystick.Horizontal;

        //cloneIndicator.transform.position = transform.position + new Vector3(0f, -0.4f, 0f);

        // moves forward relative to where the game camera faces
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        playerRb.AddForce(focalPoint.transform.right * sideInput * speed);

        // limits the max speed
        if(playerRb.velocity.magnitude > maxSpeed)
        {
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, maxSpeed);
        }

        SpeedDisplay = playerRb.velocity.magnitude;

    }

    private void OnTriggerEnter(Collider other)
    {
        // if player collides with powerup collision box 
        // & the player doesnt have a powerup (hasPowerup = false)
        if(other.gameObject.CompareTag("Powerup") && hasPowerup == false)
        {
            // destroy the powerUp gameObject
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("Pickup");
            // Create a PowerupIndicator gameObject and store a reference to its instance
            // This will be placed around the player (at the player position)
            // for a certain amount of time
            cloneIndicator = Instantiate(PowerupIndicator, transform.position + new Vector3(0f, -0.4f, 0f), PowerupIndicator.transform.rotation);
            // sets hasPowerup to true to give to player powerUp abilities
            hasPowerup = true;
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    // This interface function is passed into a Coroutine
    IEnumerator PowerupCountdownRoutine()
    {
        // This suspends the following lines of code for a number of seconds
        // set by PowerupTimer
        yield return new WaitForSeconds(PowerupTimer);
        // After waiting for a number of seconds
        // hasPowerup is set to false removing power up abilities
        hasPowerup = false;
        // the powerup ability indicator is removed to signify the powerUp has worn off
        Destroy(cloneIndicator);
    }

    // this checks to see if the player collides with the enemy while the player
    // has powerUp abilities, and if so, it multiplies the resultant momentum
    // of the enemy's collision. This pushes the enemy away from the player very fast
    // on collisions.
    private void OnCollisionEnter(Collision collision)
    {
        // if the player collides with the enemy marble
        // & the player has powerUp abilites
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            // gets that enemy instance's Rigidbody componenet
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            // gets a direction vector that is opposite to the direction vector
            // between the enemy and the player
            Vector3 awayFromPlayer = collision.gameObject.transform.position - gameObject.transform.position;
            // Add a Force Impulse multiplier to the enemy Rigidbody to push it away from the player
            // the PowerupStrength determines the magnitude of the Force Push
            enemyRb.AddForce(awayFromPlayer * PowerupStrength, ForceMode.Impulse);
            // Debug log checks
            Debug.Log("Collided with: " + collision.gameObject.name + " with Powerup set to " + hasPowerup);
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<AudioManager>().Play("MarbleClick");
        }
    }
}