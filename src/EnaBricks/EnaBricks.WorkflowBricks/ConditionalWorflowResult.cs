namespace EnaBricks.WorkflowBricks
{
    public sealed class ConditionalWorflowResult : IWorkflowResult
    {
        private readonly bool _isSuccessful;

        public ConditionalWorflowResult(bool isSuccessful)
        {
            this._isSuccessful = isSuccessful;
        }

        public bool IsSuccessful() => _isSuccessful;
    }
}