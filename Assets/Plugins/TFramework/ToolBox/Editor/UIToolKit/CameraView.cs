using UnityEngine;
using UnityEngine.UIElements;

namespace TFramework.ToolBox.UIToolKit
{
    [UxmlElement]
    public partial class CameraView : VisualElement
    {
        private VisualElement _cameraView;
        private Texture2D _texture;

        public CameraView()
        {
            _cameraView = new VisualElement
            {
                style =
                {
                    flexShrink = 0,
                    backgroundColor = new StyleColor(new Color(0,0,0,0)),
                    backgroundSize = new BackgroundSize(BackgroundSizeType.Contain),
                    flexGrow = 1
                }
                
            };
            style.flexGrow = 1;
            CaptureMainCamera();
            Add(_cameraView);
        }

        public void ClearCapture()
        {
            _cameraView.style.backgroundImage = null;
        }
        public void CaptureMainCamera()
        {
            CaptureCamera(Camera.main);
        }
        public void CaptureCamera(Camera camera)
        {
            if(camera == null)
                return;
            if (camera.targetTexture == null)
            {
                _texture = new Texture2D(camera.pixelWidth, camera.pixelHeight,TextureFormat.RGB24,false);
                RenderTexture rt = new RenderTexture((int)_texture.width, (int)_texture.height, 10);//创建一个RenderTexture对象
                camera.targetTexture = rt;//临时设置相关相机的targetTexture为rt, 并手动渲染相关相机
                camera.Render();
                RenderTexture.active = rt;//激活这个rt, 并从中中读取像素。
                _texture.ReadPixels(new Rect(0,0,_texture.width,_texture.height), 0, 0);
                _texture.Apply();
                camera.targetTexture = null;
                RenderTexture.active = null; 
                Object.DestroyImmediate(rt);
                _cameraView.style.backgroundImage = new StyleBackground(_texture);
            }
            else
            {
                _texture = new Texture2D(camera.pixelWidth, camera.pixelHeight,TextureFormat.RGB24,false);
                RenderTexture.active = camera.targetTexture;//激活这个rt, 并从中中读取像素。
                _texture.ReadPixels(new Rect(0,0,_texture.width,_texture.height), 0, 0);
                _texture.Apply();
                RenderTexture.active = null; 
                _cameraView.style.backgroundImage = new StyleBackground(_texture);
            }
           
        }
    }
}
