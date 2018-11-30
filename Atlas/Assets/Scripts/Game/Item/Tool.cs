using UnityEngine;

namespace Game.Item
{
	public abstract class Tool : ItemAbstract
	{
		[SerializeField] private GameObject _prefabHolded;
		public GameObject PrefabHolded
		{
			get { return (_prefabHolded); }
		}
		
		public override int MaxStackSize
		{
			get { return (1); }
		}
	}
}
