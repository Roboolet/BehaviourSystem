using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

[RequireComponent(typeof(Canvas))]
public class AgentDebugDisplay : MonoBehaviour
{
    [FormerlySerializedAs("text")] [SerializeField] private TMP_Text tmp;
    [SerializeField] private Agent agent;
    [SerializeField, Range(1,100)] private int batchMax;
    private Canvas canvas;
    private Camera cam;
    private Quaternion initialRotation;
    
    private string[] batch;
    private int batchID;
    private string lastAddition, firstAddition;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        cam = Camera.main;
        batch = new string[batchMax];

        initialRotation = transform.rotation;
    }

    private void Update()
    {
        // billboarding
        transform.rotation = cam.transform.rotation * initialRotation;

        // batching
        string nodeLog = agent.GetNodeLog();
        if (batchID >= batchMax - 1 || nodeLog == firstAddition)
        {
            // send batch
            string newText = "";
            for (int i = 0; i < batchMax; i++)
            {
                newText += batch[i];
            }

            tmp.text = newText;
            batchID = 0;
            batch = new string[batchMax];
            lastAddition = "";;
        }
        if (nodeLog != lastAddition)
        {
            batch[batchID] = "\n" + nodeLog;
            lastAddition = nodeLog;
            if (batchID == 0)
            {
                firstAddition = nodeLog;
            }
            
            batchID++;
        }
    }
}
