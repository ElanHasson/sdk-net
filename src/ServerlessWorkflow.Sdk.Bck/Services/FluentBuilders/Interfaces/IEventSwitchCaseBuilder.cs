﻿/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using CloudNative.CloudEvents;
using ServerlessWorkflow.Sdk.Models;
using System;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="CloudEvent"/>-based <see cref="SwitchCaseDefinition"/>
    /// </summary>
    public interface IEventSwitchCaseBuilder
         : ISwitchCaseBuilder<IEventSwitchCaseBuilder>
    {

        /// <summary>
        /// Creates and configures a new outcome when consuming any of the specified events
        /// </summary>
        /// <param name="e">The reference name of the <see cref="EventDefinition"/>s to consume</param>
        /// <returns>A new <see cref="IStateOutcomeBuilder"/> used to build the outcome of the consumed <see cref="CloudEvent"/>s</returns>
        IStateOutcomeBuilder On(string e);

        /// <summary>
        /// Creates and configures a new outcome when consuming any of the specified events
        /// </summary>
        /// <param name="eventSetup">The <see cref="Action{T}"/> used to build the <see cref="EventDefinition"/>s to consume</param>
        /// <returns>A new <see cref="IStateOutcomeBuilder"/> used to build the outcome of the consumed <see cref="CloudEvent"/>s</returns>
        IStateOutcomeBuilder On(Action<IEventBuilder> eventSetup);

        /// <summary>
        /// Creates and configures a new outcome when consuming any of the specified events
        /// </summary>
        /// <param name="e">The <see cref="EventDefinition"/>s to consume</param>
        /// <returns>A new <see cref="IStateOutcomeBuilder"/> used to build the outcome of the consumed <see cref="CloudEvent"/>s</returns>
        IStateOutcomeBuilder On(EventDefinition e);

        /// <summary>
        /// Builds the <see cref="EventCaseDefinition"/>
        /// </summary>
        /// <returns>A new <see cref="EventCaseDefinition"/></returns>
        new EventCaseDefinition Build();

    }

}
