﻿// Copyright © 2023-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the service used to validate <see cref="SubflowReference"/>s
/// </summary>
public class SubflowReferenceValidator
    : AbstractValidator<SubflowReference>
{

    /// <summary>
    /// Initializes a new <see cref="SubflowReferenceValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="SubflowReference"/>s to validate belong to</param>
    public SubflowReferenceValidator(WorkflowDefinition workflow)
    {
        this.Workflow = workflow;
        this.RuleFor(w => w.WorkflowId)
            .NotEmpty()
            .WithErrorCode($"{nameof(SubflowReference)}.{nameof(SubflowReference.WorkflowId)}");
    }

    /// <summary>
    /// Gets the <see cref="WorkflowDefinition"/> the <see cref="FunctionReference"/>s to validate belong to
    /// </summary>
    protected WorkflowDefinition Workflow { get; }

}
