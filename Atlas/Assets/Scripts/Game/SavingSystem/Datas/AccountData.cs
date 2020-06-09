using System;
using System.Collections.Generic;
using Menu.LevelSelector;

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
        public XPSaveData                   XPData = new XPSaveData(0f, 200f, 0f, 1, false);
        
        public AccountData(int id)
	    {
		    ID = id;
	    }
	    
    }

    [Serializable]
    public class CharacterGlobalInfoData
    {
        public int    PlayerChallengeOwned = 0;
        public int    PlayerTimePlayed = 0;
        public LevelInfoData[] LevelInfoDatas;

        public void SaveLevels(List<LiveLevelInfo> levelInfos)
        {
	        LevelInfoDatas = new LevelInfoData[levelInfos.Count];
	        for (int x = 0; x < levelInfos.Count; x++)
	        {
		        LiveLevelInfo info = levelInfos[x];
		        LevelInfoDatas[x] = new LevelInfoData(info);
	        }
        }
    }

    [Serializable]
	public class ProfilData
	{
		public int 							ID = 0;

		public string						Name = "Empty name";

		public CharacterGlobalInfoData      CharacterGlobalInfo = new CharacterGlobalInfoData();

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

	[Serializable]
	public struct LevelInfoData
	{
		public string LevelTitle;
		public bool Challenge1Complete;
		public bool Challenge2Complete;
		public bool Challenge3Complete;

		public LevelInfoData(LevelInfo levelInfo)
		{
			LevelTitle = levelInfo.LevelTitle;
			Challenge1Complete = false;
			Challenge2Complete = false;
			Challenge3Complete = false;
		}
		
		public LevelInfoData(LiveLevelInfo levelInfo)
		{
			LevelTitle = levelInfo.LevelInfo.LevelTitle;
			Challenge1Complete = levelInfo.ChallengeOneComplete;
			Challenge2Complete = levelInfo.ChallengeTwoComplete;
			Challenge3Complete = levelInfo.ChallengeThreeComplete;
		}
	}
}
