using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public Transform m_cannonRot;
	public Transform m_muzzle;
	public GameObject m_shotPrefab;
	public Texture2D m_guiTexture;

	[SerializeField]
	bool manualControl = false;
	[SerializeField]
	bool continuousFire = false;
	bool continuouslyFiring = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (manualControl)
		{
			if (continuousFire)
			{
				if (Input.GetButton("Fire1"))
				{
					Fire();
				}
			}
			else
			{
				if (Input.GetButtonDown("Fire1"))
				{
					Fire();
				}
			}
		}
		if (continuouslyFiring)
        {
			Fire();
        }
	}

	void OnGUI()
	{
		//GUI.DrawTexture(new Rect(0f, 0f, m_guiTexture.width / 2, m_guiTexture.height / 2), m_guiTexture);
	}

	private void Rotate(bool toTheRight)
    {
		if (toTheRight)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                m_cannonRot.transform.Rotate(Vector3.up, Time.deltaTime * 100f);
            }
        }
		else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                m_cannonRot.transform.Rotate(Vector3.up, -Time.deltaTime * 100f);
            }
        }
	}

	public void Fire()
    {
		GameObject go = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
		GameObject.Destroy(go, 5f);
	}

	public void StartContinuousFire()
    {
		continuouslyFiring = true;
    }

	public void StopContinuousFire()
    {
		continuouslyFiring = false;
    }
}
