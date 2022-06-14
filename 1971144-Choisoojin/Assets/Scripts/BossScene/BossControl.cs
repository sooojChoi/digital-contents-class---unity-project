using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossControl : MonoBehaviour
{
    int hp;

    Animator animator;

    public GameObject rock;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    string golemState;  // ���� ���� ���¸� ��Ÿ�� (alive, die �� �ϳ�)
    public GameObject realCorn;  // ��¥ ��������
    public static int numOfRealCorn;

    public Image foregroundImage;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        float rndTime = Random.Range(2.5f, 4); // �������� �ʴ� �޽� �ð�.
        StartCoroutine("delayTime", rndTime);

        hp = 1000;
        SetMaxHealth(hp);

        golemState = "alive";

        numOfRealCorn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp < 0)
        {
            animator.SetBool("die", true);
            golemState = "die";

            StartCoroutine("dieMonster");

        }
        SetHealth(hp);

        if(numOfRealCorn == 10)
        {
            numOfRealCorn += 1;
            FadeOut(2, goToOtherScene);   // ȭ���� ���� ��ο�����, �� �̵��Ѵ�.
        }
    }

    IEnumerator delayTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        float rndX = Random.Range(-7, 3); // startX ~ endX ������ �Ǽ��� ����

        animator.SetTrigger("throw");
        // ���� �����Ѵ�.
        Instantiate(rock, new Vector2(rndX, 10), rock.transform.rotation);
        Instantiate(rock, new Vector2(rndX+7, 10), rock.transform.rotation);  
        Instantiate(rock, new Vector2(rndX + 14, 10), rock.transform.rotation);  

        float rndTime = Random.Range(2.5f, 4); // �������� �ʴ� �޽� �ð�.

        if(golemState == "alive")
        {
            StartCoroutine("delayTime", rndTime);
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        // ���� ���� ������Ʈ�� �������� �浹�ߴµ� �� �浹ü�� �±װ� "Weapon"�̶��, 
        if (collision.gameObject.tag == "Weapon")
        {
            hp -= 50;
            Destroy(collision.gameObject);

        }else if(collision.gameObject.tag == "Bomb")
        {
            hp -= 70;
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    IEnumerator dieMonster()
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);

        for(int i = 0; i < 10; i++)
        {
            int rndR = Random.Range(0, 5);  // 0 �Ǵ� 1�� ����
            int plusMinus = Random.Range(0, 2);
            if(plusMinus == 0)
            {
                Vector3 pos = new Vector3(rndR, 0, 0);
                Instantiate(realCorn, gameObject.transform.position + pos, realCorn.transform.rotation);  // ���������� �����Ѵ�.
            }
            else
            {
                Vector3 pos = new Vector3(rndR*-1, 0, 0);
                Instantiate(realCorn, gameObject.transform.position + pos, realCorn.transform.rotation);  // ���������� �����Ѵ�.
            }
        }
    }

    void goToOtherScene()
    {
        SceneManager.LoadScene("AnimationScene");
    }
    public void FadeOut(float fadeOutTime, System.Action nextEvent = null)
    {
        StartCoroutine(CoFadeOut(fadeOutTime, nextEvent));
    }

    // ���� -> ������
    IEnumerator CoFadeOut(float fadeOutTime, System.Action nextEvent = null)
    {
        float black = 0.5f;
        foregroundImage.transform.SetAsLastSibling();
        Color tempColor = foregroundImage.color;
        while (tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / fadeOutTime;
            foregroundImage.color = tempColor;

            if (tempColor.a >= 1f) tempColor.a = 1f;

            yield return null;
        }

        foregroundImage.color = tempColor;

        while (black < 1f)
        {
            black += Time.deltaTime / fadeOutTime;

            yield return null;
        }
        if (nextEvent != null) nextEvent();
    }
}
