using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luminaris : Ship
{

    // Start is called before the first frame update
    void Start()
    {
        forwardMultiplier = 5f;
        upMultiplier = 5f;
        rollMultiplier = 5f;
        fBrakeMultiplier = 5f;
        boostMultiplier = 5f;
    }
}
