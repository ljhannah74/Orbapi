using System.Net;

namespace orbapi.Models
{
    public class ApiResponse<T>
    {
        public string status { get; set; }      // success | failure | redirect
        public string message { get; set; }
        public DateTime execution_time_utc { get; set; }
        public List<string> warnings { get; set; }
        public int status_code { get; set; }
        public T data { get; set; }
        public Dictionary<string, object> test_data { get; set; }

        public ApiResponse()
        {
            this.warnings = new List<string>();
            this.test_data = new Dictionary<string, object>();
            this.execution_time_utc = DateTime.UtcNow;
        }

        public ApiResponse(string status, string message)
        {
            this.status = status;
            this.message = message;
            this.warnings = new List<string>();
            this.execution_time_utc = DateTime.UtcNow;
        }
    }



    #region standardized responses

    /// <summary>
    /// Used for Getting standardized Http Responses and Messages.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ApiResponseUtil<T>
    {

        public static ApiResponse<T> GetApiResponse(HttpStatusCode statusCode, string warnings = "")
        {

            #region 2xx Success
            if ((int)statusCode >= 200 && (int)statusCode < 300)
            {
                if (statusCode == HttpStatusCode.OK) return GetOkStatusApiResponse(warnings);
                if (statusCode == HttpStatusCode.Created) return GetCreatedStatusApiResponse(warnings);
                if (statusCode == HttpStatusCode.Accepted) return GetAcceptedStatusApiResponse(warnings);
                if (statusCode == HttpStatusCode.NonAuthoritativeInformation) return GetNonAuthoritativeInformationApiResponse(warnings);
                if (statusCode == HttpStatusCode.NoContent) return GetNoContentApiResponse(warnings);
                if (statusCode == HttpStatusCode.ResetContent) return GetResetContentApiResponse(warnings);
                if (statusCode == HttpStatusCode.PartialContent) return GetPartialContentApiResponse(warnings);            // PartialContent
            }
            #endregion 2xx Success

            #region 3xx Redirection
            if ((int)statusCode >= 300 && (int)statusCode < 400)
            {
                if (statusCode == HttpStatusCode.Ambiguous) return GetAmbiguousApiResponse(warnings);
                if (statusCode == HttpStatusCode.MultipleChoices) return GetAmbiguousApiResponse(warnings);
                if (statusCode == HttpStatusCode.Moved) return GetMovedPermantentlyApiResponse(warnings);
                if (statusCode == HttpStatusCode.MovedPermanently) return GetMovedPermantentlyApiResponse(warnings);
                if (statusCode == HttpStatusCode.Found) return GetFoundApiResponse(warnings);
                if (statusCode == HttpStatusCode.Redirect) return GetRedirectApiResponse(warnings);
                if (statusCode == HttpStatusCode.RedirectMethod) return GetRedirectMethodApiResponse(warnings);
                if (statusCode == HttpStatusCode.SeeOther) return GetSeeOtherApiResponse(warnings);
                if (statusCode == HttpStatusCode.NotModified) return GetNotModifiedApiResponse(warnings);
                if (statusCode == HttpStatusCode.TemporaryRedirect || statusCode == HttpStatusCode.RedirectKeepVerb) return GetTemporaryRedirectApiResponse(warnings);
                if (statusCode == HttpStatusCode.Ambiguous) return GetAmbiguousApiResponse(warnings);
            }
            #endregion 3xx Redirection


            #region 4xx Client Error
            if ((int)statusCode >= 300 && (int)statusCode < 500)
            {
                if (statusCode == HttpStatusCode.BadRequest) return GetBadRequestApiResponse(warnings);
                if (statusCode == HttpStatusCode.Unauthorized) return GetUnauthorizedApiResponse(warnings);
                if (statusCode == HttpStatusCode.PaymentRequired) return GetPaymentRequiredApiResponse(warnings);
                if (statusCode == HttpStatusCode.Forbidden) return GetForbiddenApiResponse(warnings);
                if (statusCode == HttpStatusCode.NotFound) return GetNotFoundApiResponse(warnings);
                if (statusCode == HttpStatusCode.MethodNotAllowed) return GetMethodNotAllowedApiResponse(warnings);
                if (statusCode == HttpStatusCode.NotAcceptable) return GetNotAcceptableApiResponse(warnings);
                if (statusCode == HttpStatusCode.RequestTimeout) return GetRequestTimeoutApiResponse(warnings);
                if (statusCode == HttpStatusCode.Conflict) return GetConflictApiResponse(warnings);
                if (statusCode == HttpStatusCode.Gone) return GetGoneApiResponse(warnings);
                if (statusCode == HttpStatusCode.LengthRequired) return GetLengthRequiredApiResponse(warnings);
                if (statusCode == HttpStatusCode.PreconditionFailed) return GetPreconditionFailedApiResponse(warnings);
                if (statusCode == HttpStatusCode.RequestEntityTooLarge)
                    return GetRequestEntityTooLargeApiResponse(warnings);
                if (statusCode == HttpStatusCode.RequestUriTooLong) return GetRequestUriTooLongApiResponse(warnings);
                if (statusCode == HttpStatusCode.UnsupportedMediaType)
                    return GetUnsupportedMediaTypeApiResponse(warnings);
                if (statusCode == HttpStatusCode.RequestedRangeNotSatisfiable)
                    return GetRequestedRangeNotSatisfiableApiResponse(warnings);
                if (statusCode == HttpStatusCode.ExpectationFailed) return GetExpectationFailedApiResponse(warnings);
            }
            #endregion 4xx Client Error


            #region 5xx Server Error
            if ((int)statusCode >= 500 && (int)statusCode < 600)
            {
                if (statusCode == HttpStatusCode.InternalServerError) return GetInternalServerErrorApiResponse(warnings);
                if (statusCode == HttpStatusCode.NotImplemented) return GetNotImplementedApiResponse(warnings);
                if (statusCode == HttpStatusCode.BadGateway) return GetBadRequestApiResponse(warnings);
                if (statusCode == HttpStatusCode.ServiceUnavailable) return GetServiceUnavailableApiResponse(warnings);
                if (statusCode == HttpStatusCode.GatewayTimeout) return GetGatewayTimeoutApiResponse(warnings);
                if (statusCode == HttpStatusCode.HttpVersionNotSupported) return GetHttpVersionNotSupportedApiResponse(warnings);
            }
            #endregion 5xx Server Error

            ApiResponse<T> apiResponse = new ApiResponse<T>();
            apiResponse.status_code = 599;
            apiResponse.status = "Other HTTP Response";

            return apiResponse;
        }




















        #region 2xx Api Response methods


        /// <summary>
        /// 200 - Standard response for HTTP Requests.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetOkStatusApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 200;
            response.status = "success";
            response.message = "OK";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 201 - The request has been fulfilled, resulting in the creation of a new resource.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetCreatedStatusApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 201;
            response.status = "success";
            response.message = "Created";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 202 - The request has been accepted for processing.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetAcceptedStatusApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 202;
            response.status = "success";
            response.message = "Accepted";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 203 - Get Non authoratives api response.
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private static ApiResponse<T> GetNonAuthoritativeInformationApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 203;
            response.status = "success";
            response.message = "Non Authoritative Information";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 204 - The server successfully processed the request and is not returning the content.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetNoContentApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 204;
            response.status = "success";
            response.message = "No Content";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 205 - Reset Content
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private static ApiResponse<T> GetResetContentApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 205;
            response.status = "success";
            response.message = "Reset Content";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 205 - Partial content
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private static ApiResponse<T> GetPartialContentApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 205;
            response.status = "success";
            response.message = "Reset Content";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        #endregion 2xx Api Response methods




        #region 3xx Api Response methods

        /// <summary>
        /// 300 - Ambiguous
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private static ApiResponse<T> GetAmbiguousApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 300;
            response.status = "redirect";
            response.message = "Ambiguous";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 301 - This and all future requests should be directed to the given
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetMovedPermantentlyApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 301;
            response.status = "redirect";
            response.message = "Moved Permanently";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 302 - Not Found
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private static ApiResponse<T> GetFoundApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 302;
            response.status = "redirect";
            response.message = "Found";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 302 - Redirect 
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private static ApiResponse<T> GetRedirectApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 302;
            response.status = "redirect";
            response.message = "Redirect";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 302 - Redirect Method
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private static ApiResponse<T> GetRedirectMethodApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 302;
            response.status = "redirect";
            response.message = "Redirect Method";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 303 - See Other
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private static ApiResponse<T> GetSeeOtherApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 303;
            response.status = "redirect";
            response.message = "See Other";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 304 - Not Modified
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private static ApiResponse<T> GetNotModifiedApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 304;
            response.status = "redirect";
            response.message = "Not Modified";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 307 - Temporary Redirect
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private static ApiResponse<T> GetTemporaryRedirectApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 307;
            response.status = "redirect";
            response.message = "Temporary Redirect";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 308 - Redirect Permanently
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private static ApiResponse<T> GetPermanentRedirectApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 308;
            response.status = "redirect";
            response.message = "Permanent Redirect";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }
        #endregion 3xx Api Response methods



