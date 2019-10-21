using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCounter : MonoBehaviour {
    public TextMeshProUGUI text;
    public IntVar gemCount;
    public IntVar gemsCaught;
    // Start is called before the first frame update
    void Start() {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {


        text.SetText(gemsCaught.runtimeValue.ToString()+"/" + gemCount.runtimeValue.ToString());
    }
}
