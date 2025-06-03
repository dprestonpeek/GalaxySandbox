using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GlobalTimer : MonoBehaviour
{
    public static GlobalTimer Instance;
    public float timer1 = -1;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer1 > -1)
        {
            timer1 += Time.deltaTime;
        }
    }
}
