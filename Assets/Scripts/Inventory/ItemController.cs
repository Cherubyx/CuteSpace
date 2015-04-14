using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemController : MonoBehaviour {

  public Item item;
  public Trade tradeController;

  private Text price;
  private Text quantity;
  private Image currencyBadge;
  private Image itemImage;
  private Button button;

  private void Start() {
    GameObject ItemImage = this.transform.FindChild("ItemImage").gameObject;
    Transform ItemPricing = this.transform.FindChild("ItemPricing");
    Transform Quantity = this.transform.FindChild("Quantity");

    this.itemImage = ItemImage.GetComponent<Image>();

    if (ItemPricing != null) {
      this.price = ItemPricing.FindChild("Price").gameObject.GetComponent<Text>();
      this.currencyBadge = ItemPricing.FindChild("CurrencyBadge").gameObject.GetComponent<Image>();
    }

    if (Quantity != null) {
      this.quantity = Quantity.GetComponent<Text>();
    }

    this.button = this.GetComponent<Button>();

    this.button.onClick.AddListener(() => this.HandleClick());
  }

  private void Update() {
    if (this.item == null) return;

    if (this.price != null) {
      this.price.text = this.item.cost + "";

      int currency = 0;

      if (this.item.currency == Currency.DOGECOIN)
        currency = PersistentGameData.dogecoinCount;
      else
        currency = PersistentGameData.cheezburgerCount;

      if (currency < this.item.cost) {
        Color color = this.GetComponent<Image>().color;
        color.a = 0.3f;
        this.GetComponent<Image>().color = color;

        this.button.enabled = false;
      }
    }

    if (this.quantity != null) this.quantity.text = "x" + this.item.quantity;
  }

  private void HandleClick() {
    // If this is something we can buy...
    if (this.price != null) {
      this.tradeController.PlayerBuysItem(this.item);
      this.item.quantity--;
    }
  }

}
