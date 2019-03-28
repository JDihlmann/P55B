using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
	#region Methods
    void Start()
    {
		Destroy(gameObject);
    }
	#endregion
}
