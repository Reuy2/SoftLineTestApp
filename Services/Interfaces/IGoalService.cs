using SoftLineTestApp.Data.Entity;
using SoftLineTestApp.Domain.Responses;
using SoftLineTestApp.Domain.ViewEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftLineTestApp.Services.Interfaces
{
    public interface IGoalService
    {
        public Task<BaseResponse<IEnumerable<Goal>>> GetGoals();
        public Task<BaseResponse<Goal>> GetGoal(int id);
        public Task<BaseResponse<Goal>> CreateGoal(GoalViewEntity goal);
        public Task<BaseResponse<Goal>> DeleteGoal(GoalViewEntity goal);
        public Task<BaseResponse<Goal>> EditGoal(GoalEditViewEntity goal);
    }
}
