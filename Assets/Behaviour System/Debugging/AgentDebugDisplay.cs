using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

[RequireComponent(typeof(Canvas))]
public class AgentDebugDisplay : MonoBehaviour
{
    [FormerlySerializedAs("text")] [SerializeField] private TMP_Text tmp;
    [SerializeField] private Agent agent;
    private Canvas canvas;
    private Camera cam;
    private Quaternion initialRotation;
    
    private List<string> batch;
    private string lastNodeLog;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        cam = Camera.main;
        batch = new List<string>();

        initialRotation = transform.rotation;
    }

    private void Update()
    {
        // billboarding
        transform.rotation = cam.transform.rotation * initialRotation;

        // batching
        string nodeLog = agent.GetNodeLog();
        
        if (!batch.Contains(nodeLog))
        {
            batch.Add(nodeLog);
            lastNodeLog = nodeLog;
        }
        else if(nodeLog != lastNodeLog)
        {
            string newText = "";
            for (int i = 0; i < batch.Count; i++)
            {
                newText += "\n" + batch[i];
            }

            tmp.text = newText;
            batch.Clear();
        }
    }
}
