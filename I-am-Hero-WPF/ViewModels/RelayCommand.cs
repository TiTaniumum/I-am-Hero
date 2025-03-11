using System;

internal class RelayCommand<T> : RelayCommand
{
    private Action<object> value;

    public RelayCommand(Action<object> value) : base(value, null)
    {
        this.value = value;
    }
}
