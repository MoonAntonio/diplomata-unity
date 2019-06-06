using UnityEditor;
using UnityEngine;

namespace LavaLeak.Diplomata.Editor.Helpers
{
    public class ColorHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Color ColorAdd(Color color, float r, float g, float b, float a = 0)
        {
            var newColor = new Color(0, 0, 0);
            newColor = color;
            newColor.r += r;
            newColor.g += g;
            newColor.b += b;
            newColor.a += a;
            return newColor;
        }

        public static Color ColorSub(Color color, float r, float g, float b, float a = 0)
        {
            var newColor = color;
            newColor.r -= r;
            newColor.g -= g;
            newColor.b -= b;
            newColor.a -= a;
            return newColor;
        }

        public static Color ColorMul(Color color, float r, float g, float b, float a = 1)
        {
            var newColor = color;
            newColor.r *= r;
            newColor.g *= g;
            newColor.b *= b;
            newColor.a *= a;
            return newColor;
        }

        public static Color ColorDiv(Color color, float r, float g, float b, float a = 1)
        {
            var newColor = color;
            newColor.r /= r;
            newColor.g /= g;
            newColor.b /= b;
            newColor.a /= a;
            return newColor;
        }

        public static Color ColorAdd(Color colorA, Color colorB) =>
            ColorAdd(colorA, colorB.r, colorB.g, colorB.b, colorB.a);

        public static Color ColorSub(Color colorA, Color colorB) =>
            ColorSub(colorA, colorB.r, colorB.g, colorB.b, colorB.a);

        public static Color ColorMul(Color colorA, Color colorB) =>
            ColorMul(colorA, colorB.r, colorB.g, colorB.b, colorB.a);

        public static Color ColorDiv(Color colorA, Color colorB) =>
            ColorDiv(colorA, colorB.r, colorB.g, colorB.b, colorB.a);

        public static Color ColorAdd(Color color, float value, float alpha = 1.0f) =>
            ColorAdd(color, value, value, value, alpha);

        public static Color ColorSub(Color color, float value, float alpha = 1.0f) =>
            ColorSub(color, value, value, value, alpha);

        public static Color ColorMul(Color color, float value, float alpha = 1.0f) =>
            ColorMul(color, value, value, value, alpha);

        public static Color ColorDiv(Color color, float value, float alpha = 1.0f) =>
            ColorDiv(color, value, value, value, alpha);

        public static Color ResetColor()
        {
            if (EditorGUIUtility.isProSkin)
            {
                return GUIHelper.proBGColor;
            }

            else
            {
                return GUIHelper.BGColor;
            }
        }

        public static Color PlaymodeTint()
        {
            try
            {
                if (Application.isPlaying)
                {
                    var playmodeTintArray = EditorPrefs.GetString("Playmode tint").Split(';');
                    return new Color(float.Parse(playmodeTintArray[1]), float.Parse(playmodeTintArray[2]),
                        float.Parse(playmodeTintArray[3]), float.Parse(playmodeTintArray[4]));
                }
            }

            catch
            {
                Debug.Log("Cannot get playmode tint.");
            }

            return Color.gray;
        }
    }
}