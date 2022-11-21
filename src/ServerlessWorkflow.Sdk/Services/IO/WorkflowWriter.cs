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
using Microsoft.Extensions.DependencyInjection;
using ServerlessWorkflow.Sdk.Models;
using System;
using System.IO;

namespace ServerlessWorkflow.Sdk.Services.IO
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IWorkflowWriter"/> interface
    /// </summary>
    public class WorkflowWriter
        : IWorkflowWriter
    {

        /// <summary>
        /// Initializes a new <see cref="WorkflowWriter"/>
        /// </summary>
        /// <param name="jsonSerializer">The service used to serialize and deserialize JSON</param>
        /// <param name="yamlSerializer">The service used to serialize and deserialize YAML</param>
        public WorkflowWriter(IJsonSerializer jsonSerializer, IYamlSerializer yamlSerializer)
        {
            this.JsonSerializer = jsonSerializer;
            this.YamlSerializer = yamlSerializer;
        }

        /// <summary>
        /// Gets the service used to serialize and deserialize JSON
        /// </summary>
        protected IJsonSerializer JsonSerializer { get; }

        /// <summary>
        /// Gets the service used to serialize and deserialize YAML
        /// </summary>
        protected IYamlSerializer YamlSerializer { get; }

        /// <inheritdoc/>
        public virtual void Write(WorkflowDefinition workflow, Stream stream, string format = WorkflowDefinitionFormat.Yaml)
        {
            if (workflow == null)
                throw new ArgumentNullException(nameof(workflow));
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            Neuroglia.Serialization.ISerializer serializer = format switch
            {
                WorkflowDefinitionFormat.Json => this.JsonSerializer,
                WorkflowDefinitionFormat.Yaml => this.YamlSerializer,
                _ => throw new NotSupportedException($"The specified workflow definition format '{format}' is not supported"),
            };
            serializer.Serialize(workflow, stream);
        }

        /// <summary>
        /// Creates a new default instance of the <see cref="IWorkflowWriter"/> interface
        /// </summary>
        /// <returns>A new <see cref="IWorkflowWriter"/></returns>
        public static IWorkflowWriter Create()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddServerlessWorkflow();
            return services.BuildServiceProvider().GetRequiredService<IWorkflowWriter>();
        }

    }

}
