using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}
public class DataManager : MonoBehaviour
{
    public Dictionary<string, GameSaveData> GameSaveData { get; private set; } = new Dictionary<string, GameSaveData>();
    public Dictionary<string, Sprite> ItemSprite { get; private set; } = new Dictionary<string, Sprite>();  //������ �̹����� ���� ��ųʸ�.
    public Dictionary<string, Item> ItemData { get; private set; } = new Dictionary<string, Item>();


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Init()
    {
        GameSaveData = LoadJson<GameSaveInfo, string, GameSaveData>("gameSaveData").MakeDict();
        ItemData = LoadJson<ItemInfo, string, Item>("itemData").MakeDict();
        foreach (KeyValuePair<string, Item> recp in ItemData)
        {
            //��ü �������� ��� ItemData�� Ȱ���Ͽ� {������ �̸�-Key, �̹���-Value} Dictionary�� �����.
            Sprite sprite = Resources.Load<Sprite>($"Image/Item/{recp.Key}");
            //�̹����� ������ �� '�̹��� ���� �̸�'�� ItemData.json�� �����ϴ� "name"�� �̸��� �����ؾ� �Ѵ�.(��ҹ��ڱ���)
            //ex) Mint.jpg���Ϸ� ���������� ItemData���� "name":"Mint"��, PlayerData������ "element":"Mint"�� �ؾ���
            if (sprite != null)
            {
                ItemSprite.Add(recp.Key, sprite);  //�������̸��� �̹����� ��´�.
            }
        }
    }


    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

}
