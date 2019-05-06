using System;
using Game.Inventory;
using InputManagement;
using Localization;
using Player.Scripts;
using UnityEngine;

namespace Game.Item
{
	[Serializable]
	public abstract class ItemAbstract : ScriptableObject
	{
		[Header("Base item variables")]
		[SerializeField] private int _id;
		public int Id => _id;

		[SerializeField] private GameObject _prefabDroppedGO;
		public GameObject PrefabDroppedGO => _prefabDroppedGO;

		protected GameObject EquipedObject;
		[SerializeField] private GameObject _prefabHoldedGO;
		public GameObject PrefabHoldedGO => _prefabHoldedGO;

		public virtual int MaxStackSize => 100;

		[SerializeField] private Sprite _sprite;
		public Sprite Sprite => _sprite;

		[SerializeField] private LocalizedText _name;
		public string Name => _name;

		[SerializeField] private LocalizedText _description;
		public string Description => _description;

		[SerializeField] private LocalizedText _usageText;
		public string UsageText => _usageText;

		[SerializeField] private PlayerAnimationData _animation;
        public PlayerAnimationData Animation => _animation;

		public virtual GameObject Equip(Transform parent)
		{
			EquipedObject = Instantiate(PrefabHoldedGO, parent);
			return EquipedObject;
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
