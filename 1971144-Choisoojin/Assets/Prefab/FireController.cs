using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float speed = 12f; //Fire�� �̵� �ӵ�
    public float dieTime = 1.0f;  // ���� �ð� ���Ŀ� ���������� ��.
    public float current = 0.0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveFire();
        current += Time.deltaTime;
        if(current > dieTime)
        {
            Destroy(gameObject);
        }
    }

    void moveFire()
    {
        transform.Translate(Vector3.down * speed* 2 * Time.deltaTime);
    }
}
