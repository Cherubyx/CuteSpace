using System.Xml.Serialization;

public class Item {

  [XmlAttribute("description")]
  public string description;

  [XmlAttribute("quantity")]
  public int quantity;

}
