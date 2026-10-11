using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System;
using Avalonia.Platform;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Demo.Converters
{
    public class ImagePathConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string fileName && !string.IsNullOrEmpty(fileName))
            {
                try
                {
                    // Путь к папке с изображениями
                    var uri = new Uri($"avares://Demo/Assets/Images/{fileName}");
                    return new Bitmap(AssetLoader.Open(uri));
                }
                catch
                {
                    // Если файл не найден, возвращаем изображение-заглушку
                    var uri = new Uri($"avares://Demo/Assets/Images/picture.png");
                    return new Bitmap(AssetLoader.Open(uri));
                }
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
