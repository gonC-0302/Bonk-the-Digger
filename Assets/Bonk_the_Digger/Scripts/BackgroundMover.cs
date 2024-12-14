using UnityEngine;

public class BackGroundMover : MonoBehaviour
{
    [SerializeField]
    private Vector2 offsetSpeed;
    [SerializeField]
    private Material material;
    private const float MAX_LENGTH = 1f;
    private const string PROPERTY_NAME = "_MainTex";

    private void Update()
    {
        if (material)
        {
            var x = Mathf.Repeat(Time.time * offsetSpeed.x, MAX_LENGTH);
            var y = Mathf.Repeat(Time.time * offsetSpeed.y, MAX_LENGTH);
            var offset = new Vector2(x, y);
            material.SetTextureOffset(PROPERTY_NAME, offset);
        }
    }

    private void OnDestroy()
    {
        if (material)
        {
            material.SetTextureOffset(PROPERTY_NAME, Vector2.zero);
        }
    }
}