using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class StoreManager : MonoBehaviour
{
    public Button LeftButton;
    public Button RightButton;
    public Button buttonForHPItem;  // ��ư�� ������ ȸ�� �����۸� ������.
    public Button buttonForMPItem;  // ��ư�� ������ ���� �����۸� ������.
    public Button purchaseButton;  // '�����ϱ�' ��ư
    public Button exitButton;  // '������' ��ư
    int isHPMode; // 1�� ȭ�鿡 hp������ �����ְ�, 0�̸� mp������ ������.
    public Image selectedImage;  // ���õ� �������� �����ִ� �̹���
    public Text selectedItemName;  // ���õ� ������ �̸�
    public Text selectedItemDes;  // ���õ� ������ ����
    public Text selectedItemPrice;  // ���õ� ������ ����
    public Text moneyText;  // ����� �� ��Ÿ���� text
    public Text moneyTextForStore;  // ���������� ���̴� ����� money Text UI

    List<Item> hpItemList;
    List<Item> mpItemList;
    int hpItemPage = 0;  // ȭ��ǥ ��ư Ȱ��ȭ ������ ���� ����
    int mpItemPage = 0;
    int currentPage = 1;  // item ���� ȭ��ǥ ��ư Ŭ���ϸ� �Ѿ�� ������ ����
    int showedItemNum = 8;  // �� ȭ�鿡 ���̴� ������ ����

    string firstItemName = "banana";  // ó�� ������ �� ���õǾ��ִ� ������ ����.
    string selectedItemEngName = "";  // �������� ���õǸ� ���⿡ ���� �̸�(��ųʸ� Ű��)�� �����.

    void Init()
    {
        initList();

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
        RightButton.onClick.AddListener(rightBtnClicked);
        LeftButton.onClick.AddListener(leftBtnClicked);
    }
    void initList()
    {
        hpItemList = new List<Item>();
        mpItemList = new List<Item>();

        foreach (KeyValuePair<string, Item> item in Managers.Data.ItemData)
        {
            if (item.Value.hp == 0)
            {
                mpItemList.Add(item.Value);
            }else if(item.Value.mp == 0)
            {
                hpItemList.Add(item.Value);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isHPMode = 1;
        Init();
        itemSet((currentPage - 1) * showedItemNum + 1);

        selectedImage.color = new Color(1, 1, 1, 1);
        selectedImage.sprite = Managers.Data.ItemSprite[firstItemName];
        selectedItemName.text = Managers.Data.ItemData[firstItemName].koname;
        selectedItemDes.text = "ȸ�� +"+Managers.Data.ItemData[firstItemName].hp;
        selectedItemPrice.text = Managers.Data.ItemData[firstItemName].price.ToString();

        int money = Managers.Data.PlayerData["money"].content;
        if (Managers.Data.ItemData[firstItemName].price > money)
        {
            purchaseButton.interactable = false;
        }
        else
        {
            purchaseButton.interactable = true;
        }
        selectedItemEngName = firstItemName;

        buttonForHPItem.onClick.AddListener(changeItemModeToHP);
        buttonForMPItem.onClick.AddListener(changeItemModeToMP);
        purchaseButton.onClick.AddListener(onClickForPurchase);

        InitItemButton();
        exitButton.onClick.AddListener(exitStore);

        moneyTextForStore.text = moneyText.text;
    }
    void exitStore()
    {
        selectedImage.color = new Color(1, 1, 1, 1);
        selectedImage.sprite = Managers.Data.ItemSprite[firstItemName];
        selectedItemName.text = Managers.Data.ItemData[firstItemName].koname;
        selectedItemDes.text = "ȸ�� +" + Managers.Data.ItemData[firstItemName].hp;
        selectedItemPrice.text = Managers.Data.ItemData[firstItemName].price.ToString();

        int money = Managers.Data.PlayerData["money"].content;
        if (Managers.Data.ItemData[firstItemName].price > money)
        {
            purchaseButton.interactable = false;
        }
        else
        {
            purchaseButton.interactable = true;
        }

        selectedItemEngName = firstItemName;
    }
    void itemSet(int itemCnt)
    {
        int i = 1;
        int num = 0;
        int realITemNum = 0;
        List<Item> list = new List<Item>();
        if(isHPMode == 0)
        {
            list = mpItemList;
        }
        else
        {
            list = hpItemList;
        }
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
                break;
            }
            Image itemImage = GameObject.Find($"Item{i++}").GetComponent<Image>();
            itemImage.color = new Color(1, 1, 1, 1);
            itemImage.sprite = Managers.Data.ItemSprite[item.name];
        }

        for (i = realITemNum+1; i <= showedItemNum; i++)
        {
            Image image = GameObject.Find($"Item{i}").GetComponent<Image>();
            image.color = new Color(1, 1, 1, 0);
        }
    }
    void rightBtnClicked()
    {
        LeftButton.interactable = true;
        currentPage++;
        if(isHPMode == 0)
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

    void InitItemButton()
    {
        for(int i = 1; i <= 8; i++)
        {
            Button button = GameObject.Find($"item{i}").GetComponent<Button>();
            Image image = GameObject.Find($"Item{i}").GetComponent<Image>();
            button.onClick.AddListener(delegate { onClickForItem(image); });
        }
    }
    //�������� Ŭ���� ������ �Ʒ��� ���õ� �������� �̹����� ������ ��Ÿ��.
    void onClickForItem(Image image)
    {
        selectedImage.sprite = image.sprite;
        foreach (KeyValuePair<string, Item> item in Managers.Data.ItemData)
        {
            if (item.Value.name == image.sprite.name)
            {
                selectedItemName.text = item.Value.koname;
                selectedItemPrice.text = item.Value.price.ToString();
                if (isHPMode == 1)
                {
                    selectedItemDes.text =  "ȸ�� +" + item.Value.hp;
                }else if(isHPMode == 0)
                {
                    selectedItemDes.text = "���ݷ� +" + item.Value.mp;
                }

                int money = Managers.Data.PlayerData["money"].content;
                if(item.Value.price > money)
                {
                    purchaseButton.interactable = false;
                }
                else
                {
                    purchaseButton.interactable = true;
                }

                // onClickForPurchase���� ���� ���õ� �������� Ű��(�����̸�)�� �˱� ���ؼ�..
                selectedItemEngName = item.Value.name;

                break;
            }
        }

    }

    void onClickForPurchase()  // �����ϴ� ��ư ������ ȣ��Ǵ� �Լ�
    {
        // �̹� playerData�� �ִ� ���������� Ȯ���ϰ�, ������ �߰�, ������ content�� ����.
        if (Managers.Data.PlayerData.ContainsKey(selectedItemEngName))
        {
            // �̹� ������ ���� �ִ� ���
            Managers.Data.PlayerData[selectedItemEngName].content += 1;   // ������ ������ �ø���
            Managers.Data.PlayerData["money"].content -= Managers.Data.ItemData[selectedItemEngName].price;  // ���� ���ش�.
        }
        else
        {
            // ó�� �����ϴ� ���
            playerData itemForPurchase = new playerData();
            itemForPurchase.name = selectedItemEngName;
            itemForPurchase.content = 1;
            if (Managers.Data.ItemData[selectedItemEngName].hp == 0)  // ȸ�� ���������� ���� ����������..
            {
                itemForPurchase.sort = "mp";
            }
            else
            {
                itemForPurchase.sort = "hp";
            }

            Managers.Data.PlayerData.Add(selectedItemEngName, itemForPurchase);
            Managers.Data.PlayerData["money"].content -= Managers.Data.ItemData[selectedItemEngName].price;
        }
        // ��� ������ ���� �ʿ� ��Ÿ���� �� �� �׼��� �������ش�.
        moneyText.text = Managers.Data.PlayerData["money"].content.ToString();
        moneyTextForStore.text = Managers.Data.PlayerData["money"].content.ToString();
        // ���� �� �̻� ������ ���� ���ٸ� '�����ϱ�'��ư�� ��Ȱ��ȭ ��Ų��. 
        if (Managers.Data.PlayerData["money"].content< Managers.Data.ItemData[selectedItemEngName].price)
        {
            purchaseButton.interactable = false;
        }
        // json ���Ͽ� ��������� �������ش�. 
        playerInfoSave("/Resources/Data/playerData.json");
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

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
