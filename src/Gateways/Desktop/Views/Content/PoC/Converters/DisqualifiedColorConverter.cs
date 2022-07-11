using EnduranceJudge.Domain.State.Participants;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.PoC.Converters;

public class ParticipantToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return default;
        }
        if (value is not NewParticipant participant)
        {
            var message = $"Cannot convert '{value.GetType()}' to Brush object. Expected '{nameof(NewParticipant)}'";
            throw new Exception(message);
        }
        var color = participant.LapRecords.Any(x => x.Result?.IsNotQualified ?? false)
            ? Colors.Red
            : Colors.Black;

        return new SolidColorBrush(color);
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
