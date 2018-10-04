using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour {

    static private MissionDemolition S;

    [Header("Set in Inspector")]
    public Text ultLevel;
    public Text ultShots;
    public text ultButton;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Set Dynamically")]

}
