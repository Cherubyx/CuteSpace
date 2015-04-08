using UnityEngine;
using System.Collections;

public class ColorCycle : MonoBehaviour {

	public Color color1;
	public Color color2;
	public float colorCycleOffset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<SpriteRenderer>().color = Color.Lerp(color1,color2,MathHelper.functionA(Time.time + colorCycleOffset));	
	}
}
