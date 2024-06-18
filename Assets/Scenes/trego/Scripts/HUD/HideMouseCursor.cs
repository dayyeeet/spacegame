using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouseCursor : MonoBehaviour
{
    private void Update()
    {
#if UNITY_EDITOR
        Cursor.visible = false;
#endif
    }
}
