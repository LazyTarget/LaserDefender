using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public float maxHealth = 100;
	public float curHealth = 100;
	public bool showText = true;
	public bool showProgress = true;
	public Vector2 fixedSize;
	public Slider slider;

	private Vector2 _unitSize;


	// Use this for initialization
	void Start () {
		var spriteRenderer = GetComponent<SpriteRenderer>();
		_unitSize = new Vector2(spriteRenderer.sprite.rect.width, spriteRenderer.sprite.rect.height);

		slider.maxValue = maxHealth;
		slider.transform.parent = transform;
		SetCurrent (curHealth);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {
		var percentage = curHealth / maxHealth;

		// Text
		string content = "";
		if (showText) {
			content = curHealth + "/" + maxHealth;
		}

		// Size
		float healthBarLength;
		if (fixedSize != null && fixedSize.x > 0) {
			healthBarLength = fixedSize.x;
		} else {
			//healthBarLength = (Screen.width / 6) * (curHealth / maxHealth);
			healthBarLength = _unitSize.x * percentage;
		}

		float healthBarHeight;
		if (fixedSize != null && fixedSize.y > 0) {
			healthBarHeight = fixedSize.y;
		} else {
			healthBarHeight = 18;
		}

		// Offset to unit
		var offset = new Vector2(healthBarLength / 2, _unitSize.y / 2);
		Vector2 targetPos = Camera.main.WorldToScreenPoint(transform.position);
		targetPos -= offset;
		targetPos = new Vector2(targetPos.x, Screen.height - targetPos.y);
		var targetRect = new Rect(targetPos.x, targetPos.y, healthBarLength, healthBarHeight);

		// Draw
		//GUI.Box(targetRect, content);
		//GUI.HorizontalSlider(targetRect, percentage, 0, maxHealth);
		//GUI.Slider(targetRect, percentage, maxHealth, 0, percentage, null, null, true, 13);

		slider.transform.position = targetPos;
		
		if (showProgress) {
			var length = healthBarLength * percentage;
			//GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, length, healthBarHeight), percentage.ToString ());
			//GUI.color = Color.red;
		}
	}

	public void SetCurrent(float health) {
		curHealth = health;
		slider.value = curHealth;
		
		if (curHealth < 0)
			curHealth = 0;
		if (curHealth > maxHealth)
			curHealth = maxHealth;
		if (maxHealth < 1)
			maxHealth = 1;
	}
}
