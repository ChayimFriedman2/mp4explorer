//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mp4Explorer
{
    public static class BoxViewUtil
    {
        public static void AddField(Grid grid, string caption, string value)
        {
            grid.RowDefinitions.Add(new RowDefinition());
            var label = new Label
            {
                HorizontalContentAlignment = HorizontalAlignment.Right,
                Content = caption,
            };
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            var textBox = new TextBox
            {
                IsReadOnly = true,
                Text = value,
            };
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);
        }

        public static void AddField(Grid grid, string caption, uint value) => AddField(grid, caption, value.ToString());

        public static void AddField(Grid grid, string caption, int value) => AddField(grid, caption, value.ToString());

        public static void AddField(Grid grid, string caption, ulong value) => AddField(grid, caption, value.ToString());

        public static void AddField(Grid grid, string caption, bool value) => AddField(grid, caption, value.ToString());

        public static void AddField(Grid grid, string caption, byte[] value) =>
            AddField(grid, caption, $"{Convert.ToBase64String(value)}({string.Concat(value.Select(b => b.ToString("X2")))})");

        public static RowDefinition AddControl(Grid grid, Control control)
        {
            var row = new RowDefinition();
            grid.RowDefinitions.Add(row);
            Grid.SetColumn(control, 0);
            Grid.SetRow(control, grid.RowDefinitions.Count - 1);
            Grid.SetColumnSpan(control, 2);
            grid.Children.Add(control);
            return row;
        }

        public static void AddControl(Grid grid, Control control, Brush brush)
        {
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(control, 0);
            Grid.SetRow(control, grid.RowDefinitions.Count - 1);
            Grid.SetColumnSpan(control, 2);
            grid.Children.Add(control);
            control.Background = brush;
        }

        public static void AddHeader(Grid grid, string text)
        {
            AddControl(grid, new Label
            {
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Content = text,
            }, Brushes.LightBlue);
        }

        public static void AddSubHeader(Grid grid, string text)
        {
            AddControl(grid, new Label
            {
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Content = text,
            }, Brushes.LightGray);
        }
    }
}
