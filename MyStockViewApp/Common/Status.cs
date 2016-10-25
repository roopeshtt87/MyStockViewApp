using System;

namespace MyStockViewApp
{
    public class Status : IStatus
    {
        public Status()
        {
            SetSuccess();
        }
        public Status(string message, StatusSeverity severity, Exception ex)
        {
            StatusMessage = message;
            Severity = severity;
            if(ex != null)
                StatusDetails = ex.Message;
        }

        public Status(string message, Exception ex)
        {
            StatusMessage = message;
            Severity = StatusSeverity.Error;
            if (ex != null)
                StatusDetails = ex.Message;
        }

        String statusMessage = String.Empty;
        public string StatusMessage
        {
            get { return statusMessage; }
            set { statusMessage = value; }
        }

        String statusDetails = String.Empty;
        public string StatusDetails
        {
            get { return statusDetails; }
            set { statusDetails = value; }
        }

        public bool IsSuccessful
        {
            get { return (Severity == StatusSeverity.Success) ? true : false; }
        }

        public StatusSeverity Severity { get; set; }

        public void SetSuccess()
        {
            StatusMessage = String.Empty;
            StatusDetails = String.Empty;
            Severity = StatusSeverity.Success;
        }

        public void SetError(string message)
        {
            StatusMessage = message;
            StatusDetails = String.Empty;
            Severity = StatusSeverity.Error;
        }
    }
}
