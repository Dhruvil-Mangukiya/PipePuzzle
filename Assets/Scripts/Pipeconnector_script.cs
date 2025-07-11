using UnityEngine;
using System.Collections.Generic;

public class PipeConnector : MonoBehaviour

{
    public List<GameObject> pipes;
    public float pipeLength; 
    void Start()
    {
        for (int i = 1; i < pipes.Count; i++) 
        {
            Transform previousPipe = pipes[i - 1].transform;

            Vector3 endPosition = previousPipe.position + (previousPipe.forward * pipeLength);

            pipes[i].transform.position = endPosition;
        }
    }
}

