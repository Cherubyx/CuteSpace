using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Trade : MonoBehaviour {

  public GameObject playerItemPrefab;
  public GameObject partnerItemPrefab;
  public Image partnerImage;

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
    this.cheezburgerCountText.text = "" + PersistentGameData.cheezburgerCount;
    this.dogecoinCountText.text = "" + PersistentGameData.dogecoinCount;
  }


  private void DrawPlayerInventory() {
    GameObject parent = GameObject.Find("PlayerInventories");

    int row = 0, col = 0;

    for (int i = 1; i <= this.playerInventory.Count; i++) {
      // Update our position according to the row and col.
      Vector3 position = new Vector3(-28.8f, 5.8f, 0f);
      position.x += 30 * col;
      position.y -= 30 * row;

      // Get Item instance
      Item item = this.playerInventory[i - 1];

      // Create our badge.
      GameObject badge = GameObject.Instantiate<GameObject>(this.playerItemPrefab);
      badge.transform.SetParent(parent.transform);
      badge.transform.localPosition = position;
      badge.transform.localScale = Vector3.one;

      // Set Item instance.
      badge.GetComponent<ItemController>().item = item;
      badge.GetComponent<ItemController>().tradeController = this;

      // Get the child objects of the badge.
      Image image = badge.transform.Find("ItemImage").gameObject.GetComponent<Image>();

      // Attempt to create a sprite for the image.
      Sprite imageSprite = Resources.Load<Sprite>("Sprites/Trade/" + item.name);

      // Set the correct image.
      if (imageSprite != null) {
        image.sprite = imageSprite;
      }

      col++;

      // Make sure we wrap for every 3 items.
      if ((i % 3) == 0) {
        row++;
        col = 0;
      }
    }
  }


  private void DrawPartnerInventory() {
    GameObject parent = GameObject.Find("TradeInventories");

    int row = 0, col = 0;

    for (int i = 1; i <= this.partnerInventory.Count; i++) {
      // Update our position according to the row and col.
      Vector3 position = new Vector3(-498f, -32f, 0f);
      position.x += 130 * col;
      position.y -= 162 * row;

      // Get the Item instance.
      Item item = this.partnerInventory[i - 1];

      // Create our badge.
      GameObject badge = GameObject.Instantiate<GameObject>(this.partnerItemPrefab);
      badge.transform.SetParent(parent.transform);
      badge.transform.localPosition = position;
      badge.transform.localScale = Vector3.one;

      // Set the Item instance
      badge.GetComponent<ItemController>().item = item;
      badge.GetComponent<ItemController>().tradeController = this;

      // Get the child objects.
      Image itemImage = badge.transform.FindChild("ItemImage").gameObject.GetComponent<Image>();
      Image currencyBadge = badge.transform.FindChild("ItemPricing").FindChild("CurrencyBadge").gameObject.GetComponent<Image>();

      // Attempt to create a sprite for the image.
      Sprite imageSprite = Resources.Load<Sprite>("Sprites/Trade/" + item.name);

      if (item.currency != Currency.DOGECOIN) {
        currencyBadge.sprite = Resources.Load<Sprite>("Sprites/cheezburger");
      }

      if (imageSprite != null) {
        itemImage.sprite = imageSprite;
      }

      col++;

      // Make sure we wrap for every 5 items.
      if ((i % 5) == 0) {
        row++;
        col = 0;
      }
    }
  }

  private void DrawInventories() {
    this.DrawPlayerInventory();
    this.DrawPartnerInventory();
  }


  private void SetupScene() {
    // Display the partner name.
    this.partnerNameText.text = this.partnerName.ToUpperInvariant();

    // Display the amount of currency the player has.
    this.UpdateCurrencyCounts();

    // Display the inventory.
    this.DrawInventories();
  }


  private void Awake() {
    // Get the name of our trading partner.
    this.partnerName = PersistentGameData.partnerName;

    // Get the image of our trading partner (latest)
    this.partnerImage.sprite = PersistentGameData.partnerAvatar;

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
    this.cheezburgerCountText = GameObject.Find("Cheezburger Count").GetComponent<Text>();
    this.dogecoinCountText = GameObject.Find("Dogecoin Count").GetComponent<Text>();
  }


  private void Start() {
    this.SetupScene();
  }


  private void Update() {
    this.UpdateCurrencyCounts();
  }

  public void PlayerBuysItem(Item item) {
    // Deduct the amount from the player wallet.
    if (item.currency == Currency.DOGECOIN) {
      PersistentGameData.dogecoinCount -= item.cost;
    } else {
      PersistentGameData.cheezburgerCount -= item.cost;
    }

    // See if the player already has this item in their inventory.
    Item playerItem = null;

    foreach (Item _playerItem in this.playerInventory) {
      if (_playerItem.name == item.name) playerItem = _playerItem;
    }

    // If they do, then just increment the quantity.
    if (playerItem != null) {
      playerItem.quantity++;
      return;
    }

    // If not, then create a new item.
    playerItem = new Item();
    playerItem.name = item.name;
    playerItem.quantity = 1;
    playerItem.cost = item.cost;
    playerItem.currencyName = item.currencyName;

    // Add the item to their inventory.
    this.playerInventory.Add(playerItem);

    // Store the inventory to the persistent store.
    PersistentGameData.playerItems = this.playerInventory;

    // Redraw the inventory.

    // Delete the user inventory badges.
    GameObject[] playerItemBadges = GameObject.FindGameObjectsWithTag("PlayerItemBadges");

    foreach (GameObject badge in playerItemBadges) {
      Object.Destroy(badge);
    }

    // Redraw player inventory.
    this.DrawPlayerInventory();

    // Handle ship items.
    if (item.type == "ship") {
      PersistentGameData.playerFleet.Add(item.name);
      PersistentGameData.playerShipName = item.name;
    }
  }

}
