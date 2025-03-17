using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace I_am_Hero_WPF
{
    public static class LayoutManager
    {
        public static void SaveLayout(Canvas canvas)
        {
            var positions = new List<string>();
            var sizes = new List<string>();

            foreach (var block in canvas.Children.OfType<Grid>())
            {
                double left = Canvas.GetLeft(block);
                double top = Canvas.GetTop(block);
                double width = block.Width;
                double height = block.Height;

                positions.Add($"{block.Name},{left},{top}");
                sizes.Add($"{block.Name},{width},{height}");
            }

            Properties.Settings.Default.BlockPositions = string.Join(";", positions);
            Properties.Settings.Default.BlockSizes = string.Join(";", sizes);
            Properties.Settings.Default.Save();
        }

        public static void LoadLayout(Canvas canvas)
        {
            var positionData = Properties.Settings.Default.BlockPositions.Split(';');
            var sizeData = Properties.Settings.Default.BlockSizes.Split(';');

            foreach (var data in positionData)
            {
                var parts = data.Split(',');
                if (parts.Length != 3) continue;

                string name = parts[0];
                if (!double.TryParse(parts[1], out double left) || !double.TryParse(parts[2], out double top)) continue;

                var element = canvas.Children.OfType<Grid>().FirstOrDefault(x => x.Name == name);
                if (element != null)
                {
                    Canvas.SetLeft(element, left);
                    Canvas.SetTop(element, top);
                }
            }

            foreach (var data in sizeData)
            {
                var parts = data.Split(',');
                if (parts.Length != 3) continue;

                string name = parts[0];
                if (!double.TryParse(parts[1], out double width) || !double.TryParse(parts[2], out double height)) continue;

                var element = canvas.Children.OfType<Grid>().FirstOrDefault(x => x.Name == name);
                if (element != null)
                {
                    element.Width = width;
                    element.Height = height;
                }
            }
        }
    }
}