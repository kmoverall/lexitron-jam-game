using UnityEngine;
using System.Collections;

public class ScrollingBG : MonoBehaviour {

	BeatManager beatz;

	public float scrollSpeed;
	private Vector2 savedOffset;

	// Use this for initialization
	void Start () {
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
		savedOffset = renderer.sharedMaterial.GetTextureOffset ("_MainTex");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (x, savedOffset.y);
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);

		if (beatz.beat && beatz.beatTracker % 8 == 0) {
			gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
		} else if (beatz.beat && beatz.beatTracker % 8 == 1){
			gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
		}
	}

	void OnDisable () {
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}
