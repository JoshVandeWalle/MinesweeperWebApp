using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MinesweeperRestService.Models;
using MinesweeperRestService.Services.Business;

namespace MinesweeperRestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public RestDTO GetByID(int id)
        {
            try
            {
                GameRecordService service = new GameRecordService();

                GameRecordModel businessLayerResponseModel = service.RetrieveByID(id);

                if (businessLayerResponseModel == null)
                {
                    return new RestDTO(404, "Game Not Found", null);
                }

                List<Object> data = new List<Object>();

                data.Add(businessLayerResponseModel);

                return new RestDTO(200, "OK", data);
            }

            catch (Exception)
            {
                return new RestDTO(500, "System Error", null);
            }
        }

        public RestDTO GetByName(string name)
        {
            try
            {
                GameRecordService service = new GameRecordService();
                List<Object> data = service.RetrieveByUser(name);

                if (data.Count == 0)
                {
                    return new RestDTO(404, "User not found", null);
                }

                return new RestDTO(200, "OK", data);
            }

            catch (Exception)
            {
                return new RestDTO(500, "System Error", null);
            }
        }

    }
}
