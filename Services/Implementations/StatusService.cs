using Microsoft.EntityFrameworkCore;
using SoftLineTestApp.Data;
using SoftLineTestApp.Data.Entity;
using SoftLineTestApp.Domain.Responses;
using SoftLineTestApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftLineTestApp.Services.Implementations
{
    public class StatusService : IStatusService
    {
        private readonly ApplicationDbContext _db;

        public StatusService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<BaseResponse<IEnumerable<Status>>> GetStatuses()
        {
            var response = new BaseResponse<IEnumerable<Status>>();
            try
            {

                var statuses = _db.Statuses;

                if (statuses is null)
                {
                    response.Description = "Unable to found Statuses";
                    response.StatusCode = Domain.Enums.StatusCode.NotFound;
                    return response;
                }

                response.StatusCode = Domain.Enums.StatusCode.Ok;
                response.Data = statuses;
                return response;
            }
            catch(Exception ex)
            {
                response.Description = ex.Message;
                response.StatusCode = Domain.Enums.StatusCode.InternalServerError;
                return response;
            }

        }
    }
}
