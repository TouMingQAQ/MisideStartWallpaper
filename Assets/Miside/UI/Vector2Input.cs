using System.Globalization;
using TMPro;
using UnityEngine;

public class Vector2Input : MonoBehaviour
{
    public Vector2 Value
    {
        get
        {
            var x = xText.text;
            var y = yText.text;
            Vector2 value = new();
            float.TryParse(x, out value.x);
            float.TryParse(y, out value.y);
            return value;
        }
        set
        {
            xText.SetTextWithoutNotify(value.x.ToString(CultureInfo.InvariantCulture));
            yText.SetTextWithoutNotify(value.y.ToString(CultureInfo.InvariantCulture));
        }
    }

    [SerializeField]
    private TMP_InputField xText;
    [SerializeField]
    private TMP_InputField yText;
}
