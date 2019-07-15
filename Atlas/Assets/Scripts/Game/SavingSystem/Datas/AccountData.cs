using System;
using UnityEngine;

namespace Game.SavingSystem.Datas
{
	[Serializable]
    public class AccountData
    {
	    /// <summary>
	    /// Maximum number of profils per account
	    /// </summary>
	    public static readonly int MaxNumberOfProfils = 5;

	    /// <summary>
	    /// All the profiles of this account
	    /// </summary>
	    public ProfilData[]					Profils = new ProfilData[MaxNumberOfProfils];
	    public DateTime						LastGetScannedPlant;
	    public int 							ID = 0;

	    public AccountData(int id)
	    {
		    ID = id;
	    }
	    
    }

	[Serializable]
	public class ProfilData
	{
		public int 							ID = 0;

		public string						Name = "Empty name";
		
		/// <summary>
		/// True if the Profil contains some save
		/// </summary>
		public bool							Used = false;
		
		public ProfilData(int id, string name)
		{
			ID = id;
			Name = name;
		}
	}
}
