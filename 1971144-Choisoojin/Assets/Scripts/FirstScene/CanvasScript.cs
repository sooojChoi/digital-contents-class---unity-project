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

    // Start is called before the first frame update
    void Start()
    {
        firstChracterSaying = InitTextArray();
        sayingText.text = firstChracterSaying[textNum];
        nextButton.onClick.AddListener(ShowNextText);
    }

    // Update is called once per frame
    void Update()
    {
        
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
