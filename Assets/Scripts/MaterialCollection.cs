using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCollection : MonoBehaviour
{
    public static MaterialCollection Instance;

    [field: SerializeField] public Material Violet { get; private set; }
    [field: SerializeField] public Material Green { get; private set; }
    [field: SerializeField] public Material Blue { get; private set; }
    [field: SerializeField] public Material Yellow { get; private set; }
    [field: SerializeField] public Material Orange { get; private set; }
}
