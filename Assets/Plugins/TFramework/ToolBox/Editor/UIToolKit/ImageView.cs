using UnityEngine;
using UnityEngine.UIElements;

namespace TFramework.ToolBox.UIToolKit
{
    [UxmlElement]
    public partial class ImageView : VisualElement
    {
        [UxmlAttribute]
        public Texture Texture
        {
            get => _image.image;
            set
            {
                _image.image = value;
            }
        }

        private Image _image;
        public ImageView()
        {
            _image = new Image();
            Add(_image);
        }
    }
}

