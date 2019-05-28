using Menu.Inventory.ItemDescription;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu.Inventory
{
	/// <summary>
	/// Additionaly show the item description when selected
	/// </summary>
	public class InventoryItemStackHUD : ItemStackHUD
	{
		[SerializeField] private ItemDescriptionHUD _description;

		#region OnSelect/Deselect
		public void OnSelected(BaseEventData eventData)
		{
			DisplayDescription();
		}

		public void OnDeselected(BaseEventData eventData)
		{
			HideDescription();
		}
		
		public override void OnPointerEnter(PointerEventData eventData)
		{
			DisplayDescription();
			base.OnPointerEnter(eventData);
		}

		public override void OnPointerExit(PointerEventData eventData)
		{
			HideDescription();
			base.OnPointerExit(eventData);
		}

		private void DisplayDescription()
		{
			if (_description != null && ActualStack != null)
				_description.SetItem(ActualStack.Content);
		}

		private void HideDescription()
		{
			if (_description != null && ActualStack != null &&
			    _description.DisplayedItem == ActualStack.Content)
				_description.SetItem(null);	
		}
		#endregion
	}
}
