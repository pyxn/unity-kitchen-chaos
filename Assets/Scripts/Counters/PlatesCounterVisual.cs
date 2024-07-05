// PlatesCounterVisual.cs
using System;
using System.Collections.Generic;
using UnityEngine;


public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList = new List<GameObject>();

    private void Start()
    {
        if (platesCounter == null)
        {
            Debug.LogError("PlatesCounterVisual.cs; PlatesCounter is not assigned.");
            return;
        }

        if (counterTopPoint == null)
        {
            Debug.LogError("PlatesCounterVisual.cs; CounterTopPoint is not assigned.");
            return;
        }

        if (plateVisualPrefab == null)
        {
            Debug.LogError("PlatesCounterVisual.cs; PlateVisualPrefab is not assigned.");
            return;
        }

        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        InstantiatePlateVisual();
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        if (plateVisualGameObjectList.Count > 0)
        {
            GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
            plateVisualGameObjectList.Remove(plateGameObject);
            Destroy(plateGameObject);
        }
        else
        {
            Debug.LogWarning("PlatesCounterVisual.cs; No plates to remove.");
        }
    }

    private void InstantiatePlateVisual()
    {
        if (plateVisualPrefab == null)
        {
            Debug.LogError("PlatesCounterVisual.cs; Cannot instantiate plate visual because plateVisualPrefab is not assigned.");
            return;
        }

        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}