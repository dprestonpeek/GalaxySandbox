using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Explosions;
    public float detailLevel = 1.0f;
    public float explosionLife = 10;

    [SerializeField]
    Vector3 location = new Vector3(0, 0, 0);

    [SerializeField]
    bool manualControl = false;
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
                SpawnExplosion();
            }
        }
    }

    public void SpawnExplosion()
    {
        foreach (GameObject explosionFX in Explosions)
        {
            Detonator dTemp = (Detonator)explosionFX.GetComponent("Detonator");

            float offsetSize = dTemp.size / 3;
            GameObject exp = (GameObject)Instantiate(explosionFX, location, Quaternion.identity);
            exp.transform.localScale = Vector3.one * 5;
            dTemp = (Detonator)exp.GetComponent("Detonator");
            dTemp.detail = detailLevel;

            Destroy(exp, explosionLife);
        }
    }
}
