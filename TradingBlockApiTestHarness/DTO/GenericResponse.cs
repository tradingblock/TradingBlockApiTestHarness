namespace TradingBlockApiTestHarness.DTO
{
    public class GenericResponse
    {
        /// <summary>
        /// Code to specify whether the response was successful or a specific code to describe why the request failed
        /// </summary>
        public int ResponseCode { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - ResponseCode:{1}", base.ToString(), ResponseCode);
        }
    }
}
