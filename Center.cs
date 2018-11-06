using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour {  
    public GameObject CDoor1;
    public GameObject CDoor2;
    public GameObject CDoor3;
    public GameObject CDoor4;
    public GameObject CDoor5;
    public GameObject CDoor6;
    public  Camera tpp;
    public  Camera fpp;
    public static bool inCent = true;	
	// Update is called once per frame
	void Update () {
        if (inCent)
        {
            Center_Doors();
        }	
	}
    public  void Center_Doors()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            Ray ray;
            if (Character.cam == Character.Cameras.FPPcamera)
            {
                ray = fpp.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                ray = tpp.ScreenPointToRay(Input.mousePosition);
            }
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.LogError(hit.collider.name);
                switch (hit.collider.name)
                {
                    case "cdoor1":
                        CDoor1.transform.Rotate(new Vector3(0,90f,0));
                        StartCoroutine(Rotation(3.0f,CDoor1, new Vector3(0, -90f, 0)));
                        break;
                    case "cdoor2":
                        CDoor2.transform.Rotate(new Vector3(0, -90f, 0));
                        StartCoroutine(Rotation(3.0f, CDoor2, new Vector3(0, 90f, 0)));
                        break;
                    case "cdoor3":
                        CDoor3.transform.Rotate(new Vector3(0, 90f, 0));
                       StartCoroutine(Rotation(3.0f, CDoor3, new Vector3(0, -90f, 0)));
                        break;
                    case "cdoor4":
                        CDoor4.transform.Rotate(new Vector3(0, -90f, 0));
                        StartCoroutine(Rotation(3.0f, CDoor4, new Vector3(0, 90f, 0)));
                        break;
                    case "cdoor5":
                        CDoor5.transform.Rotate(new Vector3(0, 90f, 0));
                        StartCoroutine(Rotation(3.0f, CDoor5, new Vector3(0, -90f, 0)));
                        break;
                    case "cdoor6":
                        CDoor6.transform.Rotate(new Vector3(0, -90f, 0));
                        StartCoroutine(Rotation(3.0f, CDoor6, new Vector3(0, 90f, 0)));
                        break;
                }
            }
        }
    }
    public static IEnumerator Rotation(float waitTime,GameObject door, Vector3 vector)
    {
        yield return new WaitForSeconds(waitTime);
        door.transform.Rotate(vector);
    }
}
