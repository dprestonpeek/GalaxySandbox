using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> objsAffected = new List<GameObject>();
    [SerializeField]
    float defaultForce = 10000;
    [SerializeField]
    Transform defaultLocation;
    [SerializeField]
    float defaultRadius = 10000;

    [SerializeField]
    bool manualControl = false;

    List<Rigidbody> rbs = new List<Rigidbody>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manualControl)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ProduceShockwave();
            }
        }
    }

    public void ProduceShockwave()
    {
        ProduceShockwave(defaultForce, defaultLocation, defaultRadius);
    }

    public void ProduceShockwave(float force, Transform location, float radius)
    {
        if (!CheckObjsForRigidbodies())
            return;

        foreach (Rigidbody rb in rbs)
        {
            rb.AddExplosionForce(force, location.position, radius);
        }
    }

    private bool CheckObjsForRigidbodies()
    {
        for (int i = 0; i < objsAffected.Count; i++)
        {
            //get the rigidbody from the obj
            Rigidbody rb = objsAffected[i].GetComponent<Rigidbody>();

            if (!rb) //if rb does not exist, see if it has children with rigidbodies
            {
                Rigidbody[] childRbs = objsAffected[i].GetComponentsInChildren<Rigidbody>();
                //if not, give up
                if (childRbs.Length == 0)
                    return false;
                //otherwise, remove this obj now that we have the rigidbodies
                objsAffected.Remove(objsAffected[i]);
                i--;
                rbs.AddRange(childRbs);
            }
            else
            {
                rbs.Add(rb);
            }
        }
        return true;
    }
}
