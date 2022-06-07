using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onClickForNeoguri : MonoBehaviour
{
    public GameObject neoguriScript;
    public Text sayingText;
    string[] firstChracterSaying;
    int textNum = 0;
    public Button nextButton;
    public Image firstCharacterImage;  // ��ũ��Ʈ�� ĳ���� �̹���
    public Image secondCharacterImage;  // midScene������ ���̴� �� ��° �ʱ��� �̹���
    public GameObject portalObject;  // ��Ż ������Ʈ(�ʱ����� ��ȭ�� ������ ���δ�)

    public bool midScene = false;

    // Start is called before the first frame update
    void Start()
    {
        // ó������ ��Ż�� ������ �ʰ� �Ѵ�. (�ʱ����� ��ȭ�� ������ ��Ż�� ����)
        if (midScene == true)
        {
            neoguriScript.SetActive(false);
        }
        portalObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mouseX = position.x;
        float mouseY = position.y;
        float offsetX = 2;
        float offsetY = 1.5f;
        if (Input.GetMouseButtonDown(0))
        {
            if (mouseX < x + offsetX && mouseX > x - offsetX)
            {
                if (mouseY < y + offsetY && mouseY > y - offsetY)
                {
                    showScript();
                }
            }
        }
    }

    void showScript()
    {

        if (midScene == false)
        {
            firstChracterSaying = InitTextArray();
        }
        else
        {
            firstChracterSaying = InitTextArrayForMidScene();
        }
           
        sayingText.text = firstChracterSaying[textNum];
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(ShowNextText);

        Sprite[] sprites = Resources.LoadAll<Sprite>("Character/Neoguri_Character");
        firstCharacterImage.sprite = sprites[0];

        neoguriScript.SetActive(true);
    }

    string[] InitTextArray()
    {
        string[] tempArray = new string[4];
        tempArray[0] = "ģ��! �������� �������� �� ��°�?";
        tempArray[1] = "���Ҿ�! �׷� ���� ���� ������ ���� ������ ���������� ã����! ����� ������ �� ���������� �� 10����.";
        tempArray[2] = "���� ��Ż�� ��������״� �ٳ����!";
        tempArray[3] = "��Ż���� Ű���� 'S'�� ������ �̵��� �� �־�.";
     
        return tempArray;
    }
    string[] InitTextArrayForMidScene()
    {
        string[] tempArray = new string[8];
        tempArray[0] = "ģ��! ���� �������� 10���� ��� ���ؿ°ž�? ";
        tempArray[1] = "���� �ʹ� �������� ���̾�! ���� ���� ģ��. ";
        tempArray[2] = "�ٵ� ���̾�, ��� �̰� ������ ���������� �ƴϾ�. ���� ���� �� �� ��� ���������� �ƴ϶��..";
        tempArray[3] = "�� �Ծ���� ���� �Ҹ��İ�? ��ġ�� ���� ���� ������ ���������� �����ϰ� ¬©�� �Ϲٳ� ���������̾��� ���̾�!";
        tempArray[4] = "��������� ������ ���� �ƴϾ�����. �׷��ٸ� ������ ���ù����� ���� Ʋ������.";
        tempArray[5] = "��? ������ ���� �� �Ա���? �׷� ���� ����� ������ ���������� �������ְھ�? ";
        tempArray[6] = "����! ���� ģ���ۿ� ����! ������ ���������� �������� ���� ������� ��� �ʴ��ؼ� ��Ƽ�� ������. ";
        tempArray[7] = "�׷� �� �ٽ�, ����� ����!";


        return tempArray;
    }
    void ShowNextText()
    {
        textNum++;
        if (textNum < firstChracterSaying.Length)
        {
            sayingText.text = firstChracterSaying[textNum];
            if(midScene == true)
            {
                if(textNum == 1)
                {
                    Sprite[] sprites = Resources.LoadAll<Sprite>("Character/Neoguri_Character");
                    secondCharacterImage.sprite = sprites[4];
                    secondCharacterImage.color = new Color(1, 1, 1, 1);
                    firstCharacterImage.color = new Color(1, 1, 1, 0);
                }
                if(textNum == 5)
                {
                    Sprite[] sprites = Resources.LoadAll<Sprite>("Character/Neoguri_Character");
                    firstCharacterImage.sprite = sprites[6];
                    firstCharacterImage.color = new Color(1, 1, 1, 1);
                    secondCharacterImage.color = new Color(1, 1, 1, 0);
                }
            }
        }
        else
        {
            neoguriScript.SetActive(false);

            // ��Ż ������Ʈ ���̰���.
            portalObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
