using UnityEngine;
using System.Collections;

public class TextAppearAfter : MonoBehaviour {

    public float appearAfter;

	// Use this for initialization
	void Start () {
        Invoke("Show", appearAfter);
        Hide();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Hide()
    {
        GetComponent<UnityEngine.UI.Text>().color = new Color(GetComponent<UnityEngine.UI.Text>().color.r, GetComponent<UnityEngine.UI.Text>().color.g, GetComponent<UnityEngine.UI.Text>().color.b, 0f);
    }

    void Show()
    {
        GetComponent<UnityEngine.UI.Text>().color = new Color(GetComponent<UnityEngine.UI.Text>().color.r, GetComponent<UnityEngine.UI.Text>().color.g, GetComponent<UnityEngine.UI.Text>().color.b, 1f);
    }
}
