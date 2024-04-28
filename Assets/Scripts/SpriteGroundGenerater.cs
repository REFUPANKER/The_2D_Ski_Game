using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Video;

public class SpriteGroundGenerater : MonoBehaviour
{
    private SpriteShapeController ssc;
    public int Width = 20;
    public float VerticalDistance = 2f;
    public float HorizontalDistance = 5f;
    public float PointCurve = 1f;
    public float GroundFillDistance = 10f;

    [Header("Checkpoint System")]
    public GameObject checkpointInstance;
    public Transform CheckpointsHolder;
    public UI_Text_DistanceCalculators UiController;
    public float perDistance = 10f;

    [Header("Finishline alignment")]
    public Transform finishLine;
    public void ReStart(bool recheckpoint = false)
    {

        ssc = this.GetComponent<SpriteShapeController>();
        ssc.spline.Clear();
        foreach (Transform item in CheckpointsHolder)
        {
            Destroy(item.gameObject);
        }

        UiController.CheckPoints.Clear();

        ssc.spline.InsertPointAt(0, Vector3.zero);
        ssc.spline.InsertPointAt(1, new Vector3(HorizontalDistance, 0, 0));
        AddCheckpoint(HorizontalDistance / 2, 0);

        float l = HorizontalDistance * 2;
        float minY = ssc.spline.GetPosition(0).y;
        for (int i = 2; i <= Width; i++)
        {
            float preY = ssc.spline.GetPosition(i - 1).y;
            float t = Random.Range(preY - VerticalDistance, preY + VerticalDistance);
            minY = (t < minY) ? t : minY;
            ssc.spline.InsertPointAt(i, new Vector2(l, t));
            l += HorizontalDistance;
            if (i % perDistance == 0)
            {
                AddCheckpoint(ssc.spline.GetPosition(i).x, ssc.spline.GetPosition(i).y);
            }
        }
        // turn back to first point with fill
        Vector2 firstpoint = ssc.spline.GetPosition(0);
        Vector2 lastpoint = ssc.spline.GetPosition(ssc.spline.GetPointCount() - 1);
        ssc.spline.InsertPointAt(ssc.spline.GetPointCount(), new Vector2(lastpoint.x, minY - GroundFillDistance));
        ssc.spline.InsertPointAt(0, new Vector2(firstpoint.x, minY - GroundFillDistance));
        ssc.spline.InsertPointAt(ssc.spline.GetPointCount() - 1, new Vector2(finishLine.position.x - transform.position.x, finishLine.position.y - transform.position.y));
        ssc.spline.InsertPointAt(ssc.spline.GetPointCount() - 1, new Vector2(finishLine.position.x - transform.position.x - HorizontalDistance * 2, finishLine.position.y - transform.position.y));

        for (int i = 0; i < ssc.spline.GetPointCount(); i++)
        {
            ssc.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            ssc.spline.SetLeftTangent(i, new Vector3(-PointCurve, 0, 0));
            ssc.spline.SetRightTangent(i, new Vector3(PointCurve, 0, 0));
        }
        
        if (recheckpoint)
        {
            UiController.ReplaceCheckpoints();
        }
    }
    void Start()
    {
        ReStart();
    }

    private void AddCheckpoint(float x, float y)
    {
        GameObject newCheckpoint = Instantiate(checkpointInstance);
        newCheckpoint.SetActive(true);
        newCheckpoint.transform.parent = CheckpointsHolder;
        newCheckpoint.transform.position = new Vector2(x + this.transform.position.x, y + this.transform.position.y);
        CheckpointController cpCtrl = newCheckpoint.GetComponent<CheckpointController>();
        UiController.CheckPoints.Add(cpCtrl);
    }

}
