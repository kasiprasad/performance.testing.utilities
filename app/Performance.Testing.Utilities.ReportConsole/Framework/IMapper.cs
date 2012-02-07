namespace Performance.Testing.Utilities.ReportConsole.Framework
{
    public interface IMapper<in TInput, out TOutput>
    {
        TOutput MapFrom(TInput input);
    }
}