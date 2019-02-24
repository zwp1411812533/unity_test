using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gan : MonoBehaviour {

    public GameObject qiu;//球的位置信息
    public float rotateSpeed;//旋转速度

    private Vector3 mouseVect3;//当前鼠标的位置
    bool frist;//是否按下鼠标后第一次进入方法
    float qmDistanceOld;//上一帧鼠标与球的距离
    float t;//鼠标松开的瞬间，球杆和球的距离

    bool mouseDown;//鼠标是否按下
    bool mouseUp;//鼠标是否抬起
    Vector3 qiuGanPosition;
    Vector3 qiuPosition;


    //球杆击中球之后，会被隐藏，桌上所有球都停止运动时，球杆会出现，所有将初始化放到启用中
    private void OnEnable()
    {
        transform.position = new Vector3(qiu.transform.position.x + 6, qiu.transform.position.y, -2);
        transform.localEulerAngles = new Vector3(0,0,90);
        rotateSpeed = 20f;
        mouseDown = false;
        mouseUp = false;
        frist = true;
    }

    // Update is called once per frame
    void Update () {

        //判断是否到击球阶段，没到击球阶段跳出
        if (!GameProgress.isBattingStage)
        {
            return;
        }

        //每一帧获取球、球杆和鼠标的位置数据,并且将Z轴都置为0
        qiuGanPosition = transform.position;
        qiuGanPosition.z = 0;
        qiuPosition = qiu.transform.position;
        qiuPosition.z = 0;
        mouseVect3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVect3.z = 0;

        //判断鼠标的状态，是按下还是抬起
        if (Input.GetMouseButtonDown(0))
            mouseDown = true;
        if (Input.GetMouseButtonUp(0))
        {
            t = Vector3.Distance(qiu.transform.position, qiuGanPosition);
            mouseDown = false;
            mouseUp = true;
        }

        //调整球杆的角度
        if (!mouseDown && !mouseUp)
        {

            //运用三角函数算出鼠标位置，球，球杆之间的夹角
            float a = Vector3.Distance(qiuGanPosition, qiuPosition);
            float b = Vector3.Distance(mouseVect3, qiuPosition);
            float c = Vector3.Distance(qiuGanPosition, mouseVect3);
            float cosJiao = (a * a + b * b - c * c) / (2 * a * b);//使用余弦定理，算出cos()的值
            float jiao = Mathf.Acos(cosJiao) / Mathf.Deg2Rad;//算出角度

            //cos的角度只会无限接近0，角度等于0会报错，这里加个判断防止角度降到0
            if(jiao > 0.01)
            {
                //判断出鼠标处于球杆的哪一侧
                Vector3 dir = transform.position - mouseVect3;
                float dot1 = Vector3.Dot(transform.right, dir.normalized);

                //大于零在下侧，将角度改为负数，顺时针旋转
                if (dot1 > 0)
                {
                    jiao = jiao * -1;
                }
                //旋转球杆，旋转点，旋转方向，旋转角度（速度）
                transform.RotateAround(qiuPosition, Vector3.forward, jiao / rotateSpeed);
                Debug.DrawLine(qiu.transform.position, transform.position + transform.up * 100, Color.black);
            }




        }

        //移动球杆，控制距离
        if (mouseDown)
        {
            
            float qmDistance = Vector3.Distance(qiuPosition, mouseVect3);
            //因为要判断上一帧的距离到这一帧距离的差值，所以判断是否是第一次进入该方法
            if (frist)
            {
                qmDistanceOld = qmDistance;
                frist = false;
            }

            float oldToNew = qmDistanceOld - qmDistance;//算出上一帧到这一帧之间的差值

            float qgDistance = Vector3.Distance(qiuPosition, qiuGanPosition);//球杆与球之间的距离
            //让球杆在一定范围内移动
            if (qgDistance - oldToNew <= 6f)
            {
                oldToNew = qgDistance - 6f;
            }
            else if (qgDistance - oldToNew >= 10f)
            {
                oldToNew = qgDistance - 10f;
            }
            
            this.transform.Translate(0, oldToNew, 0, Space.Self);//移动球杆，在球杆的轴上
            qmDistanceOld = qmDistance;//这一帧的距离保存到下一帧使用


        }

        //打出球杆
        if (mouseUp)
        {
            this.transform.Translate(0, t / 10, 0, Space.Self);
        }


    }


}
