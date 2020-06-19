using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ARController : MonoBehaviour
{
    public Button toggleStartBtn;
    public TapToPlaceObj tapToPlaceObjScript;

    private bool _isPlaying = false;

    private void Start()
    {

        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    public void togglePlay()
    {
        if (_isPlaying)
        {
            tapToPlaceObjScript.StopPlayingAR();
            toggleStartBtn.GetComponentInChildren<Text>().text = "Start";
        }
        else
        {
            toggleStartBtn.GetComponentInChildren<Text>().text = "Stop Playing";
            tapToPlaceObjScript.StartPlayingAR();
        }
        _isPlaying = !_isPlaying;

    }
}
