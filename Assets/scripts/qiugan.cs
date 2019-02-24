using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qiugan : MonoBehaviour {

    private Vector2 mouseVect2;
    private Vector3 mouseVect3;
    public float a;
    public float x;

    Vector3 qiugan1;
    public Rigidbody m_rigid;
    public GameObject qiu;
    Vector3 v1;
    Quaternion q1;
    Quaternion q2;
    float time1;

    private Vector3 mouseOld;
    private Vector3 mouseNew;
    bool frist;
    float qmo;
    float t;

    bool mouseDown;
    bool mouseUp;



    void Awake()
    {
        a = 20f;
        time1 = 0f;
        qmo = 0;


    }

    // Use this for initialization
    void Start () {
        v1 = qiu.transform.position - this.transform.position;
        q1 = this.transform.rotation;
        frist = true;
        mouseDown = false;
        mouseUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
            mouseDown = true;
        if (Input.GetMouseButtonUp(0))
        {
            t = Vector3.Distance(qiu.transform.position, this.transform.position);
            mouseDown = false;
            mouseUp = true;
        }
           


            ////获取鼠标的坐标，鼠标是屏幕坐标，Z轴为0，这里不做转换
            //Vector3 mouse = Input.mousePosition;
            //Debug.Log("mouse---X:" + mouse.x + "   Y:" + mouse.y + "   Z:" + mouse.z);
            ////获取物体坐标，物体坐标是世界坐标，将其转换成屏幕坐标，和鼠标一直
            //Vector3 obj = Camera.main.WorldToScreenPoint(transform.position);
            //obj.z = 0;
            //Debug.Log("obj---X:" + obj.x + "   Y:" + obj.y + "   Z:" + obj.z);
            ////屏幕坐标向量相减，得到指向鼠标点的目标向量，即黄色线段
            //Vector3 direction = mouse - obj;
            ////将Z轴置0,保持在2D平面内
            //direction.z = 0f;
            ////将目标向量长度变成1，即单位向量，这里的目的是只使用向量的方向，不需要长度，所以变成1
            //direction = direction.normalized;
            ////当目标向量的Y轴大于等于0.4F时候，这里是用于限制角度，可以自己条件
            ////if (direction.y >= 0.4f)
            ////{
            ////物体自身的Y轴和目标向量保持一直，这个过程XY轴都会变化数值
            //transform.up = direction;
            ////}

            //Debug.Log("y=" + Input.GetAxis("Mouse Y") + ";x=" + Input.GetAxis("Mouse X"));

            if (!mouseDown && !mouseUp)
        {

            Debug.Log("11111");
            //获取鼠标的世界坐标位置
            mouseVect3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseVect3.z = 0;//将鼠标位置和球杆处在同一平面上，Z = 0
            //运用三角函数算出鼠标位置，球，球杆之间的夹角
            float a1 = Vector3.Distance(transform.position, qiu.transform.position);
       
            float b1 = Vector3.Distance(mouseVect3, qiu.transform.position);
       
            float c1 = Vector3.Distance(transform.position, mouseVect3);
       
            float ss = (a1 * a1 + b1 * b1 - c1 * c1) / (2 * a1 * b1);
            
            float j = Mathf.Acos(ss) / Mathf.Deg2Rad;
         

            //判断出鼠标处于球杆的哪一侧
            Vector3 dir = transform.position - mouseVect3;
            float dot1 = Vector3.Dot(transform.right, dir.normalized);
            //大于零在下侧，将角度改为负数，顺时针旋转
            if (dot1 > 0)
            {
                j = j * -1;
            }
            //旋转球杆，旋转点，旋转方向，旋转角度（速度）
            transform.RotateAround(qiu.transform.position, Vector3.forward, j / a);
        }

        if (mouseDown)
        {
            mouseNew = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float qm = Vector3.Distance(qiu.transform.position, mouseNew);
            if (frist)
            {
                mouseOld = mouseNew;
                qmo = qm;
                frist = false;
            }
            
           
            Vector3 dir = mouseOld - mouseNew;
      

            float f2 = qmo - qm;
            Debug.Log("f2" + f2);
            float fff = Vector3.Distance(qiu.transform.position, this.transform.position);
            if (fff - f2 < 5.5f)
            {
                f2 = fff - 5.5f;
            }
            else if (fff - f2 > 10f)
            {
                f2 = 10f - fff;
            }
            


            //if ((Vector3.Distance(qiu.transform.position, this.transform.position) <= 5.5 && qmo > qm) || (Vector3.Distance(qiu.transform.position, this.transform.position) >= 10 && qmo < qm))
            //{
            //    qmo = qm;
            //}
            this.transform.Translate(0, f2, 0, Space.Self);
            qmo = qm;





            //float s = Mathf.Sin(Mathf.Deg2Rad * a);
            //Debug.Log(s);
            //Debug.Log(Mathf.Asin(s)/ Mathf.Deg2Rad);

            //mouseVect3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 dir = transform.position - mouseVect3;

            //float dot = Vector3.Dot(transform.forward, dir.normalized);//点乘判断前后   //dot >0在前  <0在后
            //Debug.Log("dot:" + dot);
            //float dot1 = Vector3.Dot(transform.right, dir.normalized);//点乘判断左右    //dot1>0在右  <0在左  
            //Debug.Log("dot1:" + dot1);
            //float angle = Mathf.Acos(Vector3.Dot(transform.forward.normalized, dir.normalized)) * Mathf.Rad2Deg;//通过点乘求出夹角
            //Debug.Log("angle:" + angle);

            ////方式2   叉乘
            ////叉乘满足右手准则  公式：模长|c|=|a||b|sin<a,b>  
            //Vector3 cross = Vector3.Cross(transform.forward, dir.normalized);////点乘判断左右  // cross.y>0在左  <0在右 
            //Debug.Log("cross:" + cross);
            //Vector3 cross1 = Vector3.Cross(transform.right, dir.normalized);////点乘判断前后  // cross.y>0在前  <0在后 
            //Debug.Log("cross1:" + cross1);
            //angle = Mathf.Asin(Vector3.Distance(Vector3.zero, Vector3.Cross(transform.forward.normalized, dir.normalized))) * Mathf.Rad2Deg;
            //Debug.Log("angle:" + angle);


            ////获取鼠标的世界坐标位置
            //mouseVect3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mouseVect3.z = 0;//将鼠标位置和球杆处在同一平面上，Z = 0
            ////运用三角函数算出鼠标位置，球，球杆之间的夹角
            //float a1 = Vector3.Distance(transform.position, qiu.transform.position);
            //Debug.Log("a1:" + a1);
            //float b1 = Vector3.Distance(mouseVect3, qiu.transform.position);
            //Debug.Log("b1:" + b1);
            //float c1 = Vector3.Distance(transform.position, mouseVect3);
            //Debug.Log("c1:" + c1);
            //float ss = (a1 * a1 + b1 * b1 - c1 * c1) / (2 * a1 * b1);
            //Debug.Log("ss:" + ss);
            //float j = Mathf.Acos(ss) / Mathf.Deg2Rad;
            //Debug.Log(j);

            ////判断出鼠标处于球杆的哪一侧
            //Vector3 dir = transform.position - mouseVect3;
            //float dot1 = Vector3.Dot(transform.right, dir.normalized);
            ////大于零在下侧，将角度改为负数，顺时针旋转
            //if (dot1 > 0)
            //{
            //    j = j * -1;
            //}
            ////旋转球杆，旋转点，旋转方向，旋转角度（速度）
            //transform.RotateAround(qiu.transform.position, Vector3.forward, j / a);


            //mouseVect3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mouseVect3.z = 0;
            ////这个是围绕（0,0,0）点旋转的，用处不大
            //transform.position = Vector3.RotateTowards(transform.position, mouseVect3,a,x);



            //mouseVect3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("mouseVect3:" + mouseVect3.ToString());
            //mouseVect3.z = 0;
            //Vector3 v = this.transform.position - mouseVect3;
            //Debug.Log("v:" + v.ToString());
            //this.transform.Translate(v, Space.World);






            //从一个角度旋转到另一个角度，（最后一个数字是旋转速度）
            //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, qiu.transform.rotation, Time.deltaTime);

            //Debug.Log("鼠标按下");
            //Debug.Log("y=" + Input.GetAxis("Mouse Y") + ";x=" + Input.GetAxis("Mouse X"));

            ////获取鼠标在屏幕上的位置
            ////mouseVect2 = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * a;
            //mouseVect3 = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * a;

            //让物体移动到指定位置，最后一个参数是以世界坐标系移动还是自身坐标系移动
            //this.transform.Translate(mouseVect3, Space.World);

            //向量加减法
            //Vector3 v2 = qiu.transform.position - this.transform.position;
            //q2 = Quaternion.FromToRotation(v1, v2) * q1;
            //q2.x = 0;
            //q2.y = 0;
            //this.transform.rotation = q2;

            //if (Vector3.Distance(transform.position, qiu.transform.position) < 6)
            //{
            //    Vector3 v3 = this.transform.position;
            //    v3.x = qiu.transform.position.x + 6;
            //    this.transform.Translate(v3, Space.Self);
            //}

            //this.transform.Translate(Vector3.Normalize(qiu.transform.position - transform.position) * 
            //    (Vector3.Distance(transform.position, qiu.transform.position) / (x * Time.deltaTime)),Space.Self);
        }

        if (mouseUp)
        {
            this.transform.Translate(0, t / 10, 0, Space.Self);
        }


        if (Input.GetMouseButtonUp(0))
        {
            //this.transform.Translate(Vector3.Normalize(qiu.transform.position - transform.position) * 
            //    (Vector3.Distance(transform.position, qiu.transform.position) / (5000 * Time.deltaTime)));
        }



        //目标位置
        //Vector3 aimPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        //aimPos.z = 0;
        //qiugan1 = transform.position;
        //qiugan1.z = 0;
        //Vector3 dir = (aimPos - qiugan1).normalized; //方向.

        //Quaternion targetRotation = Quaternion.LookRotation(dir, qiu.transform.up);
        //Quaternion newRotation = Quaternion.Lerp(m_rigid.rotation, targetRotation, 10 * Time.deltaTime);
        //m_rigid.MoveRotation(newRotation);

        //if (Vector3.Distance(aimPos, qiugan1) > 1f)
        //{
        //    m_rigid.MovePosition(dir * Time.fixedDeltaTime * 10 + transform.position);
        //    //transform.RotateAround(qiu.transform.position, Vector3.forward, 20 * Time.deltaTime);
        //}


    }

    //void OnMouseDrag()
    //{

    //}


    //private void OnMouseUp()
    //{
    //    Debug.Log("鼠标弹起");

    //    this.transform.Translate(Vector3.Normalize(qiu.transform.position - transform.position) * (Vector3.Distance(transform.position, qiu.transform.position) / (1 * Time.deltaTime)));

    //    Debug.Log("鼠标弹起");
    //}

}
