using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateStars : MonoBehaviour
{
    [SerializeField] private Image shootingStars;

    private void Update()
    {
        shootingStars.gameObject.transform.Rotate(new Vector3(0, 0, 1 * Time.deltaTime));
    }
}
