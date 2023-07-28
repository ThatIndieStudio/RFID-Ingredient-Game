using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RfidManager : MonoBehaviour
{
    #region Singleton
    private static RfidManager _instance;
    public static RfidManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("object manager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
            GameObject.Destroy(this.gameObject);
        else
            _instance = this;
    }
    #endregion
    
    //when a message arrives
    void OnMessageArrived(string msg)
    {
        Debug.Log(msg);
        //check if is ingredient
        if(DataManager.instance.CheckIfIsIngredient(msg)){
            
            Debug.Log("is an ingredient");
            DataManager.instance.placedIngredient(msg);
        } else {
            Debug.Log("not an ingredient");
        }
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}
