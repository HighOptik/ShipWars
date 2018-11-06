using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacementControl : MonoBehaviour
{
    public PlayerCurrencyControl PCCScript;
    private string objName;
    public bool readyToPlace;

    private void Start()
    {
        PCCScript = GetComponent<PlayerCurrencyControl>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && readyToPlace)
        {
            RaycastHit hit;
            Vector3 wordPos;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                wordPos = hit.point;
                readyToPlace = false;
                GameObject unit = PoolManager.Instance.GetObjectForType(objName, false);
                int cost = unit.GetComponent<UnitControl>().unitCost;
                PCCScript.PayForUnit(cost);
                unit.transform.position = wordPos;
            }
        }
    }
    public void ScoutSelected()
    {
        objName = "Unit_Scout";
        readyToPlace = true;
    }
    public void IntercepterSelected()
    {
        objName = "Unit_Interceptor";
        readyToPlace = true;
    }
    public void FrigateSelected()
    {
        objName = "Unit_Frigate";
        readyToPlace = true;
    }
    public void DestroyerSelected()
    {
        objName = "Unit_Destroyer";
        readyToPlace = true;
    }
    public void AssaultCarrierSelected()
    {
        objName = "Unit_AssaultCarrier";
        readyToPlace = true;
    }
    public void IonCannonSelected()
    {
        objName = "Unit_IonCannon";
        readyToPlace = true;
    }
    public void PulseCannonSelected()
    {
        objName = "Unit_PulseCannon";
        readyToPlace = true;
    }
}
