using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
        public class ResponseDto
    {
            public int Status { get; set; }
            public string? Message { get; set; }
            public object? Data { get; set; }
            public string? ValidationMessage { get; set; }
            public string? Errormessage { get; set; }

        }
}
