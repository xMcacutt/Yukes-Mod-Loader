using System.Windows;
using System.Windows.Media;

namespace Yukes_Mod_Loader_GUI.GUI;

public class Colors : ResourceDictionary
{
    public static Color DarkBackColor = Color.FromArgb(0xDD, 0x1d, 0x1f, 0x20);
    public static Color DarkBackSolidColor = Color.FromArgb(0xFF, 0x1d, 0x1f, 0x20);
    public static Color DarkerBackColor = Color.FromArgb(0xFF, 0x14, 0x15, 0x16);
    public static Color LightBackColor = Color.FromArgb(0xFF, 0x30, 0x39, 0x3a);
    public static Color LighterBackColor = Color.FromArgb(0xDD, 0x47, 0x50, 0x55);
    public static Color LightTextColor = System.Windows.Media.Colors.White;
    public static Color MidTextColor = Color.FromArgb(0xFF, 0xcb, 0xcb, 0xcb);
    public static Color DarkTextColor = System.Windows.Media.Colors.Black;
    public static Color AccentColor = Color.FromArgb(0xFF, 0xE2, 0xB2, 0xD4);
    public static Color AccentHoverColor = Color.FromArgb(0xFF, 0x9A, 0x60, 0x89);
    
    public Colors()
    {
        Add("DarkBack", DarkBackColor);
        Add("DarkBackSolid", DarkBackSolidColor);
        Add("DarkerBack", DarkerBackColor);
        Add("LightBack", LightBackColor);
        Add("LigherBack", LighterBackColor);
        Add("LightText", LightTextColor);
        Add("MidText", MidTextColor);
        Add("DarkText", DarkTextColor);
        Add("Accent", AccentColor);
        Add("AccentHover", AccentHoverColor);
    }
    
    private void Add(string key, object value)
    {
        this[key] = new SolidColorBrush((Color)value);
        this[key + "Color"] = value;
    }
}