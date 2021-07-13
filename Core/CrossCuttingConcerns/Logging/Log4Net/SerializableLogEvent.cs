﻿using log4net.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    [Serializable]
   public class SerializableLogEvent
    {
        public LoggingEvent _loggingEvent;
        public SerializableLogEvent(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;

        }
        public object Message => _loggingEvent.MessageObject;
    }
}
