using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagere : MonoBehaviour
{
    public GameObject a;
    public GameObject w;
    public GameObject GO;
    public float time;
    public List<PlayerController> PC;
    public List<Line> Linee;
    public bool start = false;
    public GameObject game;
    public List<GameObject> Finish;
    public int i = 0;
    public bool Clear = false;
    //public List<float> DistansePut;
    //public float DistansePUT1;
    //public float DistansePUT2;
    public List<DrawLine> drawline;
    public bool loked = false;
    public float distGame1;
    public float distGame2;
    public float dist;
    // Start is called before the first frame update
    void Start()
    {
        //DistansePUT1 = Vector2.Distance(PC[0].tochka.position, Finish[0].transform.position);
        //DistansePUT2 = Vector2.Distance(PC[1].tochka.position, Finish[1].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //if (drawline[0].enabled == true) Debug.Log("!!!!!!!!!");
        //if (drawline[1].enabled == true) Debug.Log("&&&&&&&&&");
        if (time>0) time -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            distGame1 = Vector2.Distance(Linee[0].transform.position, Input.mousePosition);
            distGame2 = Vector2.Distance(Linee[1].transform.position, Input.mousePosition);
            if (distGame1 < 20) { i = 0; drawline[0].state = false; drawline[1].state = true; }
            if (distGame2 < 20) { i = 1; drawline[1].state = false; drawline[0].state = true; }

            if(distGame1 > 20 && distGame2 > 20)
            {
                drawline[0].state = true;
                drawline[1].state = true;
                Destroy(game);
            }
        }
        if(Input.GetMouseButton(0) && start == false)
        {
            Transform LastPosition;
            if (time <= 0)
            {
                if (game == null)
                {
                    LastPosition = transform;
                }
                else
                    LastPosition = game.transform;
                if(i == 0 && loked == true)
                {
                    i = 1;
                }
                if(i == 0)
                    game = Instantiate(GO, a.transform);
                if(i == 1)
                    game = Instantiate(GO, w.transform);
                game.transform.position = Input.mousePosition;


                //DistansePut[i] += Vector2.Distance(LastPosition.position, game.transform.position);
                PC[i].Move.Add(game);
                //Debug.Log(i);
                time = 0.01f;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            dist = Vector2.Distance(Finish[i].transform.position, game.transform.position);
            if (dist > 50)
            {
                if (i == 0)
                {
                    for (int b = 0; b < a.transform.childCount; b++)
                    {
                        Destroy(PC[i].Move[b]);
                    }
                }
                if (i == 1)
                {
                    for (int b = 0; b < w.transform.childCount; b++)
                    {
                        Destroy(PC[i].Move[b]);
                    }
                }
                PC[i].Move.Clear();
                drawline[i].lineDraw.positionCount = 0;
                if (drawline[i].enabled == true) {drawline[i].clear = true; }
                start = false;
                //i--;
                Debug.Log("Clear");
            }
            if(dist< 50)
            {
                i++;
            }
            //if (Linee.Count > 1 && i < Linee.Count)
            //{
            //    i++;
            //}
            //if(PC[0].Move.Count != 0)
            //{
            //    loked= true;
            //}
            if(a.transform.childCount > 0)
            {
                drawline[0].state = true;
                Debug.Log("a>0");
            }
            if(w.transform.childCount > 0)
            {
                drawline[1].state = true;
                Debug.Log("w>0");
            }
            if(a.transform.childCount == 0)
            {
                drawline[0].state = false;
                Debug.Log("a==0");
                i = 0;
            }
            if(w.transform.childCount == 0)
            {
                drawline[1].state = false;
                Debug.Log("w==0");
            }
            //if (PC[0].Move.Count != 0 && PC[1].Move.Count == 0)
            //{
            //    drawline[0].enabled = false;
            //    drawline[1].enabled = true;
            //}
            //if (PC[0].Move.Count == 0 && PC[1].Move.Count != 0)
            //{
            //    drawline[0].enabled = true;
            //    drawline[1].enabled = false;
            //}
            //if (PC[0].Move.Count != 0 && PC[1].Move.Count != 0)
            //{
            //    drawline[0].enabled = false;
            //    drawline[1].enabled = false;
            //    i = 2;
            //}
            //else { i = 0; }

            if (distGame1 < 50 && dist < 50 && a.transform.childCount > 0 && i == 1)
            {
                drawline[0].start = true;
                Debug.Log("0 = start");
            }
            if(distGame2 < 50 && dist < 50 && w.transform.childCount > 0 && i==1)
            {
                drawline[1].start = true;
                Debug.Log("1 = start");
            }

            if (dist < 50 && drawline[0].state == true && drawline[1].state == true)
            {
                Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                start = true;
                Linee[0].Lists();
                Linee[1].Lists();
            }
        }

    }
}
