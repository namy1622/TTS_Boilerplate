using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Logging;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Timing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Authorization.Users;

namespace TTS_boilerplate.Bg_Work_Job.BackgroundWorker
{
    public class PassiveUserCheckerWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IRepository<User, long> _userRepository;
        public PassiveUserCheckerWorker(
                AbpTimer timer,
                IRepository<User, long> userRepository
                ) : base(timer)
        {
            _userRepository = userRepository;
            Timer.Period = 5000;
        }

        [UnitOfWork]
        protected override void  DoWork()
        {
            Logger.Info("PassiveUserCheckerWorker is working...");
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var oneMonthAgo = Clock.Now.Subtract(TimeSpan.FromDays(30));

                var inactiveUsers = _userRepository.GetAllList(u =>
                u.IsActive == false && (u.CreationTime < oneMonthAgo));

                foreach (var inactiveUser in inactiveUsers)
                {
                    inactiveUser.IsActive = false;
                    Logger.Info($"User {inactiveUser.UserName} is inactive.");
                }
                CurrentUnitOfWork.SaveChanges();
            }
            
        }
    }
}
