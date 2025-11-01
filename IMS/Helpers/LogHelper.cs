using IMS.Models;

namespace IMS.Helpers
{
    public class LogHelper
    {
        private readonly ImsContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogHelper(ImsContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _httpContextAccessor = accessor;
        }

        public void LogAction(string actionName, string controllerName)
        {
            var username = _httpContextAccessor.HttpContext?.Session.GetString("Username");

            var log = new UserActivityLog
            {
                Username = username ?? "Unknown",
                Action = actionName,
                Controller = controllerName,
                Timestamp = DateTime.Now
            };

            _context.UserActivityLogs.Add(log);
            _context.SaveChanges();
        }
    }
}
