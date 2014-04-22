namespace PX.Business.Services.CurlyBrackets.CurlyBracketResolver
{
    public interface ICurlyBracketResolver
    {
        string DefaultTemplate { get; }

        void Initialize();

        string Render(string[] parameters);
    }
}
