using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Dialogue")]
public class Dialogue{

	[XmlAttribute("NpcName")]
	public string NpcName;

	[XmlArray("DialogueNodes")]
	[XmlArrayItem("DialogueNode")]
	public List<DialogueNode> dialogueNodes;

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
	
	//Loads the xml directly from the given string. Useful in combination with www.text.
	public static Dialogue LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(Dialogue));
		return serializer.Deserialize(new StringReader(text)) as Dialogue;
	}
}
