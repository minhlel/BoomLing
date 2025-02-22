using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        Destroy(gameObject); // Huỷ GameObject khi animation kết thúc
    }
}
