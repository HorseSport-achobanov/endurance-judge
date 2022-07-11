using EnduranceJudge.Domain.AggregateRoots.PoC.Events;
using EnduranceJudge.Domain.Annotations;
using EnduranceJudge.Domain.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using static EnduranceJudge.Domain.DomainConstants;

namespace EnduranceJudge.Domain.Core.Models;

public class NewDomainBase : DomainBase<NewDomainException>, INotifyPropertyChanged
{
    protected NewDomainBase()
    {
    }
    // Unused variable is needed mark the constructor which generates Id
    // That constructor should ONLY be used when creating NEW (no database entry) objects
    protected NewDomainBase(string generateIdFlag) : base(GENERATE_ID)
    {
        this.Id = DomainIdProvider.Generate();
    }
    
    private DomainValidationArguments validationArguments;
    public event PropertyChangedEventHandler PropertyChanged;
    public event DomainValidationHandler InvalidChange;

    protected void SetValue<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
    {
        if (!EqualityComparer<T>.Default.Equals(property, value))
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
    public virtual void RaiseInvalidChange(DomainValidationArguments arguments = null)
    {
        var args = arguments ?? this.validationArguments;
        if (args is null || !args.Any())
        {
            throw new Exception($"Unable to raise '{nameof(InvalidChange)}' event without arguments ");
        }
        this.InvalidChange?.Invoke(this, arguments ?? this.validationArguments);
    }
    public virtual void RaiseInvalidChange(string property, string message)
    {
        var arguments = new DomainValidationArguments { { property, message } };
        this.InvalidChange?.Invoke(this, arguments);
    }
    public virtual void RaiseInvalidChange(string message)
    {
        var arguments = new DomainValidationArguments { { GENERIC_VALIDATION_KEY, message } };
        this.InvalidChange?.Invoke(this, arguments);
    }
    public virtual void AddValidationArgument(string property, string message)
    {
        this.validationArguments ??= new DomainValidationArguments();
        this.validationArguments.Add(property, message);
    }
}
