using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOTHController : GameController 
{

	
	
	protected override bool VictoryCondition () 
    {
		return false;
	}

    protected override void GameLogic() 
    {
        
    }

	protected override string VictoryText () 
    {
		return "I like grapes";
	}
}
