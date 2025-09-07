using UnityEngine;

public class SystemRandomRNG : MonoBehaviour, IRNG
{
    public float NextFloat()
    {
        return Random.value;
    }
}
