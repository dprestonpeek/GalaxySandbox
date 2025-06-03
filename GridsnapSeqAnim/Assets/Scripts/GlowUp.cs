using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowUp : MonoBehaviour
{
    [SerializeField]
    bool manualControl = false;
    [SerializeField]
    float amount = .1f;
    [SerializeField]
    float maxSize = 1;

    bool glowUp = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manualControl)
        {
            if (Input.GetButton("Fire1"))
            {
                DoGlowUp(amount);
            }
        }
        if (glowUp)
        {
            DoGlowUp(amount);
        }
    }

    public void StartGlowUp()
    {
        glowUp = true;
    }

    void DoGlowUp(float amount)
    {
        if (transform.localScale.x < maxSize)
        {
            Vector3 newScale = transform.localScale;
            newScale.x += amount;
            newScale.y += amount;
            newScale.z += amount;
            transform.localScale = newScale;
        }
    }
}
