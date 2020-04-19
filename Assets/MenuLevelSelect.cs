using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevelSelect : MonoBehaviour{


    public void ChangeLevel(int level){
        if (level == 1)
            SceneManager.LoadScene("ProtoLevel3", LoadSceneMode.Single);
        else if (level == 2){
            SceneManager.LoadScene("ProtoLevel2", LoadSceneMode.Single);
        }
    }
    
}
