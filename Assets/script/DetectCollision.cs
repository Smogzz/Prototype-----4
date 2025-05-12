using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    public GameObject enemyprefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        var missile = GetComponent<Missile>();
        if(missile.enemy != null)
        {
            if(other.gameObject.CompareTag(missile.enemy.tag))
            {
                Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -other.contacts[0].normal;
                targetRigidbody.AddForce(away * 10, ForceMode.Impulse);
                Destroy(gameObject);
                Destroy(other.gameObject);

            }
        }
    }
}
