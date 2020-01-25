using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour{
    // Start is called before the first frame update
    TimeManager timeManager;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;

        if (Input.GetKey(KeyCode.Return))
            RestartLevel();

        if (Input.GetKeyDown(KeyCode.F))
            timeManager.SlowTime();
        

    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
