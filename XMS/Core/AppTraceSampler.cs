using OpenTelemetry.Trace;

namespace XMS.Core
{
    public class AppTraceSampler : Sampler
    {
        public override SamplingResult ShouldSample(in SamplingParameters samplingParameters)
        {
            var name = samplingParameters.Name;

            if (name.Contains("SignalR") ||
                name.Contains("Components.HandleEvent"))
            {
                return new SamplingResult(SamplingDecision.Drop);
            }

            return new SamplingResult(SamplingDecision.RecordAndSample);
        }
    }
}
