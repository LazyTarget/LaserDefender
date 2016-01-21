using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public float maxHealth = 100;
	public float curHealth = 100;
	public bool showText = true;
	public Vector2 fixedSize;
	public Slider slider;

	private Vector2 _unitSize;
	private Slider _sliderInstance;

	// Use this for initialization
	void Start () {
		var spriteRenderer = GetComponent<SpriteRenderer>();
		_unitSize = new Vector2(spriteRenderer.sprite.rect.width, spriteRenderer.sprite.rect.height);

		_sliderInstance = (Slider) Instantiate(slider, new Vector2(0, 0), Quaternion.identity);
		var canvas = GameObject.Find("Canvas");
		_sliderInstance.transform.SetParent(canvas.transform, false);
		_sliderInstance.maxValue = maxHealth;

		SetCurrent (curHealth);
	}

	//void OnGUI() {
	void Update() {
		if (_sliderInstance == null) {
			return;
		}

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
		//var offset = new Vector3(healthBarLength / 2, _unitSize.y / 2);
		var offset = new Vector3(0, _unitSize.y / 3);

		Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		targetPos = Camera.main.WorldToScreenPoint(targetPos);
		targetPos -= offset;
		var targetRect = new Rect(targetPos.x, targetPos.y, healthBarLength, healthBarHeight);
		_sliderInstance.transform.position = targetPos;
	}

	public void SetCurrent(float health) {
		curHealth = health;
		if (curHealth < 0)
			curHealth = 0;
		if (curHealth > maxHealth)
			curHealth = maxHealth;
		if (maxHealth < 1)
			maxHealth = 1;
		
		//slider.value = curHealth;
		_sliderInstance.value = curHealth;
	}

	void OnDestroy() {
		Debug.Log("Calling destroy on healthbar");
		if (_sliderInstance != null) {
			Destroy(_sliderInstance.gameObject);
		}
	}
}
