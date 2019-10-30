using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Menu.Inventory
{
	/// <summary>
	/// Additionaly show the item description when selected
	/// </summary>
	public class ItemStackActionHUD : ItemStackHUD
	{		
		[SerializeField] private GameObject _description = null;
		[SerializeField] private Text		_descriptionText = null;

		private InputAction _listenedAction;
		
		public void SetAction(InputAction action)
		{
			if (_listenedAction != null)
			{
			}

			_listenedAction = action;
			
			if (_listenedAction != null)
			{
				_descriptionText.text = _listenedAction.controls[0].name;
			}
		}

		public void Select(bool selected)
		{
			if (selected)
				Button.Select();
		}
	}
}
