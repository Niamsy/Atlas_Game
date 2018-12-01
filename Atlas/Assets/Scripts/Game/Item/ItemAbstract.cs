using System;
using Localization;
using UnityEngine;

namespace Game.Item
{
	[Serializable]
	public abstract class ItemAbstract : ScriptableObject
	{
		[SerializeField] private int _id;
		public int Id
		{
			get { return (_id); }
		}

		[SerializeField] private GameObject _prefabMesh;
		public GameObject PrefabMesh
		{
			get { return (_prefabMesh); }
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
		
		public abstract void Use();
	}
}
