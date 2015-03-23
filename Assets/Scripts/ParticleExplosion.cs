using UnityEngine;
using System.Collections;

public class ParticleExplosion : MonoBehaviour {

	public string sortingLayerName = "Foreground";
	public int sortingLayerOrder = 0;
	public float lifeTime = 10.0f;
	public bool limitedLife = true;
	
	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = sortingLayerName;
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = sortingLayerOrder;
	}
	
	// Update is called once per frame
	void Update () {
		if(limitedLife){
			lifeTime -= Time.deltaTime;
			if(lifeTime <= 0.0f){
				Destroy(this.gameObject);
			}
		}
	}
}
