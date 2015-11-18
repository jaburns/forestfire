using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Conversation : MonoBehaviour {
    public string[] watergirlPart;
    public string[] fireguyPart;

    public Text watergirlText;
    public Text fireguyText;
	// Use this for initialization
	void Start () {
        int randomConversation = (int)Random.Range(0, watergirlPart.Length - 0.00001f);
        watergirlText.text = watergirlPart[randomConversation];
        fireguyText.text = fireguyPart[randomConversation];
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

public struct ConversationParts
{
    public string WaterGirl;
    public string FireGuy;
}