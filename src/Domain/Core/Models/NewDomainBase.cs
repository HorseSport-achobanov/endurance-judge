using EnduranceJudge.Domain.Annotations;
using EnduranceJudge.Domain.Core.Exceptions;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EnduranceJudge.Domain.Core.Models;

public class NewDomainBase : DomainBase<NewDomainException>, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void SetValue<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
    {
        if (!property.Equals(value))
        {
            property = value;
            this.RaisePropertyChanged(propertyName);
        }
    }
    
    [NotifyPropertyChangedInvocator]
    protected virtual void RaisePropertyChanged(string propertyName)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
