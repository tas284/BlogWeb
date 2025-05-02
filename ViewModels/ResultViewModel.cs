namespace BlogWeb.ViewModels
{
    public class ResultViewModel<TResult>
    {
        public ResultViewModel(TResult data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public ResultViewModel(TResult data)
        {
            Data = data;
        }

        public ResultViewModel(List<string> errors)
        {
            Errors = errors;
        }

        public ResultViewModel(string error)
        {
            Errors.Add(error);
        }

        public TResult Data { get; set; }
        public List<string> Errors { get; private set; } = [];
    }
}
