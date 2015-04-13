using System.Xml.Serialization;

public class Item {

  [XmlAttribute("description")]
  public string description;

  [XmlAttribute("quantity")]
  public int quantity;

  [XmlAttribute("cost")]
  public int cost;

  [XmlAttribute("currency")]
  public string currencyName;

  public Currency Currency {
    get {
      return (this.currencyName == "dogecoin") ? Currency.DOGECOIN : Currency.CHEEZBURGER;
    }
  }

}
