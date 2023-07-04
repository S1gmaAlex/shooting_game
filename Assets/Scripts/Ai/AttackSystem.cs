using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private AttackIndicator indicatorPrefabs = null;
    [SerializeField] private RectTransform holder = null;
    [SerializeField] private new Camera camera = null;
    [SerializeField] private Transform player = null;

    private Dictionary<Transform, AttackIndicator> Indicator = new Dictionary<Transform, AttackIndicator>();

    #region Delegates
    public static Action<Transform> CreateIndicator = delegate { };
    public static Func<Transform, bool> CheckIfObjectInSight = null;
    #endregion

    private void OnEnable()
    {
        CreateIndicator += Create;
        CheckIfObjectInSight += InSight;
    }

    private void OnDisable()
    {
        CreateIndicator += Create;
        CheckIfObjectInSight += InSight;
    }

    void Create (Transform target)
    {
        if (Indicator.ContainsKey(target))
        {
            Indicator[target].Restart();
            return;
        }
        AttackIndicator newIndicator = Instantiate(indicatorPrefabs, holder);
        newIndicator.Register(target, player, new Action(() => { Indicator.Remove(target); }));
        Indicator.Add(target, newIndicator);
    }

    bool InSight (Transform t)
    {
        Vector3 screenPoint = camera.WorldToViewportPoint(t.position);
    
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

}
