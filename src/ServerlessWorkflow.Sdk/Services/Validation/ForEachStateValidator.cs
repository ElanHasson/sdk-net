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
using System.Linq;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
    /// <summary>
    /// Represents a service used to validate <see cref="ForEachStateDefinition"/>s
    /// </summary>
    internal class ForEachStateValidator
        : StateDefinitionValidator<ForEachStateDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="ForEachStateValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
        public ForEachStateValidator(WorkflowDefinition workflow)
            : base(workflow)
        {
            this.RuleFor(s => s.Actions)
                .NotEmpty()
                .WithErrorCode($"{nameof(ForEachStateDefinition)}.{nameof(ForEachStateDefinition.Actions)}");
            this.RuleForEach(s => s.Actions)
                .SetValidator(new ActionDefinitionValidator(this.Workflow))
                .When(s => s.Actions != null && s.Actions.Any())
                .WithErrorCode($"{nameof(ForEachStateDefinition)}.{nameof(ForEachStateDefinition.Actions)}");
        }

    }

}
