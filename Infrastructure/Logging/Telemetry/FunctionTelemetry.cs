using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging.Telemetry
{
    public class FunctionTelemetry : ITelemetryInitializer
    {
        private static readonly AsyncLocal<string> Operation = new AsyncLocal<string>();
        private static readonly AsyncLocal<string> Payload = new AsyncLocal<string>();
        private static readonly AsyncLocal<string> OperationStatus = new AsyncLocal<string>();
        private static readonly AsyncLocal<string> Message = new AsyncLocal<string>();
        private static readonly AsyncLocal<string> Result = new AsyncLocal<string>();
        private static readonly AsyncLocal<string> Timestamp = new AsyncLocal<string>();

        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry is ISupportProperties propertyItem)
            {
                propertyItem.Properties[nameof(TelemetryFields.Operation)] = Operation.Value ?? string.Empty;
                propertyItem.Properties[nameof(TelemetryFields.Payload)] = Payload.Value ?? string.Empty;
                propertyItem.Properties[nameof(TelemetryFields.OperationStatus)] = OperationStatus.Value ?? string.Empty;
                propertyItem.Properties[nameof(TelemetryFields.Message)] = Message.Value ?? string.Empty;
                propertyItem.Properties[nameof(TelemetryFields.Result)] = Result.Value ?? string.Empty;
                propertyItem.Properties[nameof(TelemetryFields.Timespan)] = DateTimeOffset.Now.ToString();
            }
        }

        public static void SetRequestTelemetry()
        {
            Operation.Value = "tes";
            Payload.Value = "tes 2";
            Timestamp.Value = DateTimeOffset.Now.ToString();
        }
    }
}
