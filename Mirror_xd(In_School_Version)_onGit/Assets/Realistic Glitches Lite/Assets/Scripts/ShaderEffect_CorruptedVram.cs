using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[ExecuteInEditMode]
public class ShaderEffect_CorruptedVram : MonoBehaviour
{
    Ch2VisualManager ch2visualManager;
    public float shift = 10;
    private Texture texture;
    private Material material;

    void Awake()
    {
        ch2visualManager = GameObject.Find("VisualManager").GetComponent<Ch2VisualManager>();
        // material = new Material(Shader.Find("Hidden/Distortion"));

        material = new Material(Resources.Load<Shader>("Shaders/Distortion"));
        texture = Resources.Load<Texture>("Checkerboard-big");
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_ValueX", shift);
        material.SetTexture("_Texture", texture);
        Graphics.Blit(source, destination, material);
    }

    bool plus = true;

    bool renderingFrame = true;
    public float nownumtoreach = 2f;

    public float speed = 0.05f;
    private void Update()
    {
        if (renderingFrame)
        {
            if (Mathf.Abs(nownumtoreach) < 1000)
            {
                if (plus)
                {
                    shift = Mathf.Lerp(shift, nownumtoreach, speed);
                    if (shift == nownumtoreach)
                    {
                        nownumtoreach = -nownumtoreach;
                        plus = false;
                        speed += 0.01f;
                    }
                }
                else
                {

                    shift = Mathf.Lerp(shift, nownumtoreach, speed);
                    if (shift == nownumtoreach)
                    {

                        nownumtoreach = (-nownumtoreach) * 1.2f;
                        plus = true;
                        speed += 0.01f;
                    }
                }
            }
            else
            {
                renderingFrame = false;
                openTransition();
            }
        }
    }

    public void openTransition()
    {
        ch2visualManager.openTransition();
    }
}
