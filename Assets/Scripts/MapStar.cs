using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapStar : MonoBehaviour {

	public List<MapStar> adjacentSectors;
	public List<LineRenderer> lines;
	public Color color1;
	public Color color2;
	public float colorCycleOffset;
	public float maxSizeFactor = 1.2f;
	public string sectorName;
	Vector3 startScale;
	float t;
	bool drawConnections;
	// Use this for initialization
	void Start () {
		drawConnections = false;
		startScale = this.transform.localScale;
		foreach(MapStar sector in adjacentSectors){
			GameObject lineObject = new GameObject();
			lineObject.AddComponent<LineRenderer>();
			LineRenderer line = lineObject.GetComponent<LineRenderer>();
			line.SetVertexCount(2);
			line.SetPosition(0,this.gameObject.transform.position);
			line.SetPosition(1,sector.gameObject.transform.position);
			line.SetWidth(0.1f,0.1f);
			string shaderText = "Shader \"Alpha Additive\" {" + "Properties { _Color (\"Main Color\", Color) = (1,1,1,0) }" + "SubShader {" + "	Tags { \"Queue\" = \"Transparent\" }" + "	Pass {" + "		Blend One One ZWrite Off ColorMask RGB" + "		Material { Diffuse [_Color] Ambient [_Color] }" + "		Lighting On" + "		SetTexture [_Dummy] { combine primary double, primary }" + "	}" + "}" + "}";
			line.material = new Material(shaderText);
			line.enabled = false;
			lines.Add(line);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<SpriteRenderer>().color = Color.Lerp(color1,color2,MathHelper.functionA(Time.time + colorCycleOffset));

		float scaleScalar = Mathf.Lerp(maxSizeFactor,1.0f,MathHelper.functionA(Time.time + colorCycleOffset));
		this.transform.localScale = startScale * scaleScalar;

		foreach(LineRenderer line in lines){
			line.enabled = drawConnections;
		}
	}

	void OnMouseEnter() {
		drawConnections = true;
	}

	void OnMouseExit() {
		drawConnections = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		PersistentGameData.overworldOriginPosition = this.transform.position;
		StartCoroutine(WaitAndLoadLevel(3,"Combat"));
		//PersistentGameData.overworldDestinationName = 

	}

	IEnumerator WaitAndLoadLevel(float waitTime, string levelName) {
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(levelName);
	}
}
