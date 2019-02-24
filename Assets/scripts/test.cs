using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public GameObject qiu;
    public float t;

    private void Awake()
    {
        //Debug.Log("Awake");
    }

    private void OnEnable()
    {
        //Debug.Log("OnEnable");
    }

    // Use this for initialization
    void Start () {
        t = Vector3.Distance(qiu.transform.position, this.transform.position);


        //Debug.Log("111");
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(0, t/20, 0, Space.Self);
    }
}
