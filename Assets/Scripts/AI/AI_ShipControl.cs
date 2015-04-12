using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_ShipControl : MonoBehaviour {

	//Reference to the ship controls
	protected ShipControl shipControl;

	//The AI will ignore entities beyond this radius
	public float awarenessRadius = 20f;

	protected List<ShipControl> getNearbyEnemies(){
		List<ShipControl> shipList = new List<ShipControl>();
		Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(this.transform.position,awarenessRadius);
		foreach(Collider2D nearbyObject in nearbyObjects){
			if(nearbyObject.gameObject != this.gameObject && nearbyObject.GetComponent<ShipControl>() != null && isEnemy (nearbyObject.GetComponent<ShipControl>())){
				shipList.Add(nearbyObject.GetComponent<ShipControl>());
			}
		}
		return shipList;
	}

	protected bool isEnemy(ShipControl ship){
		return PersistentGameData.factionEnemies[(int)shipControl.faction,(int)ship.faction];
	}
}
