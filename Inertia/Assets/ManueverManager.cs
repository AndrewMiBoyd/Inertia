using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
public class ManueverManager : MonoBehaviour {

    public void Start(){
    createOrderOfAction(1, 4);
    }
public string createOrderOfAction(int rotationalInertia, int acceleration)
{
    List<string> actionOrder = new List<string>();

    int absoluteRotation = Math.Abs(rotationalInertia);

    actionOrder.Add("");
    actionOrder.Add("");
    actionOrder.Add("");
    actionOrder.Add("");

    int i = actionOrder.Count/2;

    int turnsHalf = rotationalInertia/2;
    int accelerationHalf = acceleration/2;

    int leftOverRotation = rotationalInertia % 2;
    int leftOverAcceleration = acceleration % 2;

    while(turnsHalf > 1 || accelerationHalf > 1)
    {
        if (turnsHalf > accelerationHalf)
        {
            actionOrder.Insert(i+1, "T");
            actionOrder.Insert(i+1, "T");
            turnsHalf -= 2;
            i = actionOrder.Count/2;
        }
        else if (turnsHalf < accelerationHalf)
        {
            actionOrder.Insert(i+1, "A");
            actionOrder.Insert(i+1, "A");
            accelerationHalf -=2;
            i = actionOrder.Count/2;
        }
        else if (turnsHalf == accelerationHalf)
        {
            actionOrder.Insert (i+1, "A");
            actionOrder.Insert(i+1, "T");
            turnsHalf -=1;
            accelerationHalf --;
            i = actionOrder.Count/2;
        }
    }

    while (accelerationHalf + turnsHalf > 0)
    {
        if (accelerationHalf > 0){
        actionOrder.Insert(i+1,"A");
        accelerationHalf--;
            i = actionOrder.Count/2;
        }
        if (turnsHalf > 0)
        { actionOrder.Insert(i+1, "T");
        turnsHalf--;
            i = actionOrder.Count/2;}
    }
    List<string> temp = new List<string> (actionOrder);
    temp.Reverse();
    temp.InsertRange(temp.Count, actionOrder);
    actionOrder = temp;

    i = actionOrder.Count/2;
    while (leftOverAcceleration > 0)
    { actionOrder.Insert(i+1, "A");
        leftOverAcceleration--;
    }
    while (leftOverRotation > 0)
    { actionOrder.Insert(i+1, "T");
        leftOverRotation--;
    }

    Debug.Log(string.Join(string.Empty, actionOrder.ToArray()));

    return string.Join(string.Empty, actionOrder.ToArray());

}


}
