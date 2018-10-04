using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    static public GameObject POI; //static point of interest

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ; //desired Zpos of the camera

    void Awake()    {
        camZ = this.transform.position.x;
    }

    void FixedUpdate()    {
        //if there's only one line following an if, it doesn't need braces
        if (POI == null) return; //return if no poi

        //get position of the poi
        Vector3 destination = POI.transform.position;

        Vector3 destination;
        //if there is no poi, return to P:000
        if (POI == null)        {
            destination = Vector3.zero;
        } 
        else        {
            //get position of the poi
            destination = POI.transform.position;
                //if poi is a projectile, check see if rest
                if (POI.tag == "Projectile") {
                //if is sleeping/not moving
                    if (POI.GetComponent<Rigidbody>().IsSleeping()){
                    //return default view
                    POI = null;
                    //in the next update
                    return;
                    }
                }
        }
        
        
        //limit the XY to minimum value
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        //interpolate from th current camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);

        //force destination.z to be cam z to keep camera far enough away
        destination.z = camZ;

        //set te camera to the destination
        transform.position = destination;

        //set the orthopgraphicsize of the camera to keep groun in view
        Camera.main.orthographicSize = destination.y + 10;
    }
}
