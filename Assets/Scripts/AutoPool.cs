using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPool : MonoBehaviour {
    public float lifeTime;
    private void OnEnable()
    {
        StartCoroutine(PoolObject());
    }
    // Use this for initialization
    IEnumerator PoolObject () {
        yield return new WaitForSeconds(lifeTime);
        PoolManager.Instance.PoolObject(gameObject);
    }
}
