using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Dialogue")]
public class Dialogue{

	[XmlAttribute("NpcName")]
	public string NpcName;

    [XmlArray("DialogueTypes")]
    [XmlArrayItem("DialogueType")]
    public List<DialogueType> dialogueTypes;

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(Dialogue));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static Dialogue Load(string path)
	{
		var serializer = new XmlSerializer(typeof(Dialogue));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as Dialogue;
		}
	}

	//Loads the xml directly from the given string. We are currently using this
	public static Dialogue LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(Dialogue));
		return serializer.Deserialize(new StringReader(text)) as Dialogue;
	}
}
