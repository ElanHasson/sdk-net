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
using FluentValidation;
using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
    /// <summary>
    /// Represents the service used to validate <see cref="BasicAuthenticationProperties"/>s
    /// </summary>
    internal class BasicAuthenticationPropertiesValidator
        : AbstractValidator<BasicAuthenticationProperties>
    {

        /// <summary>
        /// Initializes a new <see cref="BasicAuthenticationPropertiesValidator"/>
        /// </summary>
        public BasicAuthenticationPropertiesValidator()
        {
            this.RuleFor(p => p.Username)
                .NotEmpty();
            this.RuleFor(p => p.Password)
                .NotEmpty();
        }

    }

}
