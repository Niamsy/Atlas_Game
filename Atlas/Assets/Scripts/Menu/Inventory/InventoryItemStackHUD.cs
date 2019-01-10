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
            if (ActualStack != null && _description != null)
			    _description.SetItem(ActualStack.Content);
		}

		public void OnDeselected(BaseEventData eventData)
		{
            if (_description != null)
			    _description.SetItem(null);
		}
		#endregion
	}
}
