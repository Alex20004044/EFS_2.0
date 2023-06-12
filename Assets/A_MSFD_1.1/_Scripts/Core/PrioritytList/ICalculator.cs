namespace MSFD
{
    public interface ICalculator<T>
    {
        T Calculate(T sourceValue);
    }
}