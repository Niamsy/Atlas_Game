using System;
using Game.Inventory;
using InputManagement;
using Localization;
using UnityEngine;

namespace Game.Item
{
	[Serializable]
	public abstract class ItemAbstract : ScriptableObject
	{
		[Header("Base item variables")]
		[SerializeField] private int _id;
		public int Id
		{
			get { return (_id); }
		}

		[SerializeField] private GameObject _prefabDroppedGO;
		public GameObject PrefabDroppedGO
		{
			get { return (_prefabDroppedGO); }
		}
		
		protected GameObject EquipedObject;
		[SerializeField] private GameObject _prefabHoldedGO;
		public GameObject PrefabHoldedGO
		{
			get { return (_prefabHoldedGO); }
		}

		public virtual int MaxStackSize
		{
			get { return (100); }
		}

		[SerializeField] private Sprite _sprite;
		public Sprite Sprite
		{
			get { return (_sprite); }
		}
		
		[SerializeField] private LocalizedText _name;
		public string Name
		{
			get { return (_name); }
		}
		
		[SerializeField] private LocalizedText _description;
		public string Description
		{
			get { return (_description); }
		}

		public virtual GameObject Equip(Transform parent)
		{
			EquipedObject = Instantiate(PrefabHoldedGO, parent);
			return (EquipedObject);
		}

		public virtual void UnEquip()
		{
			if (EquipedObject != null)
				Destroy(EquipedObject);
		}
		
		public abstract void Use(ItemStack selfStack, InputKeyStatus status);
        public abstract bool CanUse(Transform transform);
	}
}
