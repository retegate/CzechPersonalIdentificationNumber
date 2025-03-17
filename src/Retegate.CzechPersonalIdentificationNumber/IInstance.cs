namespace Retegate.CzechPersonalIdentificationNumber;

public interface IInstance<TImplementation>
{
    static abstract TImplementation DefaultInstance { get; }
}