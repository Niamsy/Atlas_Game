using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataHolder<ItemDataType>
{
    public delegate ItemDataType itemBuilderPrototype(INIParser parser, string SectionName);

    public static string DirectoryPath = Application.dataPath + "/ConfigFiles/";
    private INIParser fileParser = new INIParser();
    private Dictionary<string, ItemDataType> __itemsData = new Dictionary<string, ItemDataType>();

    public ItemDataHolder(string fileName) {
        fileParser.Open(DirectoryPath + fileName);
    }

    public void addData(string itemName, itemBuilderPrototype itemBuilder)
    {
        __itemsData.Add(itemName, itemBuilder(fileParser, itemName));
    }

    ItemDataType getData(string ItemName) {
        try
        {
            return __itemsData[ItemName];
        } catch (KeyNotFoundException e)
        {
            Debug.Log("Can't Get Data from item :" + ItemName + " reason : " + e.ToString());
        }
        return default(ItemDataType);
    }
}

