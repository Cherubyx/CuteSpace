using System.Xml.Serialization;

public class Item {

  [XmlAttribute("cost")]
  public int cost;
  [XmlAttribute("quantity")]
  public int quantity;
  [XmlAttribute("currency")]
  public string currencyName;
  [XmlAttribute("prefabName")]
  public string prefabName;

  public Currency currency {
    get { return (this.currencyName == "dogecoin") ? Currency.DOGECOIN : Currency.CHEEZBURGER; }
    set { this.currencyName = (value == Currency.DOGECOIN) ? "dogecoin" : "cheezburger"; }
  }

}
