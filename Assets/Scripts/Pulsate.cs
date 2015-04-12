using UnityEngine;
using System.Collections;

public class Pulsate : MonoBehaviour {

	public Color color1;
	public Color color2;
	public float maxSizeFactor = 1.2f;
	public float colorCycleOffset;
	Vector3 startScale;

	// Use this for initialization
	void Start () {
		startScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<SpriteRenderer>().color = Color.Lerp(color1,color2,MathHelper.functionA(Time.time + colorCycleOffset));		
		float scaleScalar = Mathf.Lerp(maxSizeFactor,1.0f,MathHelper.functionA(Time.time + colorCycleOffset));
		this.transform.localScale = startScale * scaleScalar;	
	}
}
