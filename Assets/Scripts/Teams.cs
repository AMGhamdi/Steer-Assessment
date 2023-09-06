using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Teams",menuName =("Scriptable Objects /Team"))]
public class Teams : ScriptableObject
{
    public enum Team
    {
        Red,
        Blue
    }
    public Team teamName;
    public Material teamMaterial;
    public Team enemyTeam;
}
