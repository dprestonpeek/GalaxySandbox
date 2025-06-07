using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class was meant to be a *just* a place to house global functions for the control panel
//has turned into an extension of the camera manager.
//TODO: consolidate camera stuff and move it into CameraManager.cs
public class ControlPanelFuncs : MonoBehaviour
{
    //public static bool isPaused = false;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}

    //public static void SwitchShipCam(ShipManager.Ships shipType, CameraManager.CamTargets targetType, string targetName, int targetPos, CameraManager.CamAngles camAngle, bool panY)
    //{
    //    CameraManager.Instance.SetNewTarget(targetType);
    //    ShipManager ship = GetShipManager(shipType, targetPos);
    //    DeactivateShipManagers(shipType, targetPos);
    //    CameraManager.Instance.SetNewTargetObj(ship.transform.Find(targetName).gameObject, targetType);
    //    CameraManager.Instance.SetNewAngle(camAngle);
    //    CameraManager.Instance.camera.GetComponent<CameraMovement>().panY = panY;
    //}

    //public static void Luminaris1LLD()
    //{
    //    CameraManager.Instance.SetNewTarget(CameraManager.CamTargets.Protag);
    //    ShipManager luminaris1 = GetShipManager(ShipManager.Ships.Luminaris, 1);
    //    DeactivateShipManagers(ShipManager.Ships.Luminaris, 1);
    //    CameraManager.Instance.SetNewTargetObj(luminaris1.transform.Find("ProtagShipHolder").gameObject, CameraManager.CamTargets.Protag);
    //    CameraManager.Instance.SetNewAngle(CameraManager.CamAngles.LowerLeftDrop);
    //    CameraManager.Instance.camera.GetComponent<CameraMovement>().panY = true;
    //}

    //public static void Luminaris1DTS()
    //{
    //    CameraManager.Instance.SetNewTarget(CameraManager.CamTargets.Protag);
    //    ShipManager luminaris1 = GetShipManager(ShipManager.Ships.Luminaris, 1);
    //    DeactivateShipManagers(ShipManager.Ships.Luminaris, 1);
    //    CameraManager.Instance.SetNewTargetObj(luminaris1.transform.Find("ProtagShipHolder").gameObject, CameraManager.CamTargets.Protag);
    //    CameraManager.Instance.SetNewAngle(CameraManager.CamAngles.DownTheStreet);
    //    CameraManager.Instance.camera.GetComponent<CameraMovement>().panY = false;
    //}

    //public static void Luminaris1FP()
    //{
    //    CameraManager.Instance.SetNewTarget(CameraManager.CamTargets.Protag);
    //    ShipManager luminaris2 = GetShipManager(ShipManager.Ships.Luminaris, 1);
    //    DeactivateShipManagers(ShipManager.Ships.Luminaris, 1);
    //    CameraManager.Instance.SetNewTargetObj(luminaris2.transform.Find("ProtagShipHolder").gameObject, CameraManager.CamTargets.Protag);
    //    CameraManager.Instance.SetNewAngle(CameraManager.CamAngles.FirstPerson);
    //    CameraManager.Instance.camera.GetComponent<CameraMovement>().panY = false;
    //}

    //public static void Luminaris2FP()
    //{
    //    CameraManager.Instance.SetNewTarget(CameraManager.CamTargets.Protag);
    //    ShipManager luminaris2 = GetShipManager(ShipManager.Ships.Luminaris, 2);
    //    DeactivateShipManagers(ShipManager.Ships.Luminaris, 2);
    //    CameraManager.Instance.SetNewTargetObj(luminaris2.transform.Find("ProtagShipHolder2").gameObject, CameraManager.CamTargets.Protag);
    //    CameraManager.Instance.SetNewAngle(CameraManager.CamAngles.FirstPerson);
    //    CameraManager.Instance.camera.GetComponent<CameraMovement>().panY = false;
    //}

    //public static void Luminaris3DTS()
    //{
    //    CameraManager.Instance.SetNewTarget(CameraManager.CamTargets.Protag);
    //    ShipManager luminaris3 = GetShipManager(ShipManager.Ships.Luminaris, 3);
    //    DeactivateShipManagers(ShipManager.Ships.Luminaris, 3);
    //    CameraManager.Instance.SetNewTargetObj(luminaris3.transform.Find("ProtagShipHolder3").gameObject, CameraManager.CamTargets.Protag);
    //    CameraManager.Instance.SetNewAngle(CameraManager.CamAngles.DownTheStreet);
    //    CameraManager.Instance.camera.GetComponent<CameraMovement>().panY = false;
    //}

