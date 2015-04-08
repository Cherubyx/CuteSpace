using UnityEngine;
using System.Collections;

public class ParticleSortLayer : MonoBehaviour {

	public string sortingLayerName = "Foreground";
	public int sortingLayerOrder = 0;

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = sortingLayerName;
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = sortingLayerOrder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
