using System;
using UnityEditor;
using UnityEngine;

namespace Game.Item.Tools
{
	/// <summary>
	/// Base Class for all the tools items
	/// </summary>
	/// <typeparam name="TBehaviour">The MonoBehaviour on the equipped gameObject that will coordinate the action of the item</typeparam>
	[Serializable]
	public abstract class Tool<TBehaviour> : ItemAbstract where TBehaviour : MonoBehaviour
	{
		public override int MaxStackSize
		{
			get { return (1); }
		}

		protected TBehaviour Behaviour;
		
		public override GameObject Equip(Transform parent)
		{
			base.Equip(parent);
			Behaviour = EquipedObject.GetComponent<TBehaviour>();
			#if UNITY_EDITOR
			if (Behaviour == null)
				Debug.LogAssertion("The tool " + name + " didn't had any /" + typeof(TBehaviour) + "/ linked to his holded object (Equipped item). This will led to crash and unexpected behaviour");
			#endif
			return (EquipedObject);
		}

		public override void UnEquip()
		{
			Behaviour = null;
			base.UnEquip();
		}	
	}
}
