namespace EnaBricks.WorkflowBricks
{
    public abstract class BaseSuccessfulResult : IWorkflowResult
    {
        public bool IsSuccessful() => true;
    }
}