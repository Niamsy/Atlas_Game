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
        public CharacterGlobalInfoData      CharacterGlobalInfo = new CharacterGlobalInfoData();
        public XPSaveData                   XPData = new XPSaveData(0f, 200f, 0f, 1, false);

        
        public AccountData(int id)
	    {
		    ID = id;
	    }
	    
    }

    [Serializable]
    public class CharacterGlobalInfoData
    {
        public int      PlayerChallengeOwned = 0;
        public int    PlayerTimePlayed = 0;
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
	
	[Serializable]
	public struct XPSaveData
	{
		public float PlayerXP;
		public float RoofLevel;
		public float FloorLevel;
		public float PlayerLevel;
		public bool NotFirstTime;

		public XPSaveData(float playerXp, float roofLevel, float floorLevel, float playerLevel, bool notFirstTime)
		{
			PlayerXP = playerXp;
			RoofLevel = roofLevel;
			FloorLevel = floorLevel;
			PlayerLevel = playerLevel;
			NotFirstTime = notFirstTime;
		}
	}
}
