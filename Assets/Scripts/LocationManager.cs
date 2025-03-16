using UnityEngine;

public class LocationManager : MonoBehaviour
{
    [SerializeField] GameObject oceanBackground;
    [SerializeField] GameObject oilSpillBackground;
    public bool atOilSpill { get; private set; } = false;

    void OnEnable()
    {
        Sail.useSail += ChangeLocation;
    }

    void OnDisable()
    {
        Sail.useSail -= ChangeLocation;
    }

    void ChangeLocation()
    {
        atOilSpill = !atOilSpill;

        oceanBackground.SetActive(!atOilSpill);
        oilSpillBackground.SetActive(atOilSpill);
    }
}
