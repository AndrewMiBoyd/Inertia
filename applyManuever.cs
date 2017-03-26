

public String createOrderOfAction(int rotationalInertia, int acceleration)
{
    List<String> actionOrder = new List<String>();

    int i = actionOrder.Count/2;

    int turnsHalf = rotationalInertia/2;
    int accelerationHalf = accerlertaion/2;

    int leftOverRotation = rotationalInertia % 2;
    int leftOverAcceleration = acceleration % 2;

    int commandsInList = 0;

    while(turnsHalf > 1 && accelerationHalf > 1)
    {
        if (turnsHalf > accelerationHalf)
        {
            actionOrder.insert(i+1, "T");
            actionOrder.insert(i+1, "T");
            turnsHalf -= 2;
            i = actionOrder.Count/2;
        }
        else if (turnsHalf < accelerationHalf)
        {
            actionOrder.insert(i+1, "A");
            actionOrder.insert(i+1, "A");
            accelerationHalf -=2;
            i = actionOrder.Count/2;
        }
        else if (turnsHalf == accelerationHalf)
        {
            actionOrder.insert (i+1, "A");
            actionOrder.insert(i+1, "T");
            turnsHalf -=1;
            accelerationHalf --;
        }
    }

    while (accelerationHalf > 0)
    { actionOrder.insert(i+1, "A");
        accelerationHalf--;
    }
    while (turnsHalf > 0)
    { actionOrder.insert(i+1, "T");
        TurnsHalf--;
    }
    actionOrder.Concat(actionOrder;)
    while (accelerationHalf > 0)
    { actionOrder.insert(i+1, "A");
        accelerationHalf--;
    }
    while (turnsHalf > 0)
    { actionOrder.insert(i+1, "T");
        turnsHalf--;
    }

}
