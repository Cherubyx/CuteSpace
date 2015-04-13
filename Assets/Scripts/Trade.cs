using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Trade : MonoBehaviour {

  /**
   * Hold the name of the trading partner.
   */
  private string partnerName;

  /**
   * Holds the inventory of the player and the partner for the duration of the
   * transaction.
   */
  private List<Item> playerInventory;
  private List<Item> partnerInventory;

  /**
   * Hold the text mesh objects for the UI.
   */
  private Text playerNameText;
  private Text partnerNameText;
  private Text cheezburgerCountText;
  private Text dogecoinCountText;


  private List<Item> GetInventoryFromXML(string name) {
    // Determine the path to the XML file.
    string path = name+"_inventory";

    // Open the XML file as a text asset
    TextAsset xmlAsset = Resources.Load(path) as TextAsset;

    // If we can't find the specified file, then return an empty list.
    if (xmlAsset == null) return new List<Item>();

    // Deserialize the XML text into an InventoryCollection instance.
    InventoryCollection collection = InventoryCollection.DeserializeFromXmlString(xmlAsset.ToString());

    return collection.items;
  }


  private void UpdateCurrencyCounts() {
    this.cheezburgerCountText.text = "x" + PersistentGameData.cheezburgerCount;
    this.dogecoinCountText.text = "x" + PersistentGameData.dogecoinCount;
  }


  private void SetupScene() {
    // Display the partner name.
    this.partnerNameText.text = this.partnerName;

    // Display the player name.
    this.playerNameText.text = PersistentGameData.playerName;

    // Display the amount of currency the player has.
    this.UpdateCurrencyCounts();
  }


  private void Awake() {
    // Get the name of our trading partner.
    this.partnerName = PersistentGameData.partnerName;

    // We should handle the potential case that the trade scene is loaded without a trading partner
    // having been specified.
    if (this.partnerName == null || this.partnerName.Equals("")) {
      // But for now, we'll just have an error message in the editor.
      Debug.LogError("No trading partner specified.");
    }

    // Get the trading partner's inventory.
    this.partnerInventory = this.GetInventoryFromXML(this.partnerName);

    // Get the player's inventory. If there is no stored instance of the inventory then we'll need to
    // retrieve the saved inventory from the XML file, if possible.
    this.playerInventory = PersistentGameData.playerItems ?? this.GetInventoryFromXML(PersistentGameData.playerName);

    // Get the UI objects.
    this.partnerNameText = GameObject.Find("Trading Partner Name").GetComponent<Text>();
    this.playerNameText = GameObject.Find("Player Name").GetComponent<Text>();
    this.cheezburgerCountText = GameObject.Find("Cheezburger Count").GetComponent<Text>();
    this.dogecoinCountText = GameObject.Find("Dogecoin Count").GetComponent<Text>();
  }


  private void Start() {
    this.SetupScene();
  }


  private void Update() {
    this.UpdateCurrencyCounts();
  }

}
