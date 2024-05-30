using DataAccess.Entities;

namespace EduWorkBlazor.Services
{
    public static class Mapper
    {
        public static WorkTime ToCommonModel(this DataAccess.Entities.WorkTime entity)
        {
            return new WorkTime
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Date = entity.Date,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime
            };
        }

        public static DataAccess.Entities.WorkTime ToEntity(this WorkTime model)
        {
            return new DataAccess.Entities.WorkTime
            {
                Id = model.Id,
                UserId = model.UserId,
                Date = model.Date,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            };
        }
    }
}
