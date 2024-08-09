using UnityEngine;

public static class StringExtensions
{
    public static string LimitLength(this string str, int maxLength)
    {
        if (str.Length > maxLength)
        {
            str = str.Substring(0, maxLength);
        }
        return str;
    }
    public static string WrapColor(this string str, Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{str}</color>";
    }
    public static string WrapColor(this string str, string colorStr)
    {
        return $"<color={(colorStr[0] == '#' ? "" : "#")}{colorStr}>{str}</color>";
    }
}
