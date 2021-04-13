using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private Vector3 orig;
    private Vector3 dir;
    private Vector3 a;
    private Vector3 b;
    private Vector3 c;

    private float t;
    private float b1;
    private float b2;

    public MeshRenderer quadMat;
    public Material redMat;
    public Material defaultMat;

    public Transform startPos;
    public Transform endPos;
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    bool rayTriangleIntersect(Vector3 orig, Vector3 dir,
        Vector3 a, Vector3 b, Vector3 c, float t, float b1, float b2)
    {
        bool isIn = false;
        Vector3 E1 = b - a;
        Vector3 E2 = c - a;
        Vector3 S = orig - a;
        Vector3 S1 = Vector3.Cross(dir, E2);
        Vector3 S2 = Vector3.Cross(S, E1);

        // 共同系数
        float coeff = 1.0f / Vector3.Dot(S1, E1);
        t = coeff * Vector3.Dot(S2, E2);
        b1 = coeff * Vector3.Dot(S1, S);
        b2 = coeff * Vector3.Dot(S2, dir);

        Debug.Log($"t = {t}, b1 = {b1}, b2 = {b2}");

        if (t >= 0 && b1 >= 0 && b2 >= 0 && (1 - b1 - b2) >= 0)
        {
            isIn = true;
        }

        return isIn;
    }

    void Update()
    {
        orig = startPos.position;
        Vector3 offset = (endPos.position - startPos.position).normalized;

        dir = offset;

        a = pointA.position;
        b = pointB.position;
        c = pointC.position;

        if (rayTriangleIntersect(orig, dir, a, b, c, t, b1, b2))
        {
            quadMat.sharedMaterial = redMat;
        }
        else
        {
            quadMat.sharedMaterial = defaultMat;
        }
    }
}