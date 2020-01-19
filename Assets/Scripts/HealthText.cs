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

	private void Start() {
        maxHp = 100;//GameManager.Instance.LocalPlayer.PlayerHealth.whatIsMaxHp();
	}

	void Update () {

        curHealth = 100;// GameManager.Instance.LocalPlayer.PlayerHealth.HitPointRemaining;

		if (curHealth < 0)
			curHealth = 0;
		
		SetHealthAmount(curHealth/maxHp);
		text.text = string.Format("{0}", curHealth);
	}

	void SetHealthAmount(float _amount){
		healthBarFill.localScale = new Vector3(_amount, 1f, 1f);
	}

}

