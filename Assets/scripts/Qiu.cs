using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qiu : MonoBehaviour {

    public GameObject gan;
    public float speed;//速度

    Vector3 qiuGan;
    float t;//球杆与球的距离，决定球被击打的速度

    private void OnEnable()
    {



        speed = 10000;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        
    }

    // Update is called once per frame
    void Update () {
        //if (!gan.active)
        //{
        //    gan.SetActive(true);
        //}

        //判断是否到击球阶段，没到击球阶段跳出
        if (!GameProgress.isBattingStage)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            qiuGan = gan.transform.position;
            qiuGan.z = 0;
            t = (Vector3.Distance(qiuGan, this.transform.position) - 5.9f) / 4f;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 moveDirection = other.transform.up;

        other.gameObject.SetActive(false);
        GetComponent<Rigidbody>().AddForce(moveDirection * speed * t, ForceMode.Force);
        GameProgress.isBattingStage = false;//球被击打出去，击球阶段结束
        //gan.SetActive(false);

    }

}
