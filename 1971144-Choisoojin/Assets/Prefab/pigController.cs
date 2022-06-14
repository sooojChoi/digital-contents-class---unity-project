using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pigController : MonoBehaviour
{
    Animator animator;
    // ���Ͱ� ������ ������ �������� �̹����� flip�� �����ϱ� ���� �ʿ�.
    // ���� ���� �̹����� flipX�� false�� �������� ����, true�̸� �ϸ� ������ ����.
    SpriteRenderer sr;

    public GameObject fakeCorn;  // ���Ͱ� �����鼭 �����Ǵ� ������

    public int whoAmI = 0;  // 1���� ������ 1, 2�����̸� 2, 3�����̸� 3

    // ��� ������ �������� �������� �˱� ���� startX, endX, positionY ����.
    public float startX = 0.0f;
    public float endX = 0.0f;
    public float positionY = 0.0f;
    public float speed = 5f;//Monster�� �̵� �ӵ�
    string direction = "any";  // ���Ͱ� �̵��� ����. "any", "left", "right" �� �ϳ��̴�.

    public float idleMinTime = 1.5f;
    public float idleMaxTime = 3.0f;
    public float walkingMinTime = 2.0f;
    public float walkingMaxTime = 4.0f;

    float offset = 0.0f;  // ������ �̵� ������ ������ �� �̿��Ѵ�.

    // ���� ������ ����
    string state = "idle";

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        float rndTime = Random.Range(idleMinTime, idleMaxTime); // idle ���� �����ð�.
        StartCoroutine("delayTime", rndTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "walking")
        {
            if(sr.flipX == false)  // ���������� �̵��� ��.
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else if(sr.flipX == true){  //�������� �̵��� ��.
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }


            // �̵��� ��ġ�� ������ ������ �����, �ȴ� ���� ���߰� idle���·� ��ȯ.
            if(transform.position.x < startX+ offset || transform.position.x > endX - offset)
            {
                // ������ �ȱ� ������ ���� �ݴ� �������� �ɾ���� �Ѵ�.
                if(transform.position.x < startX + offset)
                {
                    direction = "right";
                }
                else
                {
                    direction = "left";
                }
                state = "idle";
                animator.SetBool("isWalking", false);
            }
        }
    }

    IEnumerator delayTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        if (state == "idle")
        {
            state = "walking";
            animator.SetBool("isWalking", true);

            if(direction == "any")
            {
                // 0 �Ǵ� 1�� ����, ���� �ɾ�� ������ �������� ���Ѵ�.
                int rndFlip = Random.Range(0, 2); 

                if (rndFlip == 0)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }
            }
            else if(direction == "left")
            {
                // �������� ������.
                sr.flipX = true;
            }else if(direction == "right")
            {
                // ���������� ������.
                sr.flipX = false;
            }
            
            float rndTime = Random.Range(walkingMinTime, walkingMaxTime); // walking ���� �����ð�.
            StartCoroutine("delayTime", rndTime);
        }
        else if(state == "walking")
        {
            state = "idle";
            animator.SetBool("isWalking", false);
            float rndTime = Random.Range(idleMinTime, idleMaxTime); // idle ���� �����ð�.
            StartCoroutine("delayTime", rndTime);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("������ ���𰡶� �浹��.");
        // ���� ���� ������Ʈ�� �������� �浹�ߴµ� �� �浹ü�� �±װ� "Weapon"�̶��, 
        if (collision.gameObject.tag == "Weapon")
        {
            Debug.Log("������ ����� �浹��.");
            if (whoAmI == 1)
            {
                monsterManager.monsterNumPoint1 -= 1;
            }
            else if (whoAmI == 2)
            {
                monsterManager.monsterNumPoint2 -= 1;
            }
            else if (whoAmI == 3)
            {
                monsterManager.monsterNumPoint3 -= 1;
            }
            monsterManager.killNumber += 1;  // ���� ���� ���� ���� 1��ŭ �����Ѵ�.
            Destroy(collision.gameObject);


            animator.SetBool("isDie", true);

            // ���� ���ְ� �� �浹ü�� ���ֶ�.
            StartCoroutine("dieMonster");
        }
    }



    IEnumerator dieMonster()
    {   
        yield return new WaitForSecondsRealtime(0.8f);
        Destroy(gameObject);
        Instantiate(fakeCorn, gameObject.transform.position, fakeCorn.transform.rotation);  // ���������� �����Ѵ�.
    }
}
