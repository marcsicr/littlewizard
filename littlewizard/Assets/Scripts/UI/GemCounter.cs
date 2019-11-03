using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCounter : IntObserver {
    public TextMeshProUGUI text;
    public IntVar gemCount;
    public IntVar gemsCaught;
    
    // Start is called before the first frame update
    void Start() {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        UpdateCounter(base.var);
    }

    // Update is called once per frame
    void Update() {


      
    }

    public void UpdateCounter(ObservableInt count) {
        text.SetText(count.getRunTimeValue().ToString() + "/" + gemCount.runtimeValue.ToString());
    }

    public void resetCounter() {
        gemsCaught.reset();
        gemCount.reset();
    }
}
