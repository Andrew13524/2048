using Assets.Scripts.GameObjects;
using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Coordinate Coordinate;
    public bool HasFill;

    private void Awake()
    {
        HasFill = false;
        if (gameObject.GetComponentsInChildren<Transform>().Length > 1) HasFill = true;
    }
    private void OnTransformChildrenChanged()
    {
        if (gameObject.GetComponentsInChildren<Transform>().Length > 1) HasFill = true;
        else HasFill = false;
    }
}
