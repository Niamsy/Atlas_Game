using System;
using Game.Inventory;
using Localization;
using Player.Scripts;
using UnityEngine;

namespace Game.Item
{
    [Serializable,	CreateAssetMenu(fileName = "Item", menuName = "Item/Base", order = 1)]
    public class ItemAbstract : ScriptableObject
    {
        [Header("Base item variables")]
        [SerializeField] private int _id = 0;
        public int Id => _id;

        [SerializeField] private GameObject _prefabDroppedGO = null;
        public GameObject PrefabDroppedGO
        {
            get => _prefabDroppedGO;
            set => _prefabDroppedGO = value;
        }

        protected GameObject EquipedObject;
        [SerializeField] private GameObject _prefabHoldedGO = null;
        public GameObject PrefabHoldedGO
        {
            get => _prefabHoldedGO;
            set => _prefabHoldedGO = value;
        }

        public virtual int MaxStackSize => 100;

        [SerializeField] private Sprite _sprite = null;
        public Sprite Sprite => _sprite;

        [SerializeField] private LocalizedText _name = null;
        public string Name => _name;

        [SerializeField] private LocalizedText _description = null;
        public string Description => _description;

        [SerializeField] private LocalizedText _usageText = null;
        public string UsageText => _usageText;

        [SerializeField] private PlayerAnimationData _animation = null;
        public PlayerAnimationData Animation => _animation;

        public virtual GameObject Equip(Transform parent)
        {
            EquipedObject = Instantiate(PrefabHoldedGO, parent);
            return EquipedObject;
        }

        [SerializeField] private ItemCategory _category = null;
        public ItemCategory Category => _category;

        public virtual void UnEquip()
        {
            if (EquipedObject != null)
                Destroy(EquipedObject);
        }

        public virtual void Use(ItemStack selfStack) {}

        public virtual bool CanUse(Transform transform) { return (false); }
        public virtual bool CancelUse(ItemStack selfStack) { return false; }
    }
}
