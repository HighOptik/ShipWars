using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{
    public float lifeTimer;
    public float lifeTime;
    public GameObject muzzPrefab;
    public int damage;
    public int moveDir;
    public int moveSpeed;
    public bool isHomingProjectile;
    public bool isLaserProjectile;
    private float effecttimer = .25f;
    private float muzzeltimer = 0;

    public Vector3 target;
    public Collider2D ignoreCollider { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != ignoreCollider)
        {
            PoolManager.Instance.PoolObject(gameObject);
        }        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Boss" || collision.gameObject.layer == LayerMask.NameToLayer("PlayerUnit"))
        {
            Vector3 hitpos = collision.contacts[0].point;
            UnitControl unitObj = collision.gameObject.GetComponent<UnitControl>();
            GameObject Explosion = PoolManager.Instance.GetObjectForType("ImpactExp", false);
            Explosion.transform.position = hitpos;
            PoolManager.Instance.PoolObject(this.gameObject);
            lifeTimer = 0;
            unitObj.RecieveDamage(damage);
            unitObj.Updatehealth();           
        }
    }
    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Boss").transform.position;

        if (isHomingProjectile)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        else
        if (isLaserProjectile)
        {
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, new Vector3(10, transform.position.y, 0));
            RaycastHit hit;

            muzzeltimer += Time.deltaTime;
            if (muzzeltimer >= .25f)
            {
                GameObject muzzleExplo = PoolManager.Instance.GetObjectForType("MuzzelExpSML", false);
                muzzleExplo.transform.position = transform.position;
                muzzeltimer = 0;
            } 
            
            if (Physics.Raycast(transform.position, new Vector3(transform.position.x + 15, 0, 0), out hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("BossUnit"))
                {
                    GetComponent<LineRenderer>().SetPosition(0, transform.position);
                    GetComponent<LineRenderer>().SetPosition(1, new Vector3(hit.point.x + 1, hit.point.y, hit.point.z));
                    UnitControl unitObj = hit.collider.gameObject.GetComponent<UnitControl>();


                    effecttimer += Time.deltaTime;
                    if (effecttimer >= .25f)
                    {
                        GameObject explo = PoolManager.Instance.GetObjectForType("ImpactExp", false);
                        explo.transform.position = new Vector3(hit.point.x + 1, hit.point.y, hit.point.z);
                        effecttimer = 0;
                        unitObj.RecieveDamage(damage);
                    }
                }
            }
        }
        else if(!isHomingProjectile && !isLaserProjectile)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.localPosition.x + moveDir, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }

        lifeTimer += Time.deltaTime;
        if (lifeTimer> lifeTime)
        {
            PoolManager.Instance.PoolObject(this.gameObject);
            lifeTimer = 0;
        }
    }
}