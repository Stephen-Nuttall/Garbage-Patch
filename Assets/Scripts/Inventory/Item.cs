using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [SerializeField] Sprite image;
    [SerializeField] int maxStackSize = 99;

    public Sprite GetImage() { return image; }
    public int GetMaxStackSize() { return maxStackSize; }
}
