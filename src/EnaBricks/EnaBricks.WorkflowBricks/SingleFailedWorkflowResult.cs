namespace EnaBricks.WorkflowBricks
{
    public sealed class SingleFailedWorkflowResult : MultipleFailedWorkflowResult
    {
        public SingleFailedWorkflowResult(string errorMessage) : base(new[] { errorMessage })
        {
        }
    }
}