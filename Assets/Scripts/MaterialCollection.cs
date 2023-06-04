using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCollection : MonoBehaviour
{
    public static MaterialCollection Instance;
    private Material selectableMaterial;
    [SerializeField] UIManager uiManager;

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
        uiManager.CheckPrice();
        uiManager.BuySkin();
        selectableMaterial = Blue;
        uiManager.ExitMagazine();
    }
    public void SelectGreenColor()
    {
        uiManager.CheckPrice();
        uiManager.BuySkin();
        selectableMaterial = Green;
        uiManager.ExitMagazine();
    }
    public void SelectOrangeColor()
    {
        uiManager.CheckPrice();
        uiManager.BuySkin();
        selectableMaterial = Orange;
        uiManager.ExitMagazine();
    }
    public void SelectRedColor()
    {
        uiManager.CheckPrice();
        uiManager.BuySkin();
        selectableMaterial = Red;
        uiManager.ExitMagazine();
    }
    public void SelectVioletColor()
    {
        uiManager.CheckPrice();
        uiManager.BuySkin();
        selectableMaterial = Violet;
        uiManager.ExitMagazine();
    }
    public void SelectWaterColor()
    {
        uiManager.CheckPrice();
        uiManager.BuySkin();
        selectableMaterial = Water;
        uiManager.ExitMagazine();
    }
    public void SelectWhiteColor()
    {
        uiManager.CheckPrice();
        uiManager.BuySkin();
        selectableMaterial = White;
        uiManager.ExitMagazine();
    }
    public void SelectYellowColor()
    {
        uiManager.CheckPrice();
        uiManager.BuySkin();
        selectableMaterial = Yellow;
        uiManager.ExitMagazine();
    }

    public Material SetSelectableMaterial()
    {
        if (selectableMaterial == null)
        {
            selectableMaterial = Blue;
        }
        return selectableMaterial;
    }
}
