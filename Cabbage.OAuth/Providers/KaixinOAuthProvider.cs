﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Cabbage.OAuth.Models;
using Cabbage.QuickLogOn.Providers;
using System.Web.Mvc;

namespace Cabbage.OAuth.Providers
{
    [OrchardFeature("Cabbage.OAuth.Kaixin")]
    public class KaixinOAuthProvider : IQuickLogOnProvider
    {
        public const string Url = "http://api.kaixin001.com/oauth2/authorize?response_type=code&client_id={0}&redirect_uri={1}&state={2}{3}";

        public string Name
        {
            get { return "开心网"; }
        }

        public string Description
        {
            get { return "开心网快速登录"; }
        }

        public string GetLogOnUrl(WorkContext context)
        {
            var urlHelper = new UrlHelper(context.HttpContext.Request.RequestContext);
            var part = context.CurrentSite.As<KaixinSettingsPart>();
            var clientId = part.ClientId;
            var additional = part.Additional;
            //用于第三方应用防止CSRF攻击，成功授权后回调时会原样带回。请务必严格按照流程检查用户与state参数状态的绑定.
            var returnUrl = context.HttpContext.Request.Url;
            var state = urlHelper.Encode(returnUrl.ToString());
            var redirectUrl = new Uri(returnUrl, urlHelper.Action("Auth", "KaixinOAuth", new { Area = "Cabbage.OAuth" })).ToString();
            return string.Format(Url, clientId, urlHelper.Encode(redirectUrl), state, (string.IsNullOrWhiteSpace(additional) ? "" : ("&" + additional)));
        }


        public QuickLogOn.Models.QuickLogOnUserInfo GetUserInfo(Orchard.Security.IUser user)
        {
            throw new NotImplementedException();
        }
    }
}
