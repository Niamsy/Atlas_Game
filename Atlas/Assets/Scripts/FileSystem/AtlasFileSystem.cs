using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtlasFileSystem
{

    #region private Variable
    private ConfigFileManager __configFileManager = new ConfigFileManager();
    #endregion

    #region Item dataHolders
    private ItemDataHolder<int> __plants;
    private ItemDataHolder<int> __tools;
    private ItemDataHolder<int> __items;
    #endregion

    #region public Variables
    #endregion

    #region Singleton
    private static AtlasFileSystem _instance = null;
    public static AtlasFileSystem Instance
    {
        get
        {
            if (_instance == null)
                _instance = new AtlasFileSystem();
            return (_instance);
        }
    }

    private AtlasFileSystem(){}
    #endregion

    #region ConfigFile
    public string getConfigValue(string key, string section = "Default")
    {
        return __configFileManager.getConfigValue(key, section);
    }
    
    public void setConfigFileValue(string Section, string Key, string Value)
    {
        __configFileManager.setConfigValue(Key, Value, Section);
    }
    

    public void saveConfig()
    {
        __configFileManager.saveConfig();
    }
    #endregion

    #region ItemInformations

    public ItemObjType getItemData<ItemObjType>(ItemType type, string itemName) {
        switch (type)
        {
            case ItemType.Equipment:
            case ItemType.Item:
            case ItemType.Plant:
            case ItemType.Structure:
            case ItemType.Tool:
                return default(ItemObjType);
            default:
                return default(ItemObjType);
        }
    }
    
    #endregion
}
