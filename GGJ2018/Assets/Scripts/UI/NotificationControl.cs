using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationControl : MonoBehaviour {
	public RectTransform NotificationContainer;
	public Notification NotificationPrefab;

	public static NotificationControl SceneInstance;

	void Awake() {
		if (SceneInstance == null)
			SceneInstance = this;
		else
			Destroy (this);
	}

	public void PostNotification(string message) {
		PostNotification (message, Color.white);
	}

	public void PostNotification(string message, Color color) {
		var notification = Instantiate<Notification>(NotificationPrefab, NotificationContainer);
		notification.DisplayText.text = message;
		notification.DisplayText.color = color;
	}
}
