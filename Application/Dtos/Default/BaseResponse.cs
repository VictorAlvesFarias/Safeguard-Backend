using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Dtos.Default
{
    public class BaseResponse<T>
    {
        public BaseResponse(bool success = false) => Success = success;
        public T? Data { get; set; }
        public void AddError(string error) => Errors.Add(error);
        public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>(); 
        public ActionResult<BaseResponse<T?>> Result(Controller controller)
        {
            if (Success)
            {
                return controller.Ok(this);
            }

            else if (this.Errors.Count > 0)
            {
                return controller.BadRequest(this);
            }

            return controller.StatusCode(StatusCodes.Status500InternalServerError);
        }
        public ActionResult<DefaultResponse> DefaultResult(Controller controller)
        {
            if (Success)
            {
                return controller.Ok(this);
            }

            else if (this.Errors.Count > 0)
            {
                return controller.BadRequest(this);
            }

            return controller.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
