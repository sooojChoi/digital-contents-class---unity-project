using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameSaveData
{
    public string mapNumber;  // "mapNumber"
    public string realMapNumber;  // ���� �� �̸� ��.
}

[Serializable]
public class GameSaveInfo : ILoader<string, GameSaveData>
{
    public List<GameSaveData> gameSaveInfo = new List<GameSaveData>();

    public Dictionary<string, GameSaveData> MakeDict()
    {
        Dictionary<string, GameSaveData> dict = new Dictionary<string, GameSaveData>();

        foreach (GameSaveData save in gameSaveInfo)
        {
            dict.Add(save.mapNumber, save);
        }

        return dict;
    }
}


[Serializable]
public class Item
{
    public string name;
    public string koname;
    public int price;
    public int hp; 
    public int mp;
}

[Serializable]
public class ItemInfo : ILoader<string, Item>
{
    public List<Item> itemInfo = new List<Item>();

    public Dictionary<string, Item> MakeDict()
    {
        Dictionary<string, Item> dict = new Dictionary<string, Item>();

        foreach (Item item in itemInfo)
        {
            dict.Add(item.name, item);
        }

        return dict;
    }
}

[Serializable]
public class playerData  // player�� ������ ���� json. ������ �������̳� �� ���� ��.
{
    public string name;  // �������� ��� �������� �̸�,
                         // �� ������ "money", ������ ȸ�� �������� "hpItem", ������ ���� �������� "mpItem", �÷��̾� ü���� "hp"
                         // "hpItem"�� "mpItem"�� ������ �̸��� sort�� ����ȴ�. "content"�� �׻� 1.
                         // money�� hpItem, mpItem, hp�� �������� �ȵǴ� ����.

    public int content;  // �������� ��� ������ ����, ���� ��� �󸶸� �����ϰ� �ִ���.
    public string sort;  // �������� ��� hp���� mp����. ���� ��� null.
}

[Serializable]
public class playerDataInfo : ILoader<string, playerData>
{
    public List<playerData> playerInfo = new List<playerData>();

    public Dictionary<string, playerData> MakeDict()
    {
        Dictionary<string, playerData> dict = new Dictionary<string, playerData>();

        foreach (playerData player in playerInfo)
        {
            dict.Add(player.name, player);
        }

        return dict;
    }
}