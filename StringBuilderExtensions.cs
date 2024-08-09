using System.Text;
using UnityEngine;

public static class StringBuilderExtensions
{
    public static StringBuilder StartSize(this StringBuilder stringBuilder, float value)
    {
        return stringBuilder.Append($"<size={value}>");
    }
    public static StringBuilder EndSize(this StringBuilder stringBuilder)
    {
        return stringBuilder.Append($"</size>");
    }
    public static StringBuilder StartAlign(this StringBuilder stringBuilder, string alignment)
    {
        return stringBuilder.Append($"<align=\"{alignment}\">");
    }
    public static StringBuilder EndAlign(this StringBuilder stringBuilder)
    {
        return stringBuilder.Append($"</align>");
    }
    public static StringBuilder AlignCenter(this StringBuilder stringBuilder)
    {
        return stringBuilder.Align("center");
    }
    public static StringBuilder AlignLeft(this StringBuilder stringBuilder)
    {
        return stringBuilder.Align("left");
    }
    public static StringBuilder AlignRight(this StringBuilder stringBuilder)
    {
        return stringBuilder.Align("right");
    }
    public static StringBuilder AlignJustified(this StringBuilder stringBuilder)
    {
        return stringBuilder.Align("justified");
    }
    public static StringBuilder AlignFlushd(this StringBuilder stringBuilder)
    {
        return stringBuilder.Align("flush");
    }
    public static StringBuilder Align(this StringBuilder stringBuilder, string value)
    {
        return stringBuilder.Insert(0, $"<align=\"{value}\">").Append("</align>");
    }
    public static StringBuilder Bold(this StringBuilder stringBuilder)
    {
        return stringBuilder.Insert(0, $"<b>").Append("</b>");
    }
    public static StringBuilder AppendColor(this StringBuilder stringBuilder, string text, string color)
    {
        return stringBuilder.Append($"<color=#{color}>{text}</color>");
    }
    public static StringBuilder AppendColor(this StringBuilder stringBuilder, string text, Color color)
    {
        return stringBuilder.Append($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>");
    }
    public static StringBuilder StartStrikeThrough(this StringBuilder stringBuilder)
    {
        return stringBuilder.Append("<s>");
    }
    public static StringBuilder EndStrikeThrough(this StringBuilder stringBuilder)
    {
        return stringBuilder.Append("</s>");
    }
    public static StringBuilder StartUnderline(this StringBuilder stringBuilder)
    {
        return stringBuilder.Append("<u>");
    }
    public static StringBuilder EndUnderline(this StringBuilder stringBuilder)
    {
        return stringBuilder.Append("</u>");
    }
    public static StringBuilder AppendLines(this StringBuilder stringBuilder, int count, string text = "")
    {
        for (int i = 0; i < count; i++)
        {
            stringBuilder.AppendLine(text);
        }
        return stringBuilder;
    }
    public static StringBuilder AppendLinesColor(this StringBuilder stringBuilder, int count, string color, string text = "")
    {
        return AppendLines(stringBuilder, count, ColorText(text, color));
    }
    public static StringBuilder AppendLinesColor(this StringBuilder stringBuilder, int count, Color color, string text = "")
    {
        return AppendLines(stringBuilder, count, ColorText(text, color));
    }
    public static StringBuilder AppendLineColor(this StringBuilder stringBuilder, string text, string color)
    {
        return stringBuilder.AppendLine($"<color=#{color}>{text}</color>");
    }
    public static StringBuilder AppendLineColor(this StringBuilder stringBuilder, string text, Color color)
    {
        return stringBuilder.AppendLine($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>");
    }
    public static string ColorText(string text, string color)
    {
        var hashtagStr = color[0] == '#' ? "" : "#";
        return $"<color={hashtagStr}{color}>{text}</color>";
    }
    public static string ColorText(string text, Color color)
    {
        return ColorText(text, ColorUtility.ToHtmlStringRGBA(color));
    }
}