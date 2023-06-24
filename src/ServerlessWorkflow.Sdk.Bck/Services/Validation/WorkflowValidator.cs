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
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Schema;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServerlessWorkflow.Sdk.Services.Validation
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IWorkflowValidator"/> interface
    /// </summary>
    public class WorkflowValidator
        : IWorkflowValidator
    {

        /// <summary>
        /// Initializes a new <see cref="WorkflowValidator"/>
        /// </summary>
        /// <param name="logger">The service used to perform logging</param>
        /// <param name="externalDefinitionResolver">The service used to resolve external definitions referenced by <see cref="WorkflowDefinition"/>s</param>
        /// <param name="schemaValidator">The service used to validate <see cref="WorkflowDefinition"/>s</param>
        /// <param name="dslValidators">An <see cref="IEnumerable{T}"/> containing the services used to validate Serverless Workflow DSL</param>
        public WorkflowValidator(ILogger<WorkflowValidator> logger, IWorkflowExternalDefinitionResolver externalDefinitionResolver, IWorkflowSchemaValidator schemaValidator, IEnumerable<IValidator<WorkflowDefinition>> dslValidators)
        {
            this.Logger = logger;
            this.ExternalDefinitionResolver = externalDefinitionResolver;
            this.SchemaValidator = schemaValidator;
            this.DslValidators = dslValidators;
        }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the service used to resolve external definitions referenced by <see cref="WorkflowDefinition"/>s
        /// </summary>
        protected IWorkflowExternalDefinitionResolver ExternalDefinitionResolver { get; }

        /// <summary>
        /// Gets the service used to validate <see cref="WorkflowDefinition"/>s
        /// </summary>
        protected IWorkflowSchemaValidator SchemaValidator { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the services used to validate Serverless Workflow DSL
        /// </summary>
        protected IEnumerable<IValidator<WorkflowDefinition>> DslValidators { get; }

        /// <inheritdoc/>
        public virtual async Task<IWorkflowValidationResult> ValidateAsync(WorkflowDefinition workflowDefinition, bool validateSchema = true, bool validateDsl = true, CancellationToken cancellationToken = default)
        {
            workflowDefinition = await this.ExternalDefinitionResolver.LoadExternalDefinitionsAsync(workflowDefinition, new(), cancellationToken);
            IList<ValidationError> schemaValidationErrors = new List<ValidationError>();
            if (validateSchema) schemaValidationErrors = await this.SchemaValidator.ValidateAsync(workflowDefinition, cancellationToken);
            var dslValidationErrors = new List<ValidationFailure>();
            if (validateDsl)
            {
                foreach (var dslValidator in this.DslValidators)
                {
                    var validationResult = await dslValidator.ValidateAsync(workflowDefinition, cancellationToken);
                    if (validationResult.Errors != null)
                        dslValidationErrors.AddRange(validationResult.Errors);
                }
            }
            return new WorkflowValidationResult(schemaValidationErrors, dslValidationErrors);
        }

        /// <summary>
        /// Creates a new default instance of the <see cref="IWorkflowValidator"/> interface
        /// </summary>
        /// <returns>A new <see cref="IWorkflowValidator"/></returns>
        public static IWorkflowValidator Create()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddServerlessWorkflow();
            return services.BuildServiceProvider().GetRequiredService<IWorkflowValidator>();
        }

    }

}
