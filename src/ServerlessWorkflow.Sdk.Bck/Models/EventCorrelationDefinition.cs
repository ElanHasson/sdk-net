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
    /// Represents an object used to define the way to correlate a cloud event
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class EventCorrelationDefinition
    {

        /// <summary>
        /// Gets/sets the cloud event Extension Context Attribute name
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [ProtoMember(1, IsRequired = true)]
        [DataMember(Order = 1, IsRequired = true)]
        public virtual string ContextAttributeName { get; set; } = null!;

        /// <summary>
        /// Gets/sets the cloud event Extension Context Attribute value
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual string? ContextAttributeValue { get; set; }

    }

}