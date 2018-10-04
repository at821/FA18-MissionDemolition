using System.Collections;
using UnityEngine;

public class Slingshot : MonoBehaviour {
    static private Slingshot S;
    //fields set in the Unity Inspector pane
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    //filds set dynamically
    [Header("Set Dynamically")]


    public GameObject launchPoint;

    public Vector3 launchPos;

    public GameObject projectile;

    public bool aimingMode;

    private Rigidbody projectileRigidbody;

    static public Vector3 LAUNCH_POS    {
        get        {
            if (S == null) return Vector3.zero;
            return S.launchPos;
        }
    }
       
    void Awake()    {
        S = this;

        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
	void OnMouseEnter () {
        //print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit ()    {
        //print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }


    void OnMouseDown ()    {
        //the player has pressed the mouse button while over slingshot
        aimingMode = true;
        //instantiate a projectile
        projectile = Instantiate(prefabProjectile) as GameObject;
        //start it at the launchpoint
        projectile.transform.position = launchPos;
        //set it to is kinematic for now
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
        
    }

    void Update()    {
        
        //if slingshot is not in aimingmode, don't run this code
        if (!aimingMode) return;
        
        //get current mouse position in 2D screen coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
       
        //find the delta from the launchpos to the mousepos3D
        Vector3 mouseDelta = mousePos3D = launchPos;
        
        //limit mousedelta to the radius of the slingshot sphere collider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        //move the projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))        {
            //The mouse had been released
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
        }

    }
}
