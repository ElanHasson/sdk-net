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
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents an object used to define the execution timeout for a workflow instance
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class WorkflowExecutionTimeoutDefinition
    {

        /// <summary>
        /// Gets/sets the duration after which the workflow's execution will time out
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        [ProtoMember(1, IsRequired = true)]
        [DataMember(Order = 1, IsRequired = true)]
        public virtual TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not to terminate the workflow execution. Defaults to true.
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        [DefaultValue(true)]
        public virtual bool Interrupt { get; set; } = true;

        /// <summary>
        /// Gets/sets the name of a workflow state to be executed before workflow instance is terminated
        /// </summary>
        [ProtoMember(3)]
        [DataMember(Order = 3)]
        public virtual string? RunBefore { get; set; }

    }

}
