using System.Collections;
using UnityEngine;

public class Slingshot : MonoBehaviour {

    //fields set in the Unity Inspector pane
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;

    //filds set dynamically
    [Header("Set Dynamically")]


    public GameObject launchPoint;

    void Awake()    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
    }
	void OnMouseEnter () {
        //print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit ()    {
        //print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }
}
