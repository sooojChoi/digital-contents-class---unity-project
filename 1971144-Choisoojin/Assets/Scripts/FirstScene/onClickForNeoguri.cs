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
    public GameObject portalObject;  // ��Ż ������Ʈ(�ʱ����� ��ȭ�� ������ ���δ�)

    // Start is called before the first frame update
    void Start()
    { 
        // ó������ ��Ż�� ������ �ʰ� �Ѵ�. (�ʱ����� ��ȭ�� ������ ��Ż�� ����)
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
        firstChracterSaying = InitTextArray();
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
    void ShowNextText()
    {
        textNum++;
        if (textNum < firstChracterSaying.Length)
        {
            sayingText.text = firstChracterSaying[textNum];
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
