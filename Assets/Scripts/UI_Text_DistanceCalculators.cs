using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text_DistanceCalculators : MonoBehaviour
{
    public GroundChecker gc;
    public TextMeshProUGUI label;

    public Transform player;
    public Transform StartPoint;
    public Transform FinishPoint;
    public Slider UiFinishDistanceSlider;

    float finishDistance = 0f;
    float currentDistance = 0f;


    public RectTransform UiDistanceSlider;
    public Transform UiCheckpointHolder;
    public Transform UiCheckpointInstance;
    public List<CheckpointController> CheckPoints;
    void Start()
    {
        finishDistance = Vector3.Distance(StartPoint.position, FinishPoint.position);
        ReplaceCheckpoints();
    }
    public void ReplaceCheckpoints()
    {
        foreach (Transform item in UiCheckpointHolder)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in CheckPoints)
        {
            float InWorldPercent = 100 * Vector2.Distance(item.transform.position, StartPoint.position) / finishDistance;
            Transform NewUiCheckPoint = Instantiate(UiCheckpointInstance, UiCheckpointHolder);
            float inUiPointX = UiDistanceSlider.rect.width * (InWorldPercent / 100);
            NewUiCheckPoint.localPosition = new Vector2(inUiPointX, UiCheckpointInstance.position.y);
        }
    }
    void LateUpdate()
    {
        if (!gc.Grounded && gc.distanceToGround != Mathf.Infinity)
        {
            label.text = "Ground\n" + (int)gc.distanceToGround + "m";
        }
        else
        {
            label.text = "";
        }
        currentDistance = Vector3.Distance(player.position, FinishPoint.position);
        UiFinishDistanceSlider.value = UiFinishDistanceSlider.maxValue - (100 * currentDistance / finishDistance / 100);
    }
}
