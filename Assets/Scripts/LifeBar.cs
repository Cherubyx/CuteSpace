using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {

	ShipControl ship;
	public float maxScale = 10.0f;
	public TextMesh text;
	
	// Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3(maxScale,1f,1f);
		ship = this.GetComponentInParent<StatusBar>().ship;
	}
	
	// Update is called once per frame
	void Update () {
		if(ship != null){
			float fullRatio = ship.HP / ship.maxHP;
			this.transform.localScale = new Vector3(fullRatio * maxScale,1f,1f);
			text.text = (int)ship.HP + " / " + ship.maxHP;
		}
		else{
			Destroy (this.gameObject);
		}
	}
}
