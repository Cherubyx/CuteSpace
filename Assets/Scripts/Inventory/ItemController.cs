using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemController : MonoBehaviour {

  public Item item;

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

    if (this.button != null)
      this.button.onClick.AddListener(() => { Debug.Log("Click"); });
  }

  private void Update() {
    if (this.item == null) return;

    if (this.price != null) this.price.text = this.item.cost + "";
    if (this.quantity != null) this.quantity.text = "x" + this.item.quantity;
  }



}
