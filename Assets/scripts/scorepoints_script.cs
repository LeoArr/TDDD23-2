using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scorepoints_script : MonoBehaviour {
	public int scorepoints;
	private float timeLeft;
	Text m_text;
	public Image content;
	public float targetAmount = 0.0f;
	player_movement pm;
	// Use this for initialization
	void Start () {
		scorepoints = 0;
		timeLeft = 180.0f;
		pm = GameObject.FindWithTag("Player").GetComponent<player_movement>();
		m_text = this.gameObject.transform.GetChild(0).GetComponent<Text>();
		content.fillAmount = 0.4f;
	}

	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		int temp = (int)timeLeft;
		int sec = temp % 60;
		int min = temp / 60;
		if (sec <= 9) {
			m_text.text = "time: " + min + ":0" + sec +"   score: " + scorepoints;
		} else {
			m_text.text = "time: " + min + ":" + sec +"   score: " + scorepoints;
		}

		if (timeLeft < 0.0f) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		//this sure is messy but it works nice
		if (!pm.onThePill)
			targetAmount -= 0.001f;
		if (targetAmount > 1.0f) {
			targetAmount = 1.0f;
		} else if (targetAmount < 0.0f) {
			targetAmount = 0.0f;
		}
		if (!pm.onThePill && targetAmount <= content.fillAmount) {
				content.fillAmount -= 0.001f;
		} else if (targetAmount > content.fillAmount) {
			content.fillAmount += 0.05f;
			if (targetAmount < content.fillAmount)
				content.fillAmount = targetAmount;
		}

		if (content.fillAmount > 1.0f) {
			content.fillAmount = 1.0f;
		} else if (content.fillAmount < 0.0f) {
			content.fillAmount = 0.0f;
		}

		if (!pm.onThePill) {
			float factor = 1.0f - content.fillAmount;
			content.color = new Color(factor, 1.0f, content.fillAmount, 1.0f);
		} else {
			content.color = new Color(0.5f, 1.0f, 1.0f, 1.0f);
		}
	}

	public void increaseCombo(float amount) {
		targetAmount += amount;
		//truncate
		targetAmount -= amount % 0.001f;
	}
	public float getCombo() { return content.fillAmount; }
}
