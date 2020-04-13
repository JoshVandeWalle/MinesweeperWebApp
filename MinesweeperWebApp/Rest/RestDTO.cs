using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rest
{
    /*
     * RestDTO combines REST response data of type generic with a status code and error message
     * NOTE: This design adheres to the DTO design pattern
     * @param T the type of data the client requested
     */
    [DataContract]
    public class RestDTO<T>
    {
        // the status code for the API request
        [DataMember]
        public int ResponseCode { get; set; }
        // the message that interprets the JSON for the user
        [DataMember]
        public string Message { get; set; }
        // The requested data
        [DataMember]
        public List<T> Data { get; set; }

        /*
         * non-default constructor initializes object state from parameters
         * @param responseCode the status code
         * @param message the error message
         * @param data generic type data set
         */
        public RestDTO(int responseCode, string message, List<T> data)
        {
            ResponseCode = responseCode;
            Message = message;
            Data = data;
        }
    }
}