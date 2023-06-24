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
    /// Represents an object used to configure a 'Basic' authentication scheme
    /// </summary>
    [DataContract]
    [ProtoContract]
    public class BasicAuthenticationProperties
        : AuthenticationProperties
    {

        /// <summary>
        /// Gets/sets the username to use when authenticating
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [ProtoMember(1, IsRequired = true)]
        [DataMember(Order = 1, IsRequired = true)]
        public virtual string Username { get; set; } = null!;

        /// <summary>
        /// Gets/sets the password to use when authenticating
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [ProtoMember(2, IsRequired = true)]
        [DataMember(Order = 2, IsRequired = true)]
        public virtual string Password { get; set; } = null!;

    }

}
