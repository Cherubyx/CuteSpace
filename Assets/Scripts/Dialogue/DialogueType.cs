using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class DialogueType {

    [XmlAttribute("Target")]
    public string Target;

    [XmlArray("DialogueNodes")]
    [XmlArrayItem("DialogueNode")]
    public List<DialogueNode> dialogueNodes;
}
