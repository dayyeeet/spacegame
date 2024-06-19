using System;
using System.Collections;
using UnityEngine;

public class SpaceshipIndicatorUI : MonoBehaviour
{
    [SerializeField] private GameObject spaceshipGameObject;
    private Outline _spaceshipOutline;

    private void Start()
    {
        _spaceshipOutline = spaceshipGameObject.GetComponent<Outline>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && !_spaceshipOutline.enabled)
        {
            StartCoroutine(ShowSpaceship());
        }
    }

    private IEnumerator ShowSpaceship()
    {
        _spaceshipOutline.enabled = true;
        yield return new WaitForSeconds(3);
        _spaceshipOutline.enabled = false;
    }
}