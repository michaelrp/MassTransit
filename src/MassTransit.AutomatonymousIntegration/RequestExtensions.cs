﻿// Copyright 2007-2015 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Automatonymous
{
    using System;
    using Activities;
    using Binders;
    using MassTransit;


    public static class RequestExtensions
    {
        /// <summary>
        /// Send a request to the configured service endpoint, and setup the state machine to accept the response.
        /// </summary>
        /// <typeparam name="TInstance">The state instance type</typeparam>
        /// <typeparam name="TData">The event data type</typeparam>
        /// <typeparam name="TRequest">The request message type</typeparam>
        /// <typeparam name="TResponse">The response message type</typeparam>
        /// <param name="binder">The event binder</param>
        /// <param name="request">The configured request to use</param>
        /// <param name="requestMessageFactory">The request message factory</param>
        /// <returns></returns>
        public static EventActivityBinder<TInstance, TData> Request<TInstance, TData, TRequest, TResponse>(
            this EventActivityBinder<TInstance, TData> binder, Request<TInstance, TRequest, TResponse> request,
            Func<ConsumeContext<TData>, TRequest> requestMessageFactory)
            where TInstance : class, SagaStateMachineInstance
            where TData : class
            where TRequest : class
            where TResponse : class
        {
            var activity = new RequestActivity<TInstance, TData, TRequest, TResponse>(request, requestMessageFactory);

            return binder.Add(activity);
        }
    }
}