using AngularWithCoreDemo.Helpers;
using AngularWithCoreDemo.Models;
using AngularWithCoreDemo.Options;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithCoreDemo
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorViewModel), Constants.Http400)]
    [ProducesResponseType(typeof(ErrorViewModel), Constants.Http500)]
    public class DemoController : ControllerBase
    {
        private const string InternalServerErrorDetail = "Something went wrong and it's not your fault. Please try again or contact the system administrator.";

        public OkObjectResult Success<T>(T response) where T : SuccessViewModel
        {
            if (string.IsNullOrEmpty(response.Status))
            {
                response.Status = Constants.Http200.ToString();
            }
            if (string.IsNullOrEmpty(response.Title))
            {
                response.Status = Constants.Http200Title;
            }

            return Ok(response);
        }

        protected IActionResult MethodExecutor<T>(Func<T> method) where T : SuccessViewModel
        {
            try
            {
                T response = method.Invoke();
                if (string.IsNullOrEmpty(response.Status))
                {
                    response.Status = Constants.Http200.ToString();
                }
                if (string.IsNullOrEmpty(response.Title))
                {
                    response.Status = Constants.Http200Title;
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                ErrorViewModel response = new ErrorViewModel
                {
                    Detail = ex.Message,
                    Status = Constants.Http500.ToString(),
                    Title = Constants.Http500Title
                };
                return new InternalServerErrorObjectResult(response);
            }
        }
    }
}
