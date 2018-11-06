using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossControl : MonoBehaviour {
    public Vector3 maxPos;
    public Vector3 minPos;

    public Slider bossHealthBar;
    public float movementSpeed;
    public bool up;
    public bool down;

    public int bossHealth;
    private void Awake()
    {
        bossHealthBar.maxValue = bossHealth;
        Updatehealth();
    }
    void FixedUpdate()
    {
        //checking what direction should be applied while making sure it does not exceed the positions
        if (up)
        {
            transform.position = Vector3.MoveTowards(transform.position, maxPos, movementSpeed * Time.deltaTime);
        }
        if (down)
        {
            transform.position = Vector3.MoveTowards(transform.position, minPos, movementSpeed * Time.deltaTime);
        }
        if(transform.position == maxPos)
        {
            up = false;
            down = true;
        }
        if (transform.position == minPos)
        {
            up = true;
            down = false;
        }
    }
    public IEnumerator _RecieveDamage(int dmg)
    {
        bossHealth -= dmg;
        Updatehealth();
        GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(.1f);
        GetComponentInChildren<MeshRenderer>().material.color = Color.white;

    }
    public void Updatehealth()
    {
            bossHealthBar.value = bossHealth;
    }
    public void RecieveDamage(int dmg)
    {
        StartCoroutine(_RecieveDamage(dmg));
    }
}
