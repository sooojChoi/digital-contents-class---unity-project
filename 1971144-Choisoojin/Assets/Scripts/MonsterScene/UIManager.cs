using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject InventoryObject;  // �κ��丮 �̹����� ������ �ִ� ���� ������Ʈ
    public Button openInventoryButton;  // �κ��丮 ���� ��ư
    public Image hpItemImage;  // ���� ������ ȸ�� ������ �̹���
    public Image mpItemImage;  // ���� ������ ���� ������ �̹���
    public Text hpItemContent; // ���� ������ ȸ�� ������ ������ ��Ÿ���� �ؽ�Ʈ
    public Text mpItemContent;

    // Start is called before the first frame update
    void Start()
    {
        InventoryObject.SetActive(false);
        InventoryObject.SetActive(false);

        openInventoryButton.onClick.AddListener(openInventory);

        // ������ �̹��� ����
        string mpItemName = Managers.Data.PlayerData["mpItem"].sort;
        string hpItemName = Managers.Data.PlayerData["hpItem"].sort;
        setItemInfo(mpItemName, "mp");
        setItemInfo(hpItemName, "hp");
    }

    // Update is called once per frame
    void Update()
    {
        string mpItemName = Managers.Data.PlayerData["mpItem"].sort;
        string hpItemName = Managers.Data.PlayerData["hpItem"].sort;

        setItemInfo(mpItemName, "mp");
        setItemInfo(hpItemName, "hp");

    }

    //�κ��丮 ���� �Լ�
    void openInventory()
    {
        InventoryObject.SetActive(true);
    }

    // ȸ�� ������ �̹��� �����ϴ� �Լ�
    void setItemInfo(string imageName, string itemSort)
    {
        if(imageName == null || imageName == "")
        {
            if(itemSort == "hp")
            {
                // ������ �������� ������ �̹����� �����ϰ� �ϰ� return�Ѵ�.
                hpItemImage.color = new Color(1, 1, 1, 0);
                hpItemContent.text = "";
            }
            else if(itemSort == "mp")
            {
                // ������ �������� ������ �̹����� �����ϰ� �ϰ� return�Ѵ�.
                mpItemImage.color = new Color(1, 1, 1, 0);
                mpItemContent.text = "";
            }
      
            return;
        }

        Sprite image = Managers.Data.ItemSprite[imageName];
        if (itemSort == "hp")
        {
            hpItemImage.sprite = image;
            hpItemImage.color = new Color(1, 1, 1, 1);

            hpItemContent.text = Managers.Data.PlayerData[imageName].content.ToString();
        }
        else if (itemSort == "mp")
        {
            mpItemImage.sprite = image;
            mpItemImage.color = new Color(1, 1, 1, 1);

            mpItemContent.text = Managers.Data.PlayerData[imageName].content.ToString();
        }
  
    }
    
}
