using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussCannon : MonoBehaviour {

    public float speed = 5f;
    public float rateOfFire = 1f;
    public Rigidbody EquippedTo;

    public List<Transform> targets;
    public float shotDelay = 0f;

    private void Awake()
    {
        targets = new List<Transform>();
    }
    private void Update()
    {
        if(shotDelay <= 0)
        {
            if (targets.Count>0)
            {
             //   ProjectileControl script = bullet.GetComponent<ProjectileControl>();
               // script.ignoreCollider = EquippedTo.GetComponent<Collider>();
             //   bullet.GetComponent<Rigidbody>().velocity = EquippedTo.velocity + ((targets[0].position - transform.position).normalized * speed);
              //  bullet.transform.position = transform.position;
                shotDelay = 1 / rateOfFire;
                Debug.Log("sada");
            }
        }
        else
        {
            shotDelay -= Time.deltaTime;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other != EquippedTo.GetComponent<Collider>())
        {
            targets.Add(other.transform);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other != EquippedTo.GetComponent<Collider>())
        {
            targets.Remove(other.transform);
        }
    }
}