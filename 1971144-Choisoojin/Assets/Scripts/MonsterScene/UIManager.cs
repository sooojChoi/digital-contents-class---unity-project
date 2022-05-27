using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject InventoryObject;  // �κ��丮 �̹����� ������ �ִ� ���� ������Ʈ
    public Button openInventoryButton;  // �κ��丮 ���� ��ư


    // Start is called before the first frame update
    void Start()
    {
        InventoryObject.SetActive(false);
        InventoryObject.SetActive(false);

        openInventoryButton.onClick.AddListener(openInventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�κ��丮 ���� �Լ�
    void openInventory()
    {
        InventoryObject.SetActive(true);
    }
}
