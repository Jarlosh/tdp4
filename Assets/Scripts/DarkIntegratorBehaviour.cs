using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TDPF
{
    public class DarkIntegratorBehaviour : MonoBehaviour
    {
        private static readonly int propColor = Shader.PropertyToID("_Color");

        private MeshRenderer renderer;
        private float alpha;
        private bool isFadingOut;

        private void Start()
        {
            renderer = GetComponent<MeshRenderer>();
            var color = Color.black;
            color.a = alpha;
            renderer.material.SetColor(propColor, color);
        }

        private void Update()
        {
            if (Time.time < 20)
                return;

            if (renderer.isVisible)
            {
                if (isFadingOut)
                {
                    if (!IsVisible())
                        isFadingOut = false;
                    alpha = Mathf.Lerp(alpha, 0f, 0.02f);
                }
                else
                {
                    if (IsVisible() && NeedToFadeOut())
                        isFadingOut = true;
                    else
                        alpha = 0f;
                }
            }
            else
            {
                isFadingOut = false;
                alpha = 0.5f;
            }

            var color = Color.black;
            color.a = alpha;
            renderer.material.SetColor(propColor, color);
        }

        private bool IsVisible() =>
            alpha > 0.001f;

        private bool NeedToFadeOut() =>
            Random.Range(0f, 1f) > 0.75;
    }
}
