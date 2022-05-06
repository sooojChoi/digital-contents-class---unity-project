using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    public Button LeftButton;
    public Button RightButton;
    public Button buttonForHPItem;  // ��ư�� ������ ȸ�� �����۸� ������.
    public Button buttonForMPItem;  // ��ư�� ������ ���� �����۸� ������.
    public Button exitButton;  // '������' ��ư
    public Button openInventoryButton;  // �κ��丮 ���� ��ư
    int isHPMode; // 1�� ȭ�鿡 hp������ �����ְ�, 0�̸� mp������ ������.

    public Image hpItemImage;  // hp ���������� ������.
    public Image mpItemImage;  // mp ���������� ������.
    public Text hpTextName;  // hp ���������� ������ (������ �̸�)
    public Text mpTextName;  // mp ���������� ������ (������ �̸�)
    public Text hpTextPower;  // hp ���������� ������ (������ ����)
    public Text mpTextPower;  // mp ���������� ������ (������ ����)

    List<Item> hpItemList;
    List<Item> mpItemList;
    int hpItemPage = 0;  // ȭ��ǥ ��ư Ȱ��ȭ ������ ���� ����
    int mpItemPage = 0;
    int currentPage = 1;  // item ���� ȭ��ǥ ��ư Ŭ���ϸ� �Ѿ�� ������ ����
    int showedItemNum = 4;  // �� ȭ�鿡 ���̴� ������ ����

    void Init()
    {
        initList();

        Debug.Log("ȸ�� ������ ����:" + hpItemList.Count);
        hpItemPage = hpItemList.Count / showedItemNum;
        if (hpItemList.Count % showedItemNum != 0)
        {
            hpItemPage++;
        }
        mpItemPage = mpItemList.Count / showedItemNum;
        if (mpItemList.Count % showedItemNum != 0)
        {
            mpItemPage++;
        }

        LeftButton.interactable = false;
        if (hpItemList.Count > showedItemNum)
        {
            RightButton.interactable = true;
        }
        else
        {
            RightButton.interactable = false;
        }
        RightButton.onClick.RemoveAllListeners();
        LeftButton.onClick.RemoveAllListeners();

        RightButton.onClick.AddListener(rightBtnClicked);
        LeftButton.onClick.AddListener(leftBtnClicked);
    }
    void initList()
    {
        hpItemList = new List<Item>();
        mpItemList = new List<Item>();

        foreach (KeyValuePair<string, playerData> item in Managers.Data.PlayerData)
        {
            if (item.Value.sort == "mp")
            {
                mpItemList.Add(Managers.Data.ItemData[item.Value.name]);
            }
            else if (item.Value.sort == "hp")
            {
                hpItemList.Add(Managers.Data.ItemData[item.Value.name]);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isHPMode = 1;
        Init();
        itemSet((currentPage - 1) * showedItemNum + 1);

        buttonForHPItem.onClick.AddListener(changeItemModeToHP);
        buttonForMPItem.onClick.AddListener(changeItemModeToMP);

        InitItemButton();
        exitButton.onClick.AddListener(exitStore);
        openInventoryButton.onClick.RemoveAllListeners();
        openInventoryButton.onClick.AddListener(openInventory);

        // ������ ȸ��, ���� �������� ������ �κ��丮�� ��Ÿ����.
        if(Managers.Data.PlayerData["hpItem"].sort != "")
        {
            hpItemImage.color = new Color(1, 1, 1, 1);
            string hpItemName = Managers.Data.PlayerData["hpItem"].sort;
            hpItemImage.sprite = Managers.Data.ItemSprite[hpItemName];
            hpTextName.text = Managers.Data.ItemData[hpItemName].koname;
            hpTextPower.text = "ȸ�� +"+Managers.Data.ItemData[hpItemName].hp.ToString();
        }
        if (Managers.Data.PlayerData["mpItem"].sort != "")
        {
            mpItemImage.color = new Color(1, 1, 1, 1);
            string mpItemName = Managers.Data.PlayerData["mpItem"].sort;
            mpItemImage.sprite = Managers.Data.ItemSprite[mpItemName];
            mpTextName.text = Managers.Data.ItemData[mpItemName].koname;
            mpTextPower.text = "ȸ�� +" + Managers.Data.ItemData[mpItemName].mp.ToString();
        }
    }
    void exitStore()
    {
        changeItemModeToHP();
        gameObject.SetActive(false);
    }
    void InitItemButton()
    {
        for (int i = 1; i <= 4; i++)
        {
            Button button = GameObject.Find($"invItem{i}").GetComponent<Button>();
            Image image = GameObject.Find($"iv_item{i}").GetComponent<Image>();
            button.onClick.AddListener(delegate { onClickForItem(image); });
        }
    }

    //�������� Ŭ���� ������ �Ʒ��� ���õ� �������� �̹����� ������ ��Ÿ��.
    void onClickForItem(Image image)
    {
        if(image.sprite == null)
        {
            return;
        }
        if(isHPMode == 1)  //hp �������� Ŭ������ ���
        {
            hpItemImage.color = new Color(1, 1, 1, 1);
            hpItemImage.sprite = image.sprite;
            foreach (KeyValuePair<string, Item> item in Managers.Data.ItemData)
            {
                if (item.Value.name == image.sprite.name)
                {
                    // ȭ�鿡 ���õ� ������ ������ ��Ÿ����.
                    hpTextName.text = item.Value.koname;  
                    hpTextPower.text = "ȸ�� +" + item.Value.hp;

                    // ������� hpItem ������ ���õ� ���������� �����Ѵ�.
                    Managers.Data.PlayerData["hpItem"].sort = item.Value.name;

                    break;
                }
            }
        }
        else  // mp �������� �������� ���
        {
            mpItemImage.color = new Color(1, 1, 1, 1);
            mpItemImage.sprite = image.sprite;
            foreach (KeyValuePair<string, Item> item in Managers.Data.ItemData)
            {
                if (item.Value.name == image.sprite.name)
                {
                    // ȭ�鿡 ���õ� ������ ������ ��Ÿ����.
                    mpTextName.text = item.Value.koname;
                    mpTextPower.text = "���ݷ� +" + item.Value.mp;

                    // ������� hpItem ������ ���õ� ���������� �����Ѵ�.
                    Managers.Data.PlayerData["mpItem"].sort = item.Value.name;

                    break;
                }
            }
        }
        // json ���Ͽ� ��������� �������ش�. 
        playerInfoSave("/Resources/Data/playerData.json");

    }
    void itemSet(int itemCnt)
    {
        int i = 1;
        int num = 0;
        int realITemNum = 0;
        List<Item> list = new List<Item>();
        if (isHPMode == 0)
        {
            list = mpItemList;
        }
        else
        {
            list = hpItemList;
        }
        Debug.Log("ȸ�� ������ ����(itemSet): " + hpItemList.Count);
       
        foreach (Item item in list)
        {
            if (isHPMode == 1)
            {
                if (item.hp == 0)
                {
                    break;
                }
            }
            else if (isHPMode == 0)
            {
                if (item.mp == 0)
                {
                    break;
                }
            }

            Debug.Log("itemCnt: " + itemCnt);
            Debug.Log("currentPage : " + currentPage+ ", showedItemNum: "+ showedItemNum);
            num++;
            if (num < itemCnt)
            {
                continue;
            }
            else
            {
                realITemNum++;
            }
            if (i > showedItemNum)
            {
                Debug.Log("for�� ����. i: "+i);
                break;
            }
            Image itemImage = GameObject.Find($"iv_item{i}").GetComponent<Image>();
            itemImage.color = new Color(1, 1, 1, 1);
            itemImage.sprite = Managers.Data.ItemSprite[item.name];
            Text itemNameText = GameObject.Find($"iv_text{i}").GetComponent<Text>();
            itemNameText.text = Managers.Data.PlayerData[item.name].content.ToString();
            Text itemInfo = GameObject.Find($"iv_info{i++}").GetComponent<Text>();
            if(Managers.Data.PlayerData[item.name].sort == "hp")
            {
                itemInfo.text = item.koname + "\nHP: " + Managers.Data.ItemData[item.name].hp.ToString();
            }else if(Managers.Data.PlayerData[item.name].sort == "mp")
            {
                itemInfo.text = item.koname+"\nMP: "+Managers.Data.ItemData[item.name].mp.ToString();
            }
            
        }

        for (i = realITemNum + 1; i <= showedItemNum; i++)
        {
            Debug.Log("������ ����: " + realITemNum);
            Image image = GameObject.Find($"iv_item{i}").GetComponent<Image>();
            image.color = new Color(1, 1, 1, 0);
            Text itemNameText = GameObject.Find($"iv_text{i}").GetComponent<Text>();
            itemNameText.text = "";
            Text itemInfo = GameObject.Find($"iv_info{i}").GetComponent<Text>();
            itemInfo.text = "";
        }
    }
    void rightBtnClicked()
    {
        LeftButton.interactable = true;
        currentPage++;
        if (isHPMode == 0)
        {
            if (currentPage == mpItemPage)
            {
                RightButton.interactable = false;
            }
            itemSet((currentPage - 1) * showedItemNum + 1);
        }
        else
        {
            if (currentPage == hpItemPage)
            {
                RightButton.interactable = false;
            }
            itemSet((currentPage - 1) * showedItemNum + 1);
        }

    }
    void leftBtnClicked()
    {
        RightButton.interactable = true;
        currentPage--;
        if (currentPage == 1)
        {
            LeftButton.interactable = false;
        }
        itemSet((currentPage - 1) * showedItemNum + 1);
    }
    void changeItemModeToHP()
    {
        isHPMode = 1;
        currentPage = 1;
        itemSet((currentPage - 1) * showedItemNum + 1);
        LeftButton.interactable = false;
        if (hpItemList.Count > showedItemNum)
        {
            RightButton.interactable = true;
        }
        else
        {
            RightButton.interactable = false;
        }
    }
    void changeItemModeToMP()
    {
        isHPMode = 0;
        currentPage = 1;
        itemSet((currentPage - 1) * showedItemNum + 1);
        LeftButton.interactable = false;
        if (mpItemList.Count > showedItemNum)
        {
            RightButton.interactable = true;
        }
        else
        {
            RightButton.interactable = false;
        }
    }

    //  playerData.json ���� �����ϴ� �Լ�
    void playerInfoSave(string path)
    {
        List<playerData> playerInfo = new List<playerData>();
        playerDataInfo playerData = new playerDataInfo();

        foreach (KeyValuePair<string, playerData> player in Managers.Data.PlayerData)
        {
            playerInfo.Add(player.Value);
        }
        playerData.playerInfo = playerInfo;

        string jsonString = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + path, jsonString);
    }

    //�κ��丮 ���� �Լ�
    void openInventory()
    {
        Debug.Log("�κ��丮 ���µ�");
        gameObject.SetActive(true);
        currentPage = 1;
        isHPMode = 1;
        Init();
        itemSet((currentPage - 1) * showedItemNum + 1);
        // gameObject.SetActive(true);
    }
}
