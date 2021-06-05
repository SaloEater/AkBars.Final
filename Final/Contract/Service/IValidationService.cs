using Request.Dto;

namespace Contract.Service
{
    public interface IValidationService
    {
        public bool ValidateFuture(FutureRequest request);

        public bool ValidateTemperature(TemperatureRequest request);

        public bool ValidateWind(WindDirectionRequest request);
    }
}
