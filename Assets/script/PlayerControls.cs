using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    public bool hasPowerup;
    private float PowerupStrength = 15.0f;
    public GameObject powerupIndicator;
    public GameObject missilePrefab;

    public PowerUpType currentPowerUp = PowerUpType.None; 

    private GameObject tmpMissile;
    Coroutine powerupCountdown;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focul point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
         
        if (transform.position.y < -10)
        { 
            transform.position = new Vector3(0, 0, 0);
        }
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        
        if(Input.GetKeyDown(KeyCode.Space))
        { 
           foreach(var enemy in FindObjectsOfType<Enemy>())
           {
                
                var tmpMissile = Instantiate(missilePrefab, transform.position, missilePrefab.transform.rotation);
                tmpMissile.GetComponent<Missile>().Fire(enemy.transform);
            }
            //launch projectile from the player//
            
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<Powerup>().powerUpType;
            Destroy(other.gameObject);

            if(powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }
    void OnCollisionEnter(Collision collision)
     {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collision with" + collision.gameObject.name + "with power set to" + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * PowerupStrength, ForceMode.Impulse);
            Debug.Log("Player collided with" + collision.gameObject.name + "with power set to " + currentPowerUp.ToString());
        }
     }
}

