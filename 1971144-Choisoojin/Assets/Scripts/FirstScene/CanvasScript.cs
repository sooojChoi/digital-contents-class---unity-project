using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public Button nextButton;
    public Text sayingText;
    public GameObject firstCanvasObject;
    public Image firstCharacterImage;  // ��ũ��Ʈ�� ĳ���� �̹���
    string[] firstChracterSaying;
    int textNum = 0;
    public static int sellerScriptEnd = 0;

    public GameObject noticeBoxImage;
    public GameObject storeObject;
    public Button exitButton;
    public Button toTalkWithSeller;

    public GameObject sellerBox;
    public int firstMeetingSeller = 1;  //1 �̸� ������ ó�� ������ ��, 0�̸� �̹� �� �� ���� ��
    // ������ ó�� ���� ���� ��ȭ ��ũ��Ʈ�� ������ �ϱ� ���� ��.

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

    public void showStore()
    {
        if(firstMeetingSeller == 1)
        {
            firstMeetingSeller = 0;
            noticeBoxImage.SetActive(false);
            sayingText.text = "�������� ��� �ʹٰ��? ���ƿ�! �츮 ������ ���� ���� �������� ���ƿ�. ";
            Sprite[] sprites = Resources.LoadAll<Sprite>("Character/PackForest01");
            if(sprites == null)
            {
                Debug.Log("sprite is null");
            }
            else
            {
                Debug.Log("sprite is not null");
            }
            firstCharacterImage.sprite = sprites[3];
            nextButton.onClick.AddListener(showStoreForNextButton);
            firstCanvasObject.SetActive(true);
        }
        else
        {
            storeObject.SetActive(true);
        }
       
    }
    void showStoreForNextButton()
    {
        firstCanvasObject.SetActive(false);
        storeObject.SetActive(true);
    }
    void exitStore()
    {
        CameraMoving.meetingSellerNow = 0;
        sellerScriptEnd = 1;
        storeObject.SetActive(false);
       
    }
    string[] InitTextArray()
    {
        string[] tempArray = new string[5];
        tempArray[0] = "���谡��, �츮 ������ ���� ���� ȯ���մϴ�.";
        tempArray[1] = "�ҹ��� ��� ���̱���. ���� ���͸� ������ ��û�� ������ ���� �� �ִٴ� �ҹ� ���̿���!";
        tempArray[2] = "���͸� �������� ưư�� ü�°� ������ ���Ⱑ �ʿ��ϴ�ϴ�.";
        tempArray[3] = "����������, ü�� �����۰� ����� �������� �Ȱ� �����ϱ��.  ";
        tempArray[4] = "���� ������ ã�Ƽ� ���� �ɾ����.";

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
