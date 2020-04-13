using System;
using System.Collections.Generic;

namespace MinesweeperRest
{
    public class RestDTO
    {
        public int ResponseCode { get; set; }
        public string Message { get; set; }
        public List<Object> Data { get; set; }

        public RestDTO(int responseCode, string message, List<object> data)
        {
            ResponseCode = responseCode;
            Message = message;
            Data = data;
        }
    }
}