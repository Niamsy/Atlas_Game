using Game.Item;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Inventory.ItemDescription
{
	public abstract class ASubItemDescriptionHUD : MonoBehaviour
	{
		public abstract void SetItem(ItemAbstract item);
		public abstract void UpdateDisplay();
	}
	
	public class ItemDescriptionHUD : MonoBehaviour
	{
		#region Variables
		[SerializeField] private Image		_sprite;
		[SerializeField] private Text		_name;
		[SerializeField] private Text		_description;
		private ItemAbstract				_item;
		private ASubItemDescriptionHUD[]	_subDescriptionHUD;
		#endregion
		
		#region Methods
		private void Awake()
		{
			_subDescriptionHUD = GetComponentsInChildren<ASubItemDescriptionHUD>();
			UpdateDisplay();
		}

		public void Reset()
		{
			SetItem(null);
		}
		
		public void SetItem(ItemAbstract item)
		{
			_item = item;
			
			foreach (var subDescriptionHUD in _subDescriptionHUD)
				subDescriptionHUD.SetItem(_item);
			UpdateDisplay();
		}
		
		public void UpdateDisplay()
		{
			gameObject.SetActive(_item != null);

			if (_item == null)
				return;

			_name.text = _item.Name;
			_description.text = _item.Description;
			_sprite.sprite = _item.Sprite;
		}
		
		#endregion
	}
}
