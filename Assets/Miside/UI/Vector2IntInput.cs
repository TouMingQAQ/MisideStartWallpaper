using System.Globalization;
using TMPro;
using UnityEngine;

public class Vector2IntInput : MonoBehaviour
{
    public Vector2Int Value
    {
        get
        {
            var x = xText.text;
            var y = yText.text;
            Vector2Int value = new();
            int.TryParse(x, out var xValue);
            int.TryParse(y, out var yValue);
            value.x = xValue;
            value.y = yValue;
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
