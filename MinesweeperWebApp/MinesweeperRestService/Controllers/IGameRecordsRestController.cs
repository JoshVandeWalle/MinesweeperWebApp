using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Mvc;
using MinesweeperRestService.Models;

namespace MinesweeperRestService.Controllers
{
    public interface IGameRecordsRestController
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetGameRecordsByUsername/{name}")]
        RestDTO GetByName(string name);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetGameRecordsByID/{id}")]
        RestDTO GetByID(int id);
    }
}