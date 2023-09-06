using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;
    private void Awake()
    {
        instance = this;
    }
    public List<GameObject> blueTeam;
    public List<GameObject> redTeam;
}
