using Microsoft.IdentityModel.Tokens;
using MoodDiaryMVCWebProject.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoodDiaryMVCWebProject.Middlewares
{
    public class CheckJWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CheckJWTMiddleware> _logger;

        public CheckJWTMiddleware(RequestDelegate next, ILogger<CheckJWTMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool isNeedJWTCheck = false;

            //1.获取Controller和Action
            string requestPath = context.Request.Path;

            string logpath = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\log.txt";
            if (!File.Exists(logpath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(logpath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logpath));
                }
                File.Create(logpath).Close();
            }
            await File.AppendAllTextAsync(logpath, $"{DateTime.Now.ToString()}:RequestPath='{requestPath}'\r\n");

            string pathWithGan = requestPath;
            string controllerName = "";
            string actionName = "";

            if (requestPath.Contains("?"))
            {
                pathWithGan = requestPath.Substring(0, requestPath.IndexOf("?"));
            }

            if (requestPath.Count(c => c == '/') == 1)
            {
                pathWithGan = pathWithGan + "/Index";
            }

            if (pathWithGan.Count(c => c == '/') == 2)
            {
                controllerName = pathWithGan.Substring(1, pathWithGan.IndexOf('/', 1) - 1);
                actionName = pathWithGan.Substring(pathWithGan.IndexOf('/', 1) + 1);
            }

            if (!string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(actionName))
            {
                //2.通过反射确定是否被标记验证
                var assembly = typeof(CheckJWTMiddleware).Assembly;
                var isControllerExist = assembly.GetTypes().Where(t => t.Name == controllerName + "Controller").Any();
                if (isControllerExist)
                {
                    var cType = assembly.GetTypes().FirstOrDefault(t => t.Name == controllerName + "Controller");
                    if (cType != null)
                    {
                        var aMethod = cType.GetMethod(actionName);
                        if (aMethod != null)
                        {
                            bool isController_JWTCheck = false;
                            bool isController_JWTIgnore = false;
                            bool isAction_JWTCheck = false;
                            bool isAction_JWTIgnore = false;

                            isController_JWTCheck = cType.CustomAttributes.Any(a => a.AttributeType.Name == "JWTCheckAttribute");
                            isController_JWTIgnore = cType.CustomAttributes.Any(a => a.AttributeType.Name == "JWTIgnoreAttribute");

                            isAction_JWTCheck = aMethod.CustomAttributes.Any(a => a.AttributeType.Name == "JWTCheckAttribute");
                            isAction_JWTIgnore = aMethod.CustomAttributes.Any(a => a.AttributeType.Name == "JWTIgnoreAttribute");

                            if (isAction_JWTCheck)
                            {
                                isNeedJWTCheck = true;
                            }
                            else
                            {
                                if (isController_JWTCheck && !isAction_JWTIgnore)
                                {
                                    isNeedJWTCheck = true;
                                }
                            }

                            await File.AppendAllTextAsync(logpath, $"{DateTime.Now.ToString()}:isController_JWTCheck='{isController_JWTCheck} isController_JWTIgnore='{isController_JWTIgnore} isAction_JWTCheck='{isAction_JWTCheck} isAction_JWTIgnore='{isAction_JWTIgnore} isNeedJWTCheck='{isNeedJWTCheck}'\r\n");

                        }
                    }
                }

            }

            if (isNeedJWTCheck)
            {
                //3.如果需要验证则获取Cookie
                string jwtValue = "";
                context.Request.Cookies.TryGetValue("Authorization", out jwtValue);

                //4.验证Cookie
                if (!string.IsNullOrEmpty(jwtValue))
                {
                    object result;
                    if (ValidateJWT(jwtValue, out result))
                    {
                        //5.从Cookie中获取数据放到Controller中
                        ClaimsPrincipal pp = (ClaimsPrincipal)result;
                        if (pp != null)
                        {
                            foreach (var claim in pp.Claims)
                            {
                                if (claim.Type == "Account")
                                {
                                    SelfDefineController.UserAccount = claim.Value;
                                }
                            }
                        }

                        await _next(context);
                    }
                    else
                    {
                        await context.Response.WriteAsync("JWT失效");
                    }
                }
                else
                {
                    await context.Response.WriteAsync("Cookie中无JWT的值，请先登录！");
                }

            }
            else
            {
                await _next(context);
            }





        }


        private bool ValidateJWT(string jwt, out object msg)
        {
            string keyString = "shdoiwoejnsdncsfpoqieoqwejrnlajdkal";

            var secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.IssuerSigningKey = secKey;
            validationParameters.ValidateIssuer = false;
            validationParameters.ValidateAudience = false;

            try
            {
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken securityToken);
                msg = claimsPrincipal;
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

    }
}
