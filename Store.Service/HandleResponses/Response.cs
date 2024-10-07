using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.HandleResponses
{
    public class Response
    {
        public Response(int statusCode,string? message)
        {
            StatusCode=statusCode;
            Message  =message?? GetDefaultMessageForStatusCode(statusCode);
        }
        public int StatusCode {  get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
            => statusCode switch
            {
                100 => "Continue",
                101 => "Switching Protocols",
                200 => "OK",
                201 => "Created",
                202 => "Accepted",
                203 => "Non-Authoritative Information",
                204 => "No Content",
                205 => "Reset Content",
                206 => "Partial Content",
                300 => "Multiple Choices",
                301 => "Moved Permanently",
                302 => "Found",
                303 => "See Other",
                304 => "Not Modified",
                305 => "Use Proxy",
                307 => "Temporary Redirect",
                400 => "Bad Request",
                401 => "Unauthorized",
                402 => "Payment Required",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                501 => "Not Implemented",
                502 => "Bad Gateway",
                503 => "Service Unavailable",
                504 => "Gateway Timeout",
                505 => "HTTP Version Not Supported",
                _ => "Unknown Status Code"
            };
    }
}
