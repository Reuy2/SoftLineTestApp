using SoftLineTestApp.Data.Entity;
using SoftLineTestApp.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftLineTestApp.Services.Interfaces
{
    public interface IStatusService
    {
        public Task<BaseResponse<IEnumerable<Status>>> GetStatuses();
    }
}
