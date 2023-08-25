/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;


/**
 * When creating your message listeners you need to implement these two methods:
 *  - OnMessageArrived
 *  - OnConnectionEvent
 */
public class SampleMessageListener : MonoBehaviour
{
    public Transform canvasTrf;
    public GameObject testPrefabObj;
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        Debug.Log(msg);
        string test = msg.Trim();
        Debug.Log(test);
        if(test=="dog"){
            GameObject testObj = Instantiate(testPrefabObj, canvasTrf);
            testObj.transform.localPosition = Vector3.zero;
        } else {
            Debug.Log("not dog");
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
