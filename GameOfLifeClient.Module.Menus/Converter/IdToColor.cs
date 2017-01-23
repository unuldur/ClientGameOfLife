using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace GameOfLifeClient.Module.Menus.Converter
{
    class IdToColor : IValueConverter
    {
        private Dictionary<int, SolidColorBrush> _idColor = new Dictionary<int, SolidColorBrush>();
        private Random _rand = new Random();
        private List<SolidColorBrush> colors = new List<SolidColorBrush>()
        {
            Brushes.Blue,
            Brushes.Red,
            Brushes.BlueViolet,
            Brushes.DeepSkyBlue
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int) value;
            if (_idColor.ContainsKey(id))
            {
                return _idColor[id];
            }
            SolidColorBrush color = colors[_rand.Next(colors.Count - 1)];
            colors.Remove(color);
            _idColor.Add(id,color);
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
