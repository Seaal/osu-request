using System;

namespace OsuRequest.CrossCutting
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;
        public object Value { get; set; }
    }
}
