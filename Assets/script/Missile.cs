using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed;
    private bool homing;
    public Transform enemy;

    void Awake()
    {
        Debug.Log("Spawn");
    }
   
    
    // Update is called once per frame
    public void Fire(Transform newTarget)
    {
        enemy = newTarget;
        homing = true;
        Destroy (gameObject, 4);   
    }

    void Update()
    {
        if(homing && enemy != null)
        {
            Vector3 moveDirection = (enemy.transform.position - transform.position).normalized;

            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(enemy);
        }
    }
}
