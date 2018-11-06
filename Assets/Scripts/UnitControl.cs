using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitControl : MonoBehaviour {
    public GameControl GCScript;

    public GameObject[] projectilePrefab;
    public Transform[] firePos;
    public Canvas GameHUD;
    public int fpIndex = 0;
    public int unitCost;
    public float hbOffset;
    public GameObject healthBar;

    public Vector3 maxPos;
    public Vector3 minPos;

    public bool up;
    public bool down;
    public bool isDead;
    public bool isBoss;


    public float timer;

    [Header("UNIT STATS")]
    public Slider unitHealthBar;
    public int movementSpeed;
    public int health;
    public int ogHeath;
    public int fireRate;
    public int bullet1Count;
    public int bullet2Count;
    public int bulletIndex;
    public float bulletInterval;

    void Start()
    {
        maxPos = new Vector3(transform.position.x, 13, transform.position.z);
        minPos = new Vector3(transform.position.x, 7, transform.position.z);
        GameHUD = GameObject.FindGameObjectWithTag("GameHUD").GetComponent<Canvas>();
        GameObject healthBar = PoolManager.Instance.GetObjectForType("UnitHealthSlider", false);
        unitHealthBar = healthBar.GetComponent<Slider>();
        healthBar.transform.SetParent(GameHUD.transform, false);
        health = ogHeath;
        unitHealthBar.maxValue = ogHeath;
        unitHealthBar.value = ogHeath;
        Updatehealth();

        foreach (Transform fp in transform)
        {
            if (fp.gameObject.name == "firePos")
            {
                firePos[fpIndex] = fp.transform;
                fpIndex++;
            }
        }
    }
    void FixedUpdate()
    {
        unitHealthBar.transform.position = new Vector3(transform.position.x, transform.position.y+hbOffset, transform.position.z);
        //checking what direction should be applied while making sure it does not exceed the positions
        if (up)
        {
            transform.position = Vector3.MoveTowards(transform.position, maxPos, movementSpeed * Time.deltaTime);
        }
        if (down)
        {
            transform.position = Vector3.MoveTowards(transform.position, minPos, movementSpeed * Time.deltaTime);
        }
        if (transform.position == maxPos)
        {
            up = false;
            down = true;
        }
        if (transform.position == minPos)
        {
            up = true;
            down = false;
        }

         //timer for shooting
         timer += Time.deltaTime;
         if(timer >= fireRate)
         {
            StartCoroutine(Fire());
             timer = 0;
         }
     }

    //fireFunction
    public IEnumerator Fire()
    {
        if (!isDead)
        {
            int firePosIndex = 0;
            bulletIndex = 0;

            for (int a = 0; a < bullet1Count; a++)
            {
                GameObject bullet = PoolManager.Instance.GetObjectForType(projectilePrefab[bulletIndex].name, false);

                if (bullet.name == "Projectile_Laser" || bullet.name == "Projectile_PulseLaser")
                {
                    bullet.transform.parent = transform;
                    bullet.transform.localPosition = firePos[firePosIndex].localPosition;

                    firePosIndex++;
                    if (firePosIndex == firePos.Length)
                    {
                        firePosIndex = 0;
                    }
                    yield return new WaitForSeconds(bulletInterval);
                }
                else
                {
                    bullet.transform.position = firePos[firePosIndex].position;

                    firePosIndex++;
                    if (firePosIndex == firePos.Length)
                    {
                        firePosIndex = 0;
                    }
                    yield return new WaitForSeconds(bulletInterval);
                }
            }
            for (int b = 0; b < bullet2Count; b++)
            {
                bulletIndex = 1;
                GameObject bullet = PoolManager.Instance.GetObjectForType(projectilePrefab[bulletIndex].name, false);
                if (bullet.name == "Projectile_Laser" || bullet.name == "Projectile_PulseLaser")
                {
                    bullet.transform.parent = transform;
                    bullet.transform.localPosition = firePos[firePosIndex].localPosition;
                }
                else
                {
                    bullet.transform.position = firePos[firePosIndex].position;

                    firePosIndex++;
                    if (firePosIndex == firePos.Length)
                    {
                        firePosIndex = 0;
                    }
                    yield return new WaitForSeconds(bulletInterval);
                }
            }
        }
    }
    public void Updatehealth()
    {
        unitHealthBar.value = health;
    }
    public void RecieveDamage(int dmg)
    {
        StartCoroutine(_RecieveDamage(dmg));
    }
    public IEnumerator _RecieveDamage(int dmg)
    {
        health -= dmg;
        Updatehealth();
        GetComponentInChildren<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(.1f);
        GetComponentInChildren<Renderer>().material.color = Color.white;

        if (health<=0)
        {
            StartCoroutine(UnitDestroyed());
            isDead = true;
        }
        if (isBoss)
        {
            GCScript.bossDestroyed = true;
        }
    }

    public IEnumerator UnitDestroyed()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        GameObject explo = PoolManager.Instance.GetObjectForType("ImpactExp", false);
        explo.transform.position = this.transform.position;
        yield return new WaitForSeconds(.2f);
        PoolManager.Instance.PoolObject(this.gameObject);
    }
}