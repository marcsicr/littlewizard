using System;




public class FloatObserver{


    private ObservableFloat var;
    private readonly Action<float> callback;


    public FloatObserver(ObservableFloat var , Action<float> callBackFunc) {

        this.var = var;
        callback = callBackFunc;

        this.var.AddObserver(this);
       

    }

    public void stopObserving() {

        var.RemoveObserver(this);
    }

    public void OnVarChanged(float value) {
        callback(value);
    }


}
