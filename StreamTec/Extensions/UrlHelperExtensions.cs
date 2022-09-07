using Microsoft.AspNetCore.Mvc;

namespace StreamTec.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string GetLocalUrl(this IUrlHelper urlHelper, string localUrl)
        {
            if (!urlHelper.IsLocalUrl(localUrl))
            {
                return urlHelper!.Page("/Stream/Index");
            }

            return localUrl;
        }
    }
}