    //public static void Luminaris3PB()
    //{
    //    CameraManager.Instance.SetNewTarget(CameraManager.CamTargets.Protag);
    //    ShipManager luminaris3 = GetShipManager(ShipManager.Ships.Luminaris, 3);
    //    DeactivateShipManagers(ShipManager.Ships.Luminaris, 3);
    //    CameraManager.Instance.SetNewTargetObj(luminaris3.transform.Find("ProtagShipHolder3").gameObject, CameraManager.CamTargets.Protag);
    //    CameraManager.Instance.SetNewAngle(CameraManager.CamAngles.PassingBy);
    //    CameraManager.Instance.camera.GetComponent<CameraMovement>().panY = false;
    //}

    //public static void PlanetGUBR()
    //{
    //    CameraManager.Instance.SetNewTarget(CameraManager.CamTargets.Planet);
    //    CameraManager.Instance.SetNewAngle(CameraManager.CamAngles.UpperBackRight);
    //    CameraManager.Instance.camera.GetComponent<CameraMovement>().panY = true;
    //}

    //public static void PlanetGChase()
    //{
    //    CameraManager.Instance.SetNewTarget(CameraManager.CamTargets.Planet);
    //    CameraManager.Instance.SetNewAngle(CameraManager.CamAngles.ChaseView);
    //    CameraManager.Instance.camera.GetComponent<CameraMovement>().panY = true;
    //}

    //public static void F3_1UFL()
    //{
    //    ShipManager f31 = GetShipManager(ShipManager.Ships.F3, 1);
    //    DeactivateShipManagers(ShipManager.Ships.F3, 1);
    //    CameraManager.Instance.SetNewTargetObj(f31.gameObject, CameraManager.CamTargets.BigBoss);
    //    CameraManager.Instance.SetNewTarget(CameraManager.CamTargets.BigBoss);
    //    CameraManager.Instance.SetNewAngle(CameraManager.CamAngles.UpperFrontLeft);
    //    CameraManager.Instance.camera.GetComponent<CameraMovement>().panY = true;
    //}

    //public static void F3_2Chase()
    //{
    //    ShipManager f32 = GetShipManager(ShipManager.Ships.F3, 2);
    //    DeactivateShipManagers(ShipManager.Ships.F3, 2);
    //    CameraManager.Instance.SetNewTargetObj(f32.gameObject, CameraManager.CamTargets.BigBoss);
    //    CameraManager.Instance.SetNewTarget(CameraManager.CamTargets.BigBoss);
    //    CameraManager.Instance.SetNewAngle(CameraManager.CamAngles.ChaseView);
    //    CameraManager.Instance.camera.GetComponent<CameraMovement>().panY = false;
    //}

    //public static void Explosion1()
    //{

    //}

    //private static ShipManager GetShipManager(ShipManager.Ships shipType, int position)
    //{
    //    ShipManager[] ships = FindObjectsOfType<ShipManager>(true);
    //    foreach (ShipManager ship in ships)
    //    {
    //        if (ship.ship == shipType && ship.position == position)
    //        {
    //            return ship;
    //        }
    //    }
    //    return null;
    //}

    //public static List<ShipManager> GetShipManagers(ShipManager.Ships shipType)
    //{
    //    ShipManager[] ships = FindObjectsOfType<ShipManager>(true);
    //    List<ShipManager> shipsOfType = new List<ShipManager>();
    //    foreach (ShipManager ship in ships)
    //    {
    //        if (ship.ship == shipType)
    //        {
    //            shipsOfType.Add(ship);
    //        }
    //    }
    //    return shipsOfType;
    //}

    //private static void DeactivateShipManagers(ShipManager.Ships shipType)
    //{
    //    DeactivateShipManagers(shipType, -1);
    //}

    //private static void DeactivateShipManagers(ShipManager.Ships shipType, int exception)
    //{
    //    List<ShipManager> ships = GetShipManagers(shipType);
    //    List<ShipManager> toDisable = new List<ShipManager>();

    //    //First determine the exception
    //    foreach (ShipManager ship in ships)
    //    {
    //        if (ship.position == exception)
    //        {
    //            ship.gameObject.SetActive(true);
    //            continue;
    //        }
    //        toDisable.Add(ship);
    //    }
    //    //Deactivate other shipManager *after* we've enabled our exception
    //    foreach (ShipManager ship in toDisable)
    //    {
    //        ship.gameObject.SetActive(false);
    //    }
    //}
}
