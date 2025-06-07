using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [SerializeField]
    public int position;

    [SerializeField]
    List<GameObject> shipObjs;

    [SerializeField]
    GameObject shipHolder;

    GameObject shipObj;
    Ship shipInfo;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (shipObj == null)
        //{
        //    if (!GetShip())
        //    {
        //        SpawnShip();
        //    }
        //}
        //if (ship != shipInfo.shipType)
        //{
        //    Destroy(shipObj);
        //    SpawnShip();
        //}

    }

    //void SpawnShip()
    //{
    //    shipObj = Instantiate(shipObjs[(int)ship], shipHolder.transform);
    //    shipInfo = shipObj.GetComponent<Ship>();
    //}

    //bool GetShip()
    //{
    //    shipInfo = GetComponentInChildren<Ship>();
    //    if (shipInfo)
    //    {
    //        shipObj = shipInfo.gameObject;
    //        return true;
    //    }
    //    return false;
    //}
}
