using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftLineTestApp.Domain.ViewEntities;
using SoftLineTestApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftLineTestApp.Controllers
{
    [ApiController]
    [Route("/api")]
    public class ApiController : ControllerBase
    {
        private readonly IGoalService _goalService;

        private readonly IStatusService _statusService;

        public ApiController(IGoalService goal,IStatusService statusService)
        {
            _goalService = goal;
            _statusService = statusService;
        }
        // GET: ApiController
        [HttpGet]
        [Route("/api/getGoals")]
        public async Task<ActionResult> GetGoals()
        {
            var result = await _goalService.GetGoals();
            if(result.StatusCode == Domain.Enums.StatusCode.Ok)
            {
                return new OkObjectResult(result.Data);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        [HttpGet]
        [Route("/api/getStatuses")]
        public async Task<ActionResult> GetStatuses()
        {
            var result = await _statusService.GetStatuses();

            if(result.StatusCode != Domain.Enums.StatusCode.Ok)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result.Data);
        }

        [HttpPost]
        [Route("/api/addGoal")]
        public async Task<ActionResult> AddGoal([FromForm] GoalViewEntity entity)
        {
            var result = await _goalService.CreateGoal(entity);

            if (result.StatusCode != Domain.Enums.StatusCode.Ok)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result.Data);
        }

        [HttpPost]
        [Route("/api/deleteGoal")]
        public async Task<ActionResult> DeleteGoal([FromForm] GoalViewEntity entity)
        {
            var result = await _goalService.DeleteGoal(entity);

            if(result.StatusCode != Domain.Enums.StatusCode.Ok)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result.Data);
        }

        [HttpPost]
        [Route("/api/editGoal")]
        public async Task<ActionResult> EditGoal([FromForm] GoalEditViewEntity entity)
        {
            var result = await _goalService.EditGoal(entity);

            if (result.StatusCode != Domain.Enums.StatusCode.Ok)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result.Data);
        }
    }
}
