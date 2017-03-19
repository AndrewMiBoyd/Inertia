// this file defines the ship, how it moves, its status, and reporting

using System;
using System.Collections.Generic;

namespace ShipControl
{

                                                                    // can probably be replaced with a Vector3
                                                                    // refactoring ould be required
public class TupleList<T1, T2, T3> : List<Tuple<T1, T2, T3>>
{
    public void Add( T1 item, T2 item2, T3 item3)
        {
                Add( new Tuple<T1, T2, T3>( item, item2, item3 ) );
                    }
}

                                                                    // this is a collection of orientations a ship can be in
static private TupleList<int, int, int> orientations = new TupleList<int, int, int>
{
     {0, 1, 0},
     {0, 0, 1},
     {-1, 0, 0},
     {0, -1, 0},
     {0, 0, -1},
     {1, 0, 0}
}         
        
       
                                                                    // rotation stores rotational inertia  as a single int
        private int rotation;
                                                                    // oritentation key holds the direction pointed as a single int
                                                                    // direction stores as a tuple, this might be useless
        private int orientationKey;
        private Tuple<int, int, int> direction; 

        private char manuever;


                                                                    // we will need a function like this to store a selected manuever until it is applied
        public void setManuever(char manueverCode)
        {
            this.manuever = manueverCode;
        }
                                                                    // changes the rotation value and orientation value; keeps it simple
        private void rotate(int amount)
        {
            this.orientationKey = (orientationKey + amount) % 6;
            while (orientationKey < 0)
                orientationKey += 6;
            this.direction = orientations[orientationKey];
        
        }

