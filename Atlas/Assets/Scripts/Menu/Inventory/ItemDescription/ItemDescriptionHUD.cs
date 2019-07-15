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
		[SerializeField] private Image		_sprite = null;
		[SerializeField] private Text		_name = null;
		[SerializeField] private Text		_description = null;
		private ItemAbstract				_item = null;
		public ItemAbstract DisplayedItem => (_item);
		private ASubItemDescriptionHUD[]	_subDescriptionHUD = null;
		#endregion
		
		#region Methods
		private void Awake()
		{
			if (_subDescriptionHUD == null)
				Init();
				UpdateDisplay();
		}

		private void Init()
		{
			_subDescriptionHUD = GetComponentsInChildren<ASubItemDescriptionHUD>();
		}
		
		public void Reset()
		{
			SetItem(null);
		}
		
		public void SetItem(ItemAbstract item)
		{
			_item = item;

			if (_subDescriptionHUD == null)
				Init();
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
