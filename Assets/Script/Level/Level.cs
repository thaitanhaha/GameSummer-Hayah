using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="New Level", menuName ="Scriptable Objects - Level")]

public class Level : ScriptableObject
{
    	public int level_th;
    	public string levelName;
    	public string levelDes;
    	public Color nameColor;
    	public Sprite levelImage;
    	public Object sceneToLoad;

}
