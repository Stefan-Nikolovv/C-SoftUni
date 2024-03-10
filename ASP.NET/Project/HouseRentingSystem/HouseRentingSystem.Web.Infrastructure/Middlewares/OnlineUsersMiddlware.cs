

using HouseRentingSystem.Web.Infrastructure.Extentions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace HouseRentingSystem.Web.Infrastructure.Middlewares
{
    public class OnlineUsersMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string cookieName;
        private static readonly ConcurrentDictionary<string, bool> AllKeys = 
            new ConcurrentDictionary<string, bool>();
        private readonly int lastActivityMinutes;
        public OnlineUsersMiddleware(RequestDelegate next, string cookieName = "OnlineUserCookieName", int lastActivityMinutes = 10)
        {
            this.next = next;
            this.cookieName = cookieName;
            this.lastActivityMinutes = lastActivityMinutes;
        }

        public Task InvokeAsync(HttpContext context, IMemoryCache memoryCache)
        {
            if (context.User.Identity?.IsAuthenticated ?? false)
            {
                if (!context.Request.Cookies.TryGetValue(cookieName, out string userId))
                {
                    userId = context.User.GetId()!;

                    context.Response.Cookies.Append(cookieName, userId, new CookieOptions() { HttpOnly =  true, MaxAge = TimeSpan.FromDays(30)});
                }
                memoryCache.GetOrCreate(userId, cacheEntry =>
                {
                    if(!AllKeys.TryAdd(userId, true))
                    {
                            cacheEntry.AbsoluteExpiration = DateTimeOffset.MinValue;
                    }
                    else
                    {
                        cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(lastActivityMinutes);
                        cacheEntry.RegisterPostEvictionCallback(this.RemoveKeyWhenExpired);
                    }
                    return string.Empty;
                });
            }
            else
            {
                if(context.Request.Cookies.TryGetValue(this.cookieName, out string userId))
                {
                    if(AllKeys.TryRemove(userId, out _))
                    {
                        AllKeys.TryUpdate(userId, false, true);
                    }
                    context.Response.Cookies.Delete(this.cookieName);
                }
            }

            return this.next(context);
        }

        public static bool CheckIsUserIsOnline(string userId)
        {
            bool valieTaken = AllKeys.TryGetValue(userId.ToLower(), out bool success);

            return success && valieTaken;
        }

        private void RemoveKeyWhenExpired(object key, object value, EvictionReason eviction, object state)
        {
            string keyStr = (string)key;

            if(!AllKeys.TryRemove(keyStr, out _))
            {
                AllKeys.TryUpdate(keyStr, false, true);
            }

        }
    }
}
