using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Rest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    /*
     * This interface is the contract by which the minesweeper REST API for game history is designed
     * 
     */
    [ServiceContract]
    public interface IService1
    {
        /*
         * This is a test method that prints "hello" to the browser
         */
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "SayHello/")]
        string hello();

        /*
         * This method returns one game record based on a game ID
         * @param id the game ID
         * @return RestDTO<GameRecordModel> a REST response object containing the appropriate data 
         */
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetGameRecordsByID/{id}")]
        RestDTO<GameRecordModel> GetGameRecordsByID(string id);

        /*
         * This method returns multiple game records based on a username (email address)
         * @param id the game username the desired user's email
         * @return RestDTO<GameRecordModel> a REST response object containing the appropriate data 
         */
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetGameRecordsByUser/{username}")]
        RestDTO<GameRecordModel> GetGameRecordsByUser(string username);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
       
    }
}
