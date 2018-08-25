namespace EnaBricks.WorkflowBricks
{
    using Newtonsoft.Json;
    using System;
    using System.Linq;

    public class MultipleFailedWorkflowResult : IWorkflowResult
    {
        [JsonProperty(PropertyName = "ErrorMessages")]
        private readonly string[] _errorMessages;

        public MultipleFailedWorkflowResult(string[] errorMessages)
        {
            _errorMessages = errorMessages ?? throw new ArgumentNullException(nameof(errorMessages));
            if (!_errorMessages.All(e => !string.IsNullOrWhiteSpace(e)))
            {
                throw new ArgumentException("Contains null or empty value", nameof(errorMessages));
            }
        }

        public bool IsSuccessful() => false;
    }
}