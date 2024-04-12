using UnityEngine;

public static class LayerMaskCompare
{
    public static bool Contains(this LayerMask mask, int layer)
    {
        return (mask & (1 << layer)) != 0;
    }
}
