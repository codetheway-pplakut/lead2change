using System;

namespace Lead2Change.Web.Ui.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorViewModel()
        {
            Message = "Unknown Error Encountered";
            Exception = null;
        }

        public ErrorViewModel(string message)
        {
            Message = message;
            Exception = null;
        }

        public ErrorViewModel(string message, Exception exception)
        {
            Message = message;
            Exception = exception;
        }

        public static implicit operator ErrorViewModel(string str) => new ErrorViewModel(str);
    }
}
