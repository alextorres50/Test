using System;
namespace Test.Core.Models.WebApi
{
    public class Error
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public object Errors { get; set; }
    }
}

