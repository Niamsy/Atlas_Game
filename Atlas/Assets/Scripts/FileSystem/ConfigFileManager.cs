﻿using System.Collections.Generic;
using UnityEngine;

public class ConfigFileManager
{
    public static string configFilePath = "/atlasConfig.ini";
    public static string fullConfigFilePath = Application.dataPath + configFilePath;
    private string notFoundReference = "Key does not Exist";
    private INIParser configFile = new INIParser();
    
    public ConfigFileManager()
    {
        configFile.Open(fullConfigFilePath);
    }

    ~ConfigFileManager()
    {
        configFile.Close();
    }

    public string getConfigValue(string key, string section = "Default")
    {
        string ret = configFile.ReadValue(section, key, notFoundReference);
        if (ret == notFoundReference)
        {
            throw new KeyNotFoundException("Impossible to find Key : " + key + " In section : " + section);
        }
        return ret;
    }

    public void setConfigValue(string key, string value, string section = "Default")
    {
        configFile.WriteValue(section, key, value);
    }

    public void saveConfig()
    {
        configFile.Close();
        configFile.Open(fullConfigFilePath);
    }
}
