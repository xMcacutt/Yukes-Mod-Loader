﻿using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Yukes_Mod_Loader_GUI.Controls;

public class Icon : Control
{
    static Icon()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Icon), new FrameworkPropertyMetadata(typeof(Icon)));
    }

    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(double), typeof(Icon), new PropertyMetadata(12.0));

    public static readonly DependencyProperty CodeProperty =
        DependencyProperty.Register(nameof(Code), typeof(string), typeof(Icon), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty ForegroundProperty =
        TextElement.ForegroundProperty.AddOwner(typeof(Icon));
    
    public double Size
    {
        get => (double)GetValue(SizeProperty);
        set { SetValue(SizeProperty, value); }
    }

    public string Code
    {
        get => (string)GetValue(CodeProperty);
        set { SetValue(CodeProperty, value); }
    }

    public Brush Foreground
    {
        get => (Brush)GetValue(ForegroundProperty);
        set { SetValue(ForegroundProperty, value); }
    }
}