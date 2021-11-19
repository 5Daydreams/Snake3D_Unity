using UnityEngine;

namespace _Code._Shaders.ShaderControllers
{
    [RequireComponent(typeof(Camera))]
    [ExecuteAlways] public class DepthOfFieldTransparency : MonoBehaviour
    {
        [SerializeField] private Material _transparencyMaterial;

        private void Start(){
            Camera cam = GetComponent<Camera>();
            cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.Depth;
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source,destination,_transparencyMaterial);
        }
    }
}
