using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Scripts.Networking;

public class NameForm : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nameInput;
    private bool IsSubmiting = false;
    void Start()
    {
        if (!nameInput)
            throw new System.Exception("Missing name input");
    }

    public void OnSubmitClick()
    {
        if (IsSubmiting) return;
        string name = nameInput.text.Trim();

        if (name == string.Empty) return;

        if (NetworkingManager.Instance.ConnectionState == DarkRift.ConnectionState.Connected || NetworkingManager.Instance.Connect())
        {
            IsSubmiting = true;
            NetworkingManager.Instance.MessageNameToServer(name);
        }
    }
}
