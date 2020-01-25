using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthText : MonoBehaviour {

	[SerializeField]
	Text text;

	[SerializeField]
	RectTransform healthBarFill;

	float curHealth;
	float maxHp;
    float screenEffectalpha;

    public RawImage screenEffect;

	private void Start() {
        maxHp = 100;
        curHealth = maxHp;
        text.text = string.Format("{0}", curHealth);

        
       
    }

	void Update () {
		if (curHealth < 0){
			curHealth = 0;
            Death();
        }

        

        ChangeAlpha(screenEffectalpha);

    }

	void SetHealthAmount(float _amount){
		healthBarFill.localScale = new Vector3(_amount, 1f, 1f);
	}

    public void TakeDamage(int damage) {
        screenEffectalpha = 1f;
        curHealth -= damage;
        SetHealthAmount((curHealth / maxHp));
        text.text = string.Format("{0}", curHealth);
    }

    void Death() {
        GameObject.Find("GameController").GetComponent<GameSettings>().RestartLevel();
    }

    void ChangeAlpha(float alp) {
        screenEffectalpha = Mathf.Lerp(screenEffectalpha, 0, 0.05f);

        Color curColor = screenEffect.color;
        curColor.a = alp;
        screenEffect.color = curColor;
    }

}

