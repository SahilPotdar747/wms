namespace Kemar.UrgeTruck.Domain.ResponseModel
{
    public  class BlackListedDriverAndVehicleResponse
    {
        public bool IsVehicleBlackListed { get; set; }
        public bool IsDriverBlackListed { get; set; }
        public string ResponseMsg { get; set; }
    }
}
