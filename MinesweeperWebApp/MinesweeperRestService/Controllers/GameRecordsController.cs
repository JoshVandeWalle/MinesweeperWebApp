﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinesweeperRestService.Models;
using MinesweeperRestService.Services.Business;

namespace MinesweeperRestService.Controllers
{
    public class GameRecordsController : Controller, IGameRecordsRestController
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