using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public Button LeftButton;
    public Button RightButton;
    public Button buttonForHPItem;
    public Button buttonForMPItem;
    int isHPMode; // 1�� ȭ�鿡 hp������ �����ְ�, 0�̸� mp������ ������.
    public Image selectedImage;  // ���õ� �������� �����ִ� �̹���
    public Text selectedItemName;  // ���õ� ������ �̸�
    public Text selectedItemDes;  // ���õ� ������ ����

    List<Item> hpItemList;
    List<Item> mpItemList;
    int hpItemPage = 0;  // ȭ��ǥ ��ư Ȱ��ȭ ������ ���� ����
    int mpItemPage = 0;
    int currentPage = 1;  // item ���� ȭ��ǥ ��ư Ŭ���ϸ� �Ѿ�� ������ ����
    int showedItemNum = 8;  // �� ȭ�鿡 ���̴� ������ ����

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

        string firstItemName = "apple";
        selectedImage.color = new Color(1, 1, 1, 1);
        selectedImage.sprite = Managers.Data.ItemSprite[firstItemName];
        selectedItemName.text = Managers.Data.ItemData[firstItemName].koname;
        selectedItemDes.text = "ȸ�� +"+Managers.Data.ItemData[firstItemName].hp;

        buttonForHPItem.onClick.AddListener(changeItemModeToHP);
        buttonForMPItem.onClick.AddListener(changeItemModeToMP);
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
