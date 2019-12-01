using System;




public class IntegerObserver{


    private ObservableInteger var;
    private readonly Action<int> callback;


    public IntegerObserver(ObservableInteger var , Action<int> callBackFunc) {

        this.var = var;
        callback = callBackFunc;

        this.var.AddObserver(this);
       

    }

    public void stopObserving() {

        var.RemoveObserver(this);
    }

    public void OnVarChanged(int value) {
        callback(value);
    }


}
