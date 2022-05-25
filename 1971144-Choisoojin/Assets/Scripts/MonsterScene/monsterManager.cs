using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterManager : MonoBehaviour
{
    public static int killNumber = 0;  // �÷��̾ ���� ���� �� (�� ����)

    public static int monsterNumPoint1 = 2;  // 1������ ���� �����ϴ� ���� ����
    public static int monsterNumPoint2 = 3;  // 2������ ���� �����ϴ� ���� ����
    public static int monsterNumPoint3 = 5;  // 3������ ���� �����ϴ� ���� ����

    // ��� ������ �������� �������� �˱� ���� startX, endX, positionY ����.(�������� �ٸ��� �����Ǵ� ��)
    float[] startX = new float[3];
    float[] endX = new float[3];
    float[] positionY = new float[3];

    public GameObject mon1;  // ����1�� �����Ǵ� ���� ������
    public GameObject mon2;  // ����2�� �����Ǵ� ���� ������
    public GameObject mon3;  // ����3�� �����Ǵ� ���� ������

    // Start is called before the first frame update
    void Start()
    {
        startX[0] = 12;
        endX[0] = 22;
        positionY[0] = 2.75f;

        startX[1] = 20;
        endX[1] = 36;
        positionY[1] = 10.8f;

        startX[2] = 56;
        endX[2] = 83;
        positionY[2] = 2.8f;

    }

    // Update is called once per frame
    void Update()
    {
        // ���� �־�� �ϴ� ���� ������ ���� ������(�÷��̾ �׿���), 3�ʵڿ� ���͸� �� ���� �� �����Ѵ�.
        if(monsterNumPoint1 < 2)
        {
            // 3�� �ڿ� �� �Լ��� ȣ���ϵ��� ��.
            StartCoroutine("generateObj", 1);
            monsterNumPoint1++;
        }
        if(monsterNumPoint2 < 3)
        {
            // 3�� �ڿ� �� �Լ��� ȣ���ϵ��� ��.
            StartCoroutine("generateObj", 2);
            monsterNumPoint2++;
        }
        if (monsterNumPoint3 < 5)
        {
            // 3�� �ڿ� �� �Լ��� ȣ���ϵ��� ��.
            StartCoroutine("generateObj", 3);
            monsterNumPoint3++;
        }

        
    }

    IEnumerator generateObj(int sort)
    {
        // sort�� ������ �ǹ�. 6�� ���� ���ο� ���͸� ������ ������ �����Ѵ�.
        yield return new WaitForSeconds(6.0f);

        if (sort == 1)
        {
            pigGenerator.generateMonster(mon1, startX[0], endX[0], positionY[0], 1);
        }
        else if (sort == 2)
        {
            pigGenerator.generateMonster(mon2, startX[1], endX[1], positionY[1], 1);
        }
        else if (sort == 3)
        {
            pigGenerator.generateMonster(mon3, startX[2], endX[2], positionY[2], 1);

        }
    }
}
