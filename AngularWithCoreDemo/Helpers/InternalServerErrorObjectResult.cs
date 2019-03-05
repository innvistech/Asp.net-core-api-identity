using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AngularWithCoreDemo.Helpers
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Hiri.WebApi.CustomResults.InternalServerErrorObjectResult" /> class.
        /// </summary>
        /// <param name="value">The content to format into the entity body.</param>
        public InternalServerErrorObjectResult(object value)
            : base(value)
        {
            base.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
