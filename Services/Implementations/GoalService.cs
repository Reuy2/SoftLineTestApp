using Microsoft.EntityFrameworkCore;
using SoftLineTestApp.Data;
using SoftLineTestApp.Data.Entity;
using SoftLineTestApp.Domain.Enums;
using SoftLineTestApp.Domain.Responses;
using SoftLineTestApp.Domain.ViewEntities;
using SoftLineTestApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftLineTestApp.Services.Implementations
{
    public class GoalService : IGoalService
    {
        private readonly ApplicationDbContext _db;

        public GoalService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<BaseResponse<Goal>> CreateGoal(GoalViewEntity goalViewModel)
        {
            var response = new BaseResponse<Goal>();

            try
            {
                var goal = new Goal();

                goal.Name = goalViewModel.Name;
                goal.StatusId = goalViewModel.StatusId;
                goal.Description = goalViewModel.Description;

                _db.Add(goal);
                await _db.SaveChangesAsync();

                goal = await _db.Goals.Join(_db.Statuses, x => x.StatusId, y => y.Id, (x, y) => new Goal()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    StatusId = x.StatusId,
                    Status = y
                }).FirstOrDefaultAsync(x=> x.Name == goal.Name && x.StatusId == goal.StatusId && x.Description == goal.Description);
                
                if(goal is null)
                {
                    response.StatusCode = StatusCode.InternalServerError;
                    response.Description = "Error While Adding Goal";
                    return response;
                }

                response.Data = goal;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Description = ex.Message;
                return response;
            }
        }

        public async Task<BaseResponse<Goal>> DeleteGoal(GoalViewEntity goalViewModel)
        {
            var response = new BaseResponse<Goal>();

            try
            {
                var goal = await _db.Goals.Join(_db.Statuses, x => x.StatusId, y => y.Id, (x, y) => new Goal()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    StatusId = x.StatusId,
                    Status = y
                }).FirstOrDefaultAsync(x => x.Name == goalViewModel.Name && x.Description == goalViewModel.Description && x.StatusId == goalViewModel.StatusId);

                if (goal is null)
                {
                    response.Description = "Goal Not Found";
                    response.StatusCode = StatusCode.NotFound;
                    return response;
                }

                _db.Goals.Remove(goal);
                await _db.SaveChangesAsync();

                response.Data = goal;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Description = ex.Message;
                return response;
            }
        }

        public async Task<BaseResponse<Goal>> EditGoal(GoalEditViewEntity goalViewModel)
        {
            var response = new BaseResponse<Goal>();

            try
            {
                var goal = await _db.Goals.FirstOrDefaultAsync(x => x.Id == goalViewModel.Id);

                if (goal is null)
                {
                    response.Description = "Goal Not Found";
                    response.StatusCode = StatusCode.NotFound;
                    return response;
                }

                goal.Name = goalViewModel.Name;
                goal.StatusId = goalViewModel.StatusId;
                goal.Description = goalViewModel.Description;

                _db.Update(goal);
                await _db.SaveChangesAsync();

                goal = await _db.Goals.Join(_db.Statuses, x => x.StatusId, y => y.Id, (x, y) => new Goal()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    StatusId = x.StatusId,
                    Status = y
                }).FirstOrDefaultAsync(x => x.Id == goal.Id);

                response.Data = goal;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Description = ex.Message;
                return response;
            }
        }

        public async Task<BaseResponse<Goal>> GetGoal(int id)
        {
            var response = new BaseResponse<Goal>();

            try
            {
                var goal = await _db.Goals.Join(_db.Statuses, x => x.StatusId, y => y.Id, (x, y) => new Goal()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    StatusId = x.StatusId,
                    Status = y
                }).FirstOrDefaultAsync(x=> x.Id == id);

                if (goal is null)
                {
                    response.Description = "Goal Not Found";
                    response.StatusCode = StatusCode.NotFound;
                    return response;
                }

                response.Data = goal;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Description = "Internal Server Error";
                return response;
            }
        }

        public async Task<BaseResponse<IEnumerable<Goal>>> GetGoals()
        {
            var response = new BaseResponse<IEnumerable<Goal>>();

            try
            {
                var goals = _db.Goals.Join(_db.Statuses, x => x.StatusId, y => y.Id, (x, y) => new Goal(){
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    StatusId = x.StatusId,
                    Status = y }) ;

                if(goals is null)
                {
                    response.Description = "Goals Not Found";
                    response.StatusCode = StatusCode.NotFound;
                    return response;
                }

                response.Data = goals;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Description = "Internal Server Error";
                return response;
            }
        }
    }
}
