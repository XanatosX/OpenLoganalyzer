using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OpenLoganalyzer.Core.Helper
{
    public class FontSize
    {
        private readonly Size size;

        public int Width => (int)size.Width;
        public int Height => (int)size.Height;

        public FontSize(TextBox box, string content)
        {
            FormattedText formattedText = new FormattedText(
                content,
                CultureInfo.CurrentCulture,
                box.FlowDirection,
                new Typeface(box.FontFamily, box.FontStyle, box.FontWeight, box.FontStretch),
                box.FontSize,
                Brushes.Black,
                new NumberSubstitution(),
                TextFormattingMode.Display
                );

            size = new Size(formattedText.Width, formattedText.Height);
        }
    }
}
