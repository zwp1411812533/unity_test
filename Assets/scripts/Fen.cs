using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fen : MonoBehaviour {

    public Rect rec = new Rect(0,0,0,0);
    public GameObject qiugan;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (!qiugan.activeSelf)
        {
            Goal();
        }

        


    }

    //判断是否进球
    void Goal()
    {
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag("ball");
        foreach (GameObject gameChildren in gameObject)
        {
            if(gameChildren.transform.position.z > 0)
            {
                gameChildren.SetActive(false);
                GameProgress.isGoal = true;
            }

        }
    }

    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(rec);
    //    GUILayout.BeginHorizontal();

    //    GUILayout.Box("1");
    //    GUILayout.FlexibleSpace();
    //    GUILayout.Box("2");


    //    GUILayout.EndHorizontal();
    //    GUILayout.EndArea();

    //}


}
