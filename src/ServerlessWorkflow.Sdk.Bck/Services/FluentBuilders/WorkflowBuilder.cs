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
using Newtonsoft.Json.Schema;
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IWorkflowBuilder"/> interface
    /// </summary>
    public class WorkflowBuilder
        : MetadataContainerBuilder<IWorkflowBuilder>, IWorkflowBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="WorkflowBuilder"/>
        /// </summary>
        public WorkflowBuilder()
        {
            this.Pipeline = new PipelineBuilder(this);
        }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> to configure
        /// </summary>
        protected WorkflowDefinition Workflow { get; } = new WorkflowDefinition();

        /// <summary>
        /// Gets the service used to build the <see cref="WorkflowDefinition"/>'s <see cref="StartDefinition"/> chart
        /// </summary>
        protected IPipelineBuilder Pipeline { get; }

        /// <inheritdoc/>
        public override DynamicObject? Metadata
        {
            get
            {
                return this.Workflow.Metadata;
            }
            protected set
            {
                this.Workflow.Metadata = value;
            }
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            this.Workflow.Key = key;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));
            this.Workflow.Id = id;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            this.Workflow.Name = name;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithDescription(string description)
        {
            this.Workflow.Description = description;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentNullException(nameof(version));
            this.Workflow.Version = version;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithSpecVersion(string specVersion)
        {
            if (string.IsNullOrWhiteSpace(specVersion))
                throw new ArgumentNullException(nameof(specVersion));
            this.Workflow.SpecVersion = specVersion;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithDataInputSchema(Uri uri)
        {
            this.Workflow.DataInputSchemaUri = uri ?? throw new ArgumentNullException(nameof(uri));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithDataInputSchema(JSchema schema)
        {
            this.Workflow.DataInputSchema = new DataInputSchemaDefinition() { Schema = schema } ?? throw new ArgumentNullException(nameof(schema));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithAnnotation(string annotation)
        {
            if (string.IsNullOrWhiteSpace(annotation))
                throw new ArgumentNullException(nameof(annotation));
            if (this.Workflow.Annotations == null)
                this.Workflow.Annotations = new();
            this.Workflow.Annotations.Add(annotation);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder UseExpressionLanguage(string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                throw new ArgumentNullException(nameof(language));
            this.Workflow.ExpressionLanguage = language;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder UseJq()
        {
            return this.UseExpressionLanguage("jq");
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithExecutionTimeout(Action<IWorkflowExecutionTimeoutBuilder> timeoutSetup)
        {
            IWorkflowExecutionTimeoutBuilder builder = new WorkflowExecutionTimeoutBuilder(this.Pipeline);
            timeoutSetup(builder);
            //todo: this.Workflow.ExecutionTimeout = builder.Build();
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder KeepActive(bool keepActive = true)
        {
            this.Workflow.KeepActive = keepActive;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder ImportConstantsFrom(Uri uri)
        {
            this.Workflow.ConstantsUri = uri ?? throw new ArgumentNullException(nameof(uri));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder UseConstants(object constants)
        {
            if (constants == null)
                throw new ArgumentNullException(nameof(constants));
            this.Workflow.Constants = DynamicObject.FromObject(constants);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddConstant(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (this.Workflow.Constants == null)
                this.Workflow.Constants = new();
            this.Workflow.Constants.Set(name, value);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder UseSecrets(IEnumerable<string> secrets)
        {
            this.Workflow.Secrets = secrets?.ToList();
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddSecret(string secret)
        {
            if(this.Workflow.Secrets == null)
                this.Workflow.Secrets = new();
            this.Workflow.Secrets.Add(secret);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder ImportEventsFrom(Uri uri)
        {
            this.Workflow.EventsUri = uri ?? throw new ArgumentNullException(nameof(uri));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddEvent(EventDefinition e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));
            if (this.Workflow.Events == null)
                this.Workflow.Events = new();
            if (this.Workflow.Events.Any(ed => ed.Name == e.Name))
                throw new ArgumentException($"The workflow already defines an event with the specified name '{e.Name}'", nameof(e));
            this.Workflow.Events.Add(e);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddEvent(Action<IEventBuilder> eventSetup)
        {
            if (eventSetup == null)
                throw new ArgumentNullException(nameof(eventSetup));
            IEventBuilder builder = new EventBuilder();
            eventSetup(builder);
            return this.AddEvent(builder.Build());
        }
        
        /// <inheritdoc/>
        public virtual IWorkflowBuilder ImportFunctionsFrom(Uri uri)
        {
            this.Workflow.FunctionsUri = uri ?? throw new ArgumentNullException(nameof(uri));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddFunction(FunctionDefinition function)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));
            if(this.Workflow.Functions == null)
                this.Workflow.Functions = new();
            if (this.Workflow.Functions.Any(fd => fd.Name == function.Name))
                throw new ArgumentException($"The workflow already defines a function with the specified name '{function.Name}'", nameof(function));
            this.Workflow.Functions.Add(function);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddFunction(Action<IFunctionBuilder> functionSetup)
        {
            if (functionSetup == null)
                throw new ArgumentNullException(nameof(functionSetup));
            IFunctionBuilder builder = new FunctionBuilder(this);
            functionSetup(builder);
            return this.AddFunction(builder.Build());
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder ImportRetryStrategiesFrom(Uri uri)
        {
            this.Workflow.RetriesUri = uri ?? throw new ArgumentNullException(nameof(uri));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddRetryStrategy(RetryDefinition strategy)
        {
            if (strategy == null)
                throw new ArgumentNullException(nameof(strategy));
            if(this.Workflow.Retries == null)
                this.Workflow.Retries = new();
            if (this.Workflow.Retries.Any(rs => rs.Name == strategy.Name))
                throw new ArgumentException($"The workflow already defines a function with the specified name '{strategy.Name}'", nameof(strategy));
            this.Workflow.Retries.Add(strategy);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddRetryStrategy(Action<IRetryStrategyBuilder> retryStrategySetup)
        {
            if (retryStrategySetup == null)
                throw new ArgumentNullException(nameof(retryStrategySetup));
            IRetryStrategyBuilder builder = new RetryStrategyBuilder();
            retryStrategySetup(builder);
            return this.AddRetryStrategy(builder.Build());
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder ImportAuthenticationDefinitionsFrom(Uri uri)
        {
            this.Workflow.AuthUri = uri ?? throw new ArgumentNullException(nameof(uri));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder UseAuthenticationDefinitions(params AuthenticationDefinition[] authenticationDefinitions)
        {
            if (authenticationDefinitions == null)
                throw new ArgumentNullException(nameof(authenticationDefinitions));
            this.Workflow.Auth = authenticationDefinitions.ToList();
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddAuthentication(AuthenticationDefinition authenticationDefinition)
        {
            if (authenticationDefinition == null)
                throw new ArgumentNullException(nameof(authenticationDefinition));
            if (this.Workflow.Auth == null)
                this.Workflow.Auth = new();
            this.Workflow.Auth.Add(authenticationDefinition);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddBasicAuthentication(string name, Action<IBasicAuthenticationBuilder> configurationAction)
        {
            IBasicAuthenticationBuilder builder = new BasicAuthenticationBuilder();
            builder.WithName(name);
            configurationAction(builder);
            return AddAuthentication(builder.Build());
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddBearerAuthentication(string name, Action<IBearerAuthenticationBuilder> configurationAction)
        {
            IBearerAuthenticationBuilder builder = new BearerAuthenticationBuilder();
            builder.WithName(name);
            configurationAction(builder);
            return AddAuthentication(builder.Build());
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddOAuth2Authentication(string name, Action<IOAuth2AuthenticationBuilder> configurationAction)
        {
            IOAuth2AuthenticationBuilder builder = new OAuth2AuthenticationBuilder();
            builder.WithName(name);
            configurationAction(builder);
            return AddAuthentication(builder.Build());
        }

        /// <inheritdoc/>
        public virtual IPipelineBuilder StartsWith(StateDefinition state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            this.Pipeline.AddState(state);
            this.Workflow.StartStateName = state.Name;
            return this.Pipeline;
        }

        /// <inheritdoc/>
        public virtual IPipelineBuilder StartsWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup)
        {
            if (stateSetup == null)
                throw new ArgumentNullException(nameof(stateSetup));
            StateDefinition state = this.Pipeline.AddState(stateSetup);
            this.Workflow.StartStateName = state.Name;
            return this.Pipeline;
        }

        /// <inheritdoc/>
        public virtual IPipelineBuilder StartsWith(string name, Func<IStateBuilderFactory, IStateBuilder> stateSetup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (stateSetup == null)
                throw new ArgumentNullException(nameof(stateSetup));
            return this.StartsWith(flow => stateSetup(flow).WithName(name));
        }

        /// <inheritdoc/>
        public virtual IPipelineBuilder StartsWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup, Action<IScheduleBuilder> scheduleSetup)
        {
            if (stateSetup == null) throw new ArgumentNullException(nameof(stateSetup));
            if (scheduleSetup == null) throw new ArgumentNullException(nameof(scheduleSetup));
            var state = this.Pipeline.AddState(stateSetup);
            var schedule = new ScheduleBuilder();
            scheduleSetup(schedule);
            this.Workflow.Start = new() { StateName = state.Name, Schedule = schedule.Build() };
            return this.Pipeline;
        }

        /// <inheritdoc/>
        public virtual IPipelineBuilder StartsWith(string name, Func<IStateBuilderFactory, IStateBuilder> stateSetup, Action<IScheduleBuilder> scheduleSetup)
        {
            if (stateSetup == null) throw new ArgumentNullException(nameof(stateSetup));
            if (scheduleSetup == null) throw new ArgumentNullException(nameof(scheduleSetup));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (stateSetup == null)
                throw new ArgumentNullException(nameof(stateSetup));
            return this.StartsWith(flow => stateSetup(flow).WithName(name), scheduleSetup);
        }

        /// <inheritdoc/>
        public virtual WorkflowDefinition Build()
        {
            this.Workflow.States = this.Pipeline.Build().ToList();
            return this.Workflow;
        }

    }

}
