using UnityEngine;
using System.Collections;

public class ArmorPickup : MonoBehaviour {

	private int healthBonus = 10;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {
		ShipControl ship = other.GetComponent<ShipControl> ();
		if (ship != null && ship.HP < ship.maxHP) {
			ship.HP = Mathf.Min(ship.HP+healthBonus,ship.maxHP);
			Destroy (this.gameObject);
		}

	}
}
