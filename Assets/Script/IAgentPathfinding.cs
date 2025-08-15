using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentPath
{
    public void FollowPath(List<Node> path,Action onArrive = null);
}
