using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialData
{
    public int tutNpcIndex;
    public int loreIndex;
    public bool newWaypoint;
    public bool tutComplete;

    public TutorialData(int tutNpcIndex, int loreIndex, bool newWaypoint, bool tutComplete)
    { 
        this.tutNpcIndex = tutNpcIndex;
        this.loreIndex = loreIndex;
        this.newWaypoint = newWaypoint;
        this.tutComplete = tutComplete;
    }

   
}


