using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pigGenerator : MonoBehaviour
{
    public GameObject obj; //MonsterPrefab ����

    // ��� ������ �������� �������� �˱� ���� startX, endX, startY ����.
    public float startX = 0.0f; 
    public float endX = 0.0f;
    public float positionY = 0.0f;

    public int monsterNumber = 1;   // ó���� ������ ���� ����

    // Start is called before the first frame update
    void Start()
    {
        // ���� ����
        generateMonster(obj, startX, endX, positionY, monsterNumber);
    }

    public static void generateMonster(GameObject mon, float startx, float endx, float y, int num)
    {
        SpriteRenderer sr = mon.GetComponent<SpriteRenderer>();

        // ���͸� num�� ��ŭ ���� �����Ѵ�.
        for (int i = 0; i < num; i++)
        {
            float rndX = Random.Range(startx, endx); // startX ~ endX ������ �Ǽ��� ����
            int rndR = Random.Range(0, 2);  // 0 �Ǵ� 1�� ����

            Vector2 pos = new Vector2(rndX, y);
            // ��� ������ �ٶ󺸰� ���������� �����̴�.
            if (rndR == 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }

            Instantiate(mon, pos, mon.transform.rotation);  // ���͸� �����Ѵ�.
        }

    }
}
