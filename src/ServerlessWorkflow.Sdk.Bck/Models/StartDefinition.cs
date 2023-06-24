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
using System.ComponentModel.DataAnnotations;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents an object used to explicitly define how/when workflow instances should be created
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class StartDefinition
    {

        /// <summary>
        /// Gets/sets the name of the <see cref="WorkflowDefinition"/>'s start state definition. If not defined, defaults to the first defined state
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [ProtoMember(1, IsRequired = true)]
        [DataMember(Order = 1, IsRequired = true)]
        public virtual string StateName { get; set; } = null!;

        /// <summary>
        /// Gets/sets the object used to define the time/repeating intervals at which workflow instances can/should be started
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [ProtoMember(2, IsRequired = true)]
        [DataMember(Order = 2, IsRequired = true)]
        public virtual ScheduleDefinition? Schedule { get; set; }

    }

}
