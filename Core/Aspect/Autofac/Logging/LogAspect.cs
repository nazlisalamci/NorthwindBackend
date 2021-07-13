using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspect.Autofac.Logging
{
   public class LogAspect:MethodInterception
    {
        private LoggerSeviceBase _loggerServiceBase;
        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType!=typeof(LoggerSeviceBase))
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }
            _loggerServiceBase = (LoggerSeviceBase)Activator.CreateInstance(loggerService);
        }

        protected override void OnBefor(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetal(invocation));
        }
        private LogDetail GetLogDetal(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name=invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value=invocation.Arguments[i],
                    Type=invocation.Arguments[i].GetType().Name
                });
            }
            
                


            var logDetail = new LogDetail{
                MethodName = invocation.Method.Name,
                LogParameters=logParameters
            };
            return logDetail;
                
            
        }
    }
}
