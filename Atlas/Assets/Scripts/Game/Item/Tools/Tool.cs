﻿using System;

namespace Game.Item
{
	[Serializable]
	public abstract class Tool : ItemAbstract
	{
		public override int MaxStackSize
		{
			get { return (1); }
		}
	}
}
