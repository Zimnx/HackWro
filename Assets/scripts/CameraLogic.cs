using UnityEngine;
using System.Collections;

public class CameraLogic : MonoBehaviour {
    static public string debug = "";
    Texture progressBackground;
    Texture progressForeground;
    GameObject cannon;
    GUIStyle style;
	// Use this for initialization
	void Start () {
        progressBackground = (Texture2D)Resources.Load("progress_background");
        progressForeground = (Texture2D)Resources.Load("progress_foreground");
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnGUI()
    {
        style = new GUIStyle();
        style.normal.textColor = Color.black;
        //GUILayout.Label("Debug: " + debug,style);
        if (CannonLogic.firePowerSetuping)
        {
            DrawProgressBar();
        }
    }
    void DrawProgressBar()
    {
        float dist = 10f;
        float firePower = CannonLogic.firePower;
        if(cannon == null)
         cannon = GameObject.Find("cannon");
        if(progressBackground == null)
            progressBackground = (Texture2D)Resources.Load("progress_background");
        if(progressForeground == null)
            progressForeground = (Texture2D)Resources.Load("progress_foreground");
        Vector3 cannonPos = cannon.transform.position;
        var imageWidth = progressBackground.width / 5;
        var imageHeight = progressBackground.height / 5;
        var pLeft = cannonPos.x + dist;
        var pTop = imageHeight;
        var pRight = imageWidth;
        var pBottom = imageHeight;
        //GUILayout.Label("firePower: " + firePower, style);
        
        var rect = new Rect(pLeft + dist, pTop,pRight,pBottom);
        GUI.DrawTexture(rect, progressBackground);
        GUI.DrawTexture(new Rect(pLeft + dist, pTop, pRight,((firePower - CannonLogic.firePowerMin) * 100)/((CannonLogic.firePowerMax - CannonLogic.firePowerMin))), progressForeground);
        //Debug.Log(progressForeground == null);
    }

}
