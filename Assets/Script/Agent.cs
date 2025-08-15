using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour,IAgentPath
{
    [SerializeField] private float moveSpeed = 10F;

    private float cellSize;
    private Coroutine followPathCoroutine;

    public void SetCellSize(float cellSize) => this.cellSize = cellSize;

    public void FollowPath(List<Node> path, Action onArrive = null)
    {
        if (followPathCoroutine != null)
        {
            StopCoroutine(followPathCoroutine);
            followPathCoroutine = null;
        }

        followPathCoroutine = StartCoroutine(IEFollowPath(path, onArrive));
    }
    
    private IEnumerator IEFollowPath(List<Node> path, Action onArrive = null)
    {
        foreach (var node in path)
        {
            Vector3 targetPos = new Vector3(node.X * cellSize, 0, node.Y * cellSize);
            while (Vector3.Distance(transform.position, targetPos) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        onArrive?.Invoke();
    }
    
}