        #region 4xx Api Response methods

        /// <summary>
        /// 400 - The server cannot or will not process the request due to an apparent client error (e.g., malformed request 
        /// syntax, invalid request message framing, or deceptive request routing).
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetBadRequestApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 400;
            response.status = "failure";
            response.message = "Bad Request";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 401 - Authentication is required. Shown when the user did not provide the proper credentials.
        /// 
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetUnauthorizedApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 401;
            response.status = "failure";
            response.message = "Unauthorized";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 402 - Payment Required.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetPaymentRequiredApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 402;
            response.status = "failure";
            response.message = "Payment required";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 403 - The requested Resource is forbidden.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetForbiddenApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 403;
            response.status = "failure";
            response.message = "Forbidden";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 404 - The requested resource could not be found.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetNotFoundApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 404;
            response.status = "failure";
            response.message = "Not Found";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 405 - The method is not allowed, the URL is correct, but the method is not setup properly.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetMethodNotAllowedApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 405;
            response.status = "failure";
            response.message = "Method Not Allowed";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 406 - Not acceptable.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetNotAcceptableApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 406;
            response.status = "failure";
            response.message = "Not Acceptable";
            response.warnings.Add("The requested resource no longer exists.");
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 408 - Request Time out
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetRequestTimeoutApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 408;
            response.status = "failure";
            response.message = "Request Timeout";
            response.warnings.Add("The The request has timed out.");
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// Indicates that the resource requested is no longer available and will not be available again.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetConflictApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 409;
            response.status = "failure";
            response.message = "Conflict";
            response.warnings.Add("A conflict exists.");
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 410 - Indicates that the resource requested is no longer available and will not be available again.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetGoneApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 410;
            response.status = "failure";
            response.message = "Gone";
            response.warnings.Add("The requested resource no longer exists.");
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 411 - Length Required
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetLengthRequiredApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 411;
            response.status = "failure";
            response.message = "Length Required";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 412 - Precondition Failed.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetPreconditionFailedApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 412;
            response.status = "failure";
            response.message = "Precondition Failed";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// Too many requests api response.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetRequestEntityTooLargeApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 413;
            response.status = "failure";
            response.message = "Request Entity Too Large";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// Too many requests api response.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetRequestUriTooLongApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 414;
            response.status = "failure";
            response.message = "Request Uri Too Long";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// Too many requests api response.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetUnsupportedMediaTypeApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 415;
            response.status = "failure";
            response.message = "Unsupported Media Type";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// Too many requests api response.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetRequestedRangeNotSatisfiableApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 416;
            response.status = "failure";
            response.message = "Requested Range Not Satisfiable";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// Too many requests api response.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetExpectationFailedApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 417;
            response.status = "failure";
            response.message = "Expectation Failed";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// Too many requests api response.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetUpgradeRequiredApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 426;
            response.status = "failure";
            response.message = "Upgrade Required";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 429 - Too many requests api response.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetTooManyRequestsApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 429;
            response.status = "failure";
            response.message = "Too Many Requests";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        #endregion 4xx Api Response methods


