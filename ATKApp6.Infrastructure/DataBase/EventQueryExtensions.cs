using ATKApp6.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ATKApp6.Infrastructure.DataBase
{
    public static class EventQueryExtensions
    {
        public static IQueryable<T> WithBaseIncludes<T>(this IQueryable<T> query)
            where T : EventBase
        {
            return query
                .AsNoTracking()
                .Include(e => e.Categories)
                .Include(e => e.MediaLinks)
                .Include(e => e.Theme)
                .Include(e => e.Organizer);
        }
    }
}
