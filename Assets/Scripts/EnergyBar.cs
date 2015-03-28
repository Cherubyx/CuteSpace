using UnityEngine;
using System.Collections;

public class EnergyBar : MonoBehaviour {

	public ShipControl ship;
	public float maxScale = 5.0f;

	// Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3(maxScale,1f,1f);
	}
	
	// Update is called once per frame
	void Update () {
		float fullRatio = ship.energy / ship.maxEnergy;
		this.transform.localScale = new Vector3(fullRatio * maxScale,1f,1f);
	}
}
