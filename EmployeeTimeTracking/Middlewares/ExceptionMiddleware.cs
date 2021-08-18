using Autofac;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Middlewares
{
    public class ExceptionMiddleware :IResolveMiddleware
    {
        public PipelinePhase Phase => PipelinePhase.ParameterSelection;
        private readonly ILogger _logger;

        public ExceptionMiddleware(ILogger logger)
        {
            _logger = logger;
        }

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            try
            {
                next.Invoke(context);
            }
            catch(Exception ex)
            {
                _logger.Error($"Error  : {ex.Message}");
            }
        }
    }
}
