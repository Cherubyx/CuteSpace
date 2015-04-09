using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Scene")]
public class ScenePrefabCollection{

	[XmlArray("ScenePrefabs")]
	[XmlArrayItem("ScenePrefab")]
	public List<ScenePrefab> scenePrefabs;

	//Loads the xml directly from the given string. We are currently using this
	public static ScenePrefabCollection LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(ScenePrefabCollection));
		return serializer.Deserialize(new StringReader(text)) as ScenePrefabCollection;
	}
}
