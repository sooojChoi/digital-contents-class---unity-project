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
    public Text moneyText;  // ����� �� ��Ÿ���� text

    public GameObject noticeBoxImage;
    public GameObject storeObject;
    public Button exitButton;
  //  public Button toTalkWithSeller;

    public GameObject sellerBox;
   // public int firstMeetingSeller = 1;  //1 �̸� ������ ó�� ������ ��, 0�̸� �̹� �� �� ���� ��
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
        //  toTalkWithSeller.onClick.AddListener(showStore);

        foreach (KeyValuePair<string, playerData> player in Managers.Data.PlayerData)
        {
            Debug.Log("player money text is update.");
            if (player.Value.name == "money")
            {
                moneyText.text = player.Value.content.ToString();
                break;
            }
        }
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

    void exitStore()
    {
        CameraMoving.meetingSellerNow = 0;
        sellerScriptEnd = 1;
        storeObject.SetActive(false);
       
    }
    string[] InitTextArray()
    {
        string[] tempArray = new string[7];
        tempArray[0] = "������ ��������! ģ��, ������ ���������� ���� Ȥ�� �Ƴ�?";
        tempArray[1] = "��? �̰� �׳� ����� ���ڰ� �Ƴ�! ����ϰ� �����ѵ� ¬©�ϱ���� �ϴٰ�!";
        tempArray[2] = "ũ��.. �ʹ� ����߱�. ������ ���� �Ծ �� �� �ְ��°�.";
        tempArray[3] = "������ ���������� ������ �������� ��ﵵ �ȳ�. ���͵��� ��� ������ �����ϱ� ���̾�! ����";
        tempArray[4] = "��? �ʰ� ���͸� ��� ����� ������ �ְڴٰ�?";
        tempArray[5] = "�׷� �������� ������ ���� �鷯. ���ʹ� ������ �༮���� �ƴϾ�! ȸ�� �������� �� �ʿ��ϴٰ�!";
        tempArray[6] = "�׷� ��Ź�� ģ��! ������ ���������� ���� �� �ִ� �� ������!";

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
