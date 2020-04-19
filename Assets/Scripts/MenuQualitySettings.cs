using UnityEngine;



public class MenuQualitySettings : MonoBehaviour{


    public void PressLow(){
        QualitySettings.SetQualityLevel(1,true);
    }

    public void PressHigh(){
        QualitySettings.SetQualityLevel(0,true);
    }

}