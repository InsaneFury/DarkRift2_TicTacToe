using Scripts.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NameForm : MonoBehaviour
{
	[SerializeField]
	private TMPro.TMP_InputField InputField;
	private bool IsSubmiting = false;
	private bool LoadingScene = false;

    void Start()
    {
        if (InputField == null) {
			throw new System.Exception("missing input field");
		}
    }

	private void Update() {
		if (LoadingScene == false && NetworkingManager.Instance.GotMatch) {
			LoadingScene = true;
			SceneManager.LoadScene("Match");
		}
	}

	public void OnSubmitClick() {

		if (IsSubmiting) {
			return;
		}

		string name = InputField.text.Trim();

		if (name == string.Empty) {
			return;
		}

		if (NetworkingManager.Instance.ConnectionState == DarkRift.ConnectionState.Connected || NetworkingManager.Instance.Connect()) {
			IsSubmiting = true;
			NetworkingManager.Instance.MessageNameToServer(name);
		}

	}
}
