using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Aspect.Autofac.Exception
{
    public class ExceptionLogAspect:MethodInterception
    {
       public LoggerSeviceBase _loggerServiceBase;
        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType!=typeof(LoggerSeviceBase))
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }
            _loggerServiceBase = (LoggerSeviceBase)Activator.CreateInstance(loggerService);

        }
        protected override void OnException(IInvocation invocation,System.Exception e)
        {
            LogDetailWithExeption logDetailWithExeption = GetLogDetail(invocation);
            logDetailWithExeption.ExeptionMessage = e.Message;
            _loggerServiceBase.Error(logDetailWithExeption);
        }

        private LogDetailWithExeption GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }
            var logDetailWithExeption = new LogDetailWithExeption
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };
            return logDetailWithExeption;
        }
    }
}
