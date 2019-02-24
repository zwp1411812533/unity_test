using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

    public GameObject qiugan;
    public GameObject qiu;//白球
    public GameObject fangZhiQiu;//白球进袋后，用替代球来放置

    private Vector3 mouseVect3;

    bool distance;//距离

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (!qiugan.activeSelf && qiu.activeSelf)
        {
            //为了防止球还没开始运动，球杆就被激活，延时一段时间执行代码
            Invoke("QiuGanEnable", 0.1f);//延时执行，第一个参数：方法名；第二个参数：时间，单位秒
        }
        if (!qiu.activeSelf)
        {
            Invoke("QiuEnable", 0.1f);
        }

    }

    void QiuGanEnable()//启用球杆
    {
        if (QiuActive() && qiu.activeSelf)
        {
            //所有球都停止移动了，将球杆激活
            qiugan.SetActive(true);
        }
    }

    void QiuEnable()//放置白球球
    {
        if (QiuActive())
        {
            followMouse();
            fangZhiQiu.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                Distance();

                if (!distance)
                {
                    fangZhiQiu.SetActive(false);
                    qiu.transform.position = fangZhiQiu.transform.position;
                    qiu.SetActive(true);

                }

            }
        }
    }

    //判断球是否在移动
    bool QiuActive()
    {
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag("ball");//获取所有的球
        //遍历所有的球，判断是否在移动
        foreach (GameObject gameChildren in gameObject)
        {
            //判断速度是否为0
            if (gameChildren.GetComponent<Rigidbody>().velocity.magnitude != 0)
            {
                return false;
            }
        }
        return true;
    }

    //让白球跟随鼠标
    void followMouse()
    {
        mouseVect3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVect3.z = -0.26f;
        if (mouseVect3.x > 12.4f)
        {
            mouseVect3.x = 12.4f;
        }
        if (mouseVect3.x < -12.4f)
        {
            mouseVect3.x = -12.4f;
        }
        if (mouseVect3.y > 6f)
        {
            mouseVect3.y = 6f;
        }
        if (mouseVect3.y < -6f)
        {
            mouseVect3.y = -6f;
        }

        fangZhiQiu.transform.position = mouseVect3;
    }

    void Distance()
    {
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag("ball");//获取所有的球
        //遍历所有的球，判断是否在移动
        foreach (GameObject gameChildren in gameObject)
        {
            distance = Vector3.Distance(fangZhiQiu.transform.position, gameChildren.transform.position) < 0.55f ? true : false;
            if (distance)
            {
                break;
            }
        }
    }

}
