using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private IAgentPath agent;
    
    public PlayerController(IAgentPath agent)
    {
        this.agent = agent;
    }


    public void Move(List<Node> path)
    {
        agent.FollowPath(path,onArrive: null);
    }
}
