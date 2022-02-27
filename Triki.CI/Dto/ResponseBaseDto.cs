using System;
using System.Collections.Generic;
using System.Text;

namespace Triki.CI.Dto
{
    public class ResponseBaseDto
    {
        public bool sucess { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        public ResponseBaseDto(bool sucess, string message, object data)
        {
            this.sucess=sucess;
            this.message=message;
            this.data=data;
        }

        public ResponseBaseDto()
        {

        }
    }
}
