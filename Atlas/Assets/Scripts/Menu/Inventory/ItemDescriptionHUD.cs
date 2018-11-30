using Game.Item;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Inventory
{
	public class ItemDescriptionHUD : MonoBehaviour
	{
		[SerializeField] private Image _sprite;
		[SerializeField] private Text _name;
		[SerializeField] private Text _description;

		private ItemAbstract _item;

		public void SetItem(ItemAbstract item)
		{
			_item = item;

			gameObject.SetActive(item != null);
			
			if (_item != null)
			{
				_name.text = _item.Name;
				_description.text = _item.Description;
				_sprite.sprite = _item.Sprite;
			}
		}
	}
}