        #region 5xx Api Response


        /// <summary>
        /// Internal Server api response.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetInternalServerErrorApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 500;
            response.status = "failure";
            response.message = "Internal Server Error";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// The selected Response has not been implemented for this version.
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetNotImplementedApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 501;
            response.status = "failure";
            response.message = "Internal Server Error";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 502 - Bad Gateway
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetBadGatewayApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 502;
            response.status = "failure";
            response.message = "Bad Gateway";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 503 - Service is unavailable
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetServiceUnavailableApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 503;
            response.status = "failure";
            response.message = "Service Unavailable";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 504 - Gateway Timeout
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetGatewayTimeoutApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 504;
            response.status = "failure";
            response.message = "Gateway Timeout";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }

        /// <summary>
        /// 505 - Http Version Not Supported
        /// </summary>
        /// <returns></returns>
        private static ApiResponse<T> GetHttpVersionNotSupportedApiResponse(string warning = "")
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.status_code = 505;
            response.status = "failure";
            response.message = "Http Version Not Supported";
            if (!warning.Equals("")) response.warnings.Add(warning);
            return response;
        }



        #endregion 5xx Api Response
    }

    #endregion standardized responses


    /// <summary>
    /// Void type of ApiResponse.
    /// </summary>
    public class EmptyApiResponse
    {

    }
}
