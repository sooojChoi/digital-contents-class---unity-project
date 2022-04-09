using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public Button nextButton;
    public Text sayingText;
    public GameObject firstCanvasObject;
    string[] firstChracterSaying;
    int textNum = 0;
    public static int sellerScriptEnd = 0;

    public GameObject noticeBoxImage;
    public GameObject storeObject;
    public Button exitButton;
    public Button toTalkWithSeller;

    public GameObject sellerBox;

    // Start is called before the first frame update
    void Start()
    {
        noticeBoxImage.SetActive(false);
        firstChracterSaying = InitTextArray();
        sayingText.text = firstChracterSaying[textNum];
        nextButton.onClick.AddListener(ShowNextText);

        storeObject.SetActive(false);
        exitButton.onClick.AddListener(exitStore);
        toTalkWithSeller.onClick.AddListener(showStore);
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraMoving.sellerForCanvasScript == 1)
        {
            CameraMoving.sellerForCanvasScript = 0;
            noticeBoxImage.SetActive(true);
        }
        
    }
    //void End()
    //{
    //    CameraMoving.meetingSellerNow = 0;
    //}

    void showStore()
    {
        noticeBoxImage.SetActive(false);
        storeObject.SetActive(true);
    }
    void exitStore()
    {
        CameraMoving.meetingSellerNow = 0;
        storeObject.SetActive(false);
        sellerBox.SetActive(false);
    }
    string[] InitTextArray()
    {
        string[] tempArray = new string[5];
        tempArray[0] = "�츮 ������ ���� ���� ȯ���մϴ�.";
        tempArray[1] = "���谡��, �ҹ��� ��� ���̱���.";
        tempArray[2] = "���� ���͸� ������ ��û�� ������ ���� �� �ִٴ� �ҹ� ���̿���!";
        tempArray[3] = "�� ���� ������ �츮 ���� ����� ��� �̿� ���� �־��.";
        tempArray[4] = "���谡��, ���� ���͸� ��� ���� ã���ּ���. ��ٸ��� �����Կ�.";

        return tempArray;
    }
    void ShowNextText()
    {
        textNum++;
        if(textNum< firstChracterSaying.Length)
        {
            sayingText.text = firstChracterSaying[textNum];
        }
        else
        {
            firstCanvasObject.SetActive(false);
        }
    }
}
