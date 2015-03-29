using UnityEngine;
using System.Collections;

public class EnergyBar : MonoBehaviour {

	ShipControl ship;
	public float maxScale = 5.0f;
	public TextMesh text;

	// Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3(maxScale,1f,1f);
		ship = this.GetComponentInParent<StatusBar>().ship;
	}
	
	// Update is called once per frame
	void Update () {
		if(ship != null){
			float fullRatio = ship.energy / ship.maxEnergy;
			this.transform.localScale = new Vector3(fullRatio * maxScale,1f,1f);
			text.text = (int)ship.energy + " / " + ship.maxEnergy;
		}

	}
}
