using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCollection : MonoBehaviour
{
    public static MaterialCollection Instance;
    private Material selectableMaterial;

    [field: SerializeField] public Material Blue { get; private set; }
    [field: SerializeField] public Material Green { get; private set; }
    [field: SerializeField] public Material Orange { get; private set; }
    [field: SerializeField] public Material Red { get; private set; }
    [field: SerializeField] public Material Violet { get; private set; }
    [field: SerializeField] public Material Water { get; private set; }
    [field: SerializeField] public Material White { get; private set; }
    [field: SerializeField] public Material Yellow { get; private set; }

    public void SelectBlueColor()
    {
        selectableMaterial = Blue;
    }
    public void SelectGreenColor()
    {
        selectableMaterial = Green;
    }
    public void SelectOrangeColor()
    {
        selectableMaterial = Orange;
    }
    public void SelectRedColor()
    {
        selectableMaterial = Red;
    }
    public void SelectVioletColor()
    {
        selectableMaterial = Violet;
    }
    public void SelectWaterColor()
    {
        selectableMaterial = Water;
    }
    public void SelectWhiteColor()
    {
        selectableMaterial = White;
    }
    public void SelectYellowColor()
    {
        selectableMaterial = Yellow;
    }

    public Material SetSelectableMaterial ()
    {
        if (selectableMaterial == null)
        {
            selectableMaterial = Blue;
        }
        return selectableMaterial;
    }
}
