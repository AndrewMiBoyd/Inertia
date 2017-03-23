using UnityEngine;
using System.Collections.Generic;
public class applyManuever: MonoBehaviour {
    public int T=0;
    public int A=0;

    void Start()
    {
    createOrderOfAction(T, A);
    }
public string createOrderOfAction(int rotationalInertia, int acceleration)
{
    List<string> actionOrder = new List<string>(0);

    actionOrder.Add("");
    actionOrder.Add("");
    actionOrder.Add("");
    actionOrder.Add("");
    
    int i = actionOrder.Count/2;

    int turnsHalf = rotationalInertia/2;
    int accelerationHalf = acceleration/2;

    int leftOverRotation = rotationalInertia % 2;
    int leftOverAcceleration = acceleration % 2;

    //int commandsInList = 0;

    while(turnsHalf > 1 && accelerationHalf > 1)
    {
        Debug.Log("Hey, made it into the loo00p");
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
            actionOrder.Insert(i+1, "T");
            actionOrder.Insert (i+1, "A");
            turnsHalf -=1;
            accelerationHalf --;
        }
    }
    
    while (accelerationHalf + turnsHalf > 0)
    { 
    
        if  (turnsHalf > 0)
        { 
            actionOrder.Insert(i, "T");
            turnsHalf--;
        }
        if (accelerationHalf>0)
        {
            actionOrder.Insert(i, "A");
            accelerationHalf--;
        }
    }
    actionOrder.Reverse();
    List <string> tempList = new List<string>(actionOrder);
    tempList.Reverse();
    actionOrder.AddRange( tempList); 
    i = actionOrder.Count/2;
    while (leftOverRotation > 0)
    { actionOrder.Insert(i+1, "T");
        leftOverRotation--;
    }
    while (leftOverAcceleration > 0)
    { actionOrder.Insert(i+1, "A");
      leftOverAcceleration--;
    }
    string actions = string.Join(string.Empty, actionOrder.ToArray());
    Debug.Log(actions);
    return actions;//returnValue;
}

}
