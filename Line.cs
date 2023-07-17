using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Line : MonoBehaviour
{
    [Range(0, 1)] public float value;
    List<Transform> line2;
    public List<float> distansLine2;
    public List<float> lerpLine2;
    float sumlerpLine2;
    float coefficientLine2;
    int countLine2;
    public Transform object2;
    public Transform parentLine2;
    public GameObject End;
    public GameObject Player1;
    public GameObject Finish1;
    public float Distanse1;
    public float Distanse2;
    public GameObject Bam;
    public Animator anim;
    public float NapravlienieX;
    public float NapravlienieY;
    public float NapravlienieX1;
    public float NapravlienieY1;
    float timeNapravlenie = 0.1f;
    void Start()
    {
        lerpLine2 = new List<float>();
        distansLine2 = new List<float>();
        line2 = new List<Transform>();

        RefreshLine2();

        value = 0;
        StartCoroutine(PlusValue());
    }
    private void FixedUpdate()
    {
        if (timeNapravlenie <= 0 && NapravlienieX1 == 0)
        {
            NapravlienieX1 = gameObject.transform.position.x;
            NapravlienieY1 = gameObject.transform.position.y;
            timeNapravlenie = 0.1f;
        }

        if (timeNapravlenie <= 0 && NapravlienieX1 != 0)
        {
            NapravlienieX = gameObject.transform.position.x - NapravlienieX1;
            NapravlienieY = gameObject.transform.position.y - NapravlienieY1;
            NapravlienieX1 = 0;
            anim.SetFloat("SpeedX", NapravlienieX/10);
            anim.SetFloat("SpeedY", NapravlienieY/10);
            timeNapravlenie = 0.1f;
        }
        else timeNapravlenie -= Time.deltaTime;
        Distanse1 = Vector2.Distance(transform.position, Player1.transform.position);
        Distanse2 = Vector2.Distance(transform.position, Finish1.transform.position);

        if (Distanse1 < 50) {GameOver(); }
        if (Distanse2 < 50) Winn();
    }
    public void Lists()
    {
        anim.SetBool("Walk", true);
        lerpLine2 = new List<float>();
        distansLine2 = new List<float>();
        line2 = new List<Transform>();

        RefreshLine2();
        value = 0;
        StartCoroutine(PlusValue());
    }
    public void RefreshLine2()
    {
        parentLine2.GetComponentsInChildren<Transform>(line2);
        countLine2 = line2.Count;
        lerpLine2.Clear();
        distansLine2.Clear();
        float sumDistanceLine2 = 0;
        for (int i = 1; i < line2.Count - 1; i++)
        {
            float distance = Vector3.Distance(line2[i].position, line2[i + 1].position);
            sumDistanceLine2 += distance;
            distansLine2.Add(distance);
        }
        coefficientLine2 = 1 / sumDistanceLine2;
        foreach (var item in distansLine2)
        {
            sumlerpLine2 += item * coefficientLine2;
            lerpLine2.Add(sumlerpLine2);
        }
        sumlerpLine2 = 0;
    }
    void LerpLine2()
    {
        if (value <= lerpLine2[0])
        {
            float test2 = (1 / (lerpLine2[0])) * value;
            object2.position = Vector3.Lerp(line2[1].position, line2[2].position, test2);
        }
        else
        {
            Lerp2Line2(1);
        }
    }
    void Lerp2Line2(int count)
    {
        if (count < lerpLine2.Count)
        {
            if (value <= lerpLine2[count])
            {
                float test2 = (1 / (lerpLine2[count] - lerpLine2[count - 1])) * (value - lerpLine2[count - 1]);
                object2.position = Vector3.Lerp(line2[count + 1].position, line2[count + 2].position, test2);
            }
            else
            {
                Lerp2Line2(count + 1);
            }
        }
    }
    IEnumerator PlusValue()
    {
        while (value <= 1)
        {
            yield return new WaitForSeconds(0.01f);
            value += 0.005f;
            Move();
        }
        //StartCoroutine(MinusValue());
    }
    IEnumerator MinusValue()
    {
        while (value >= 0)
        {
            yield return new WaitForSeconds(0.01f);
            value -= 0.05f;
            Move();
        }
        StartCoroutine(PlusValue());
    }
    void Move()
    {
        if (parentLine2.childCount != countLine2 - 1)
        {
            RefreshLine2();
        }
        LerpLine2();
        DrawLine2();
    }
    void DrawLine2()
    {
        for (int i = 1; i < line2.Count - 1; i++)
        {
            Debug.DrawLine(line2[i].position, line2[i + 1].position, Color.red, 0.01f);
        }
    }
    public void GameOver()
    {
        StopAllCoroutines();
        End.SetActive(true);
        End.GetComponent<End>().Falied();
    }
    private void Winn()
    {
        StopAllCoroutines();
        End.SetActive(true);
        End.GetComponent<End>().Win();
    }
}