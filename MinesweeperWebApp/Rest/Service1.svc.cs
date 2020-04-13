using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Rest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    /*
     * This class acts as a REST controller and implements the interface contract for the game history REST API
     */
    public class Service1 : IService1
    {
        /*
         * This method gets a game record based on its game ID
         * @param id the game ID
         * @return RestDTO<GameRecordModel> a REST DTO response containing the appropriate data 
         */
        public RestDTO<GameRecordModel> GetGameRecordsByID(string id)
        {
            // use try/catch block to handle exceptions
            try
            {
                // parse the game ID from browser parameter
                int ID = Int32.Parse(id);

                // instantiate service
                GameRecordService service = new GameRecordService();

                // pass control to service and cacth return value
                GameRecordModel responseModel = service.RetrieveByID(ID);

                // check for null return (404)
                if (responseModel == null)
                {
                    // return status code 404
                    return new RestDTO<GameRecordModel>(404, "Game not found", null);
                }

                // put response model in list
                List<GameRecordModel > data = new List<GameRecordModel>();

               data.Add(responseModel);

                // return DTO wih response model and code 200
                return new RestDTO<GameRecordModel>(200, "OK", data);
                
            }

            // handle exceptions
            catch (Exception)
            {
                // return status code 500
                return new RestDTO<GameRecordModel>(500, "System Error", null);
            }
        }

        /*
         * This method returns multiple game records based on a username (email address)
         * @param id the game username the desired user's email
         * @return RestDTO<GameRecordModel> a REST DTO response containing the appropriate data 
         */
        public RestDTO<GameRecordModel> GetGameRecordsByUser(string username)
        {
            // use try/catch block to handle exceptions
            try
            {
                // instantiate service
                GameRecordService service = new GameRecordService();

                // pass control to business layer to get all the user's won games
                List<GameRecordModel> data = service.RetrieveByUser(username);

                // if no games are found
                if (data.Count == 0)
                {
                    // return status code 404
                    return new RestDTO<GameRecordModel>(404, "User not found", null);
                }

                // return status code 200 and the data
                return new RestDTO<GameRecordModel>(200, "OK", data);
            }

            // handle excpetions
            catch (Exception)
            {
                // return status code 500
                return new RestDTO<GameRecordModel>(500, "System Error", null);
            }
        }

        public string hello()
        {
            return "hello";
        }
    }
}
