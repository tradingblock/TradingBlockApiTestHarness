namespace TradingBlockApiTestHarness.DTO
{
    public class ResponseContainer<T> : GenericResponse
    {
        /// <summary>
        /// Response object
        /// </summary>
        public T Payload { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - Payload:{1}", base.ToString(), Payload);
        }
    }
}
