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

using Neuroglia;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a workflow state that executes <see cref="BranchDefinition"/>es in parallel
/// </summary>
[DataContract]
[DiscriminatorValue(StateType.Parallel)]
public class ParallelStateDefinition
    : StateDefinition
{

    /// <summary>
    /// Initializes a new <see cref="ParallelStateDefinition"/>
    /// </summary>
    public ParallelStateDefinition() : base(StateType.Parallel) { }

    /// <summary>
    /// Gets/sets an <see cref="List{T}"/> containing the <see cref="BranchDefinition"/>es executed by the <see cref="ParallelStateDefinition"/>
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 6, Name = "branches"), JsonPropertyOrder(6), JsonPropertyName("branches"), YamlMember(Alias = "branches", Order = 6)]
    public virtual List<BranchDefinition> Branches { get; set; } = new List<BranchDefinition>();

    /// <summary>
    /// Gets/sets a value that configures the way the <see cref="ParallelStateDefinition"/> completes. Defaults to 'And'
    /// </summary>
    [DataMember(Order = 7, Name = "completionType"), JsonPropertyOrder(7), JsonPropertyName("completionType"), YamlMember(Alias = "completionType", Order = 7)]
    public virtual string CompletionType { get; set; } = ParallelCompletionType.AllOf;

    /// <summary>
    /// Gets/sets a value that represents the amount of <see cref="BranchDefinition"/>es to complete for completing the state, when <see cref="CompletionType"/> is set to <see cref="ParallelCompletionType.AtLeastN"/>
    /// </summary>
    [DataMember(Order = 8, Name = "n"), JsonPropertyOrder(8), JsonPropertyName("n"), YamlMember(Alias = "n", Order = 8)]
    public virtual uint? N { get; set; }

    /// <summary>
    /// Gets the <see cref="BranchDefinition"/> with the specified name
    /// </summary>
    /// <param name="name">The name of the <see cref="BranchDefinition"/> to get</param>
    /// <returns>The <see cref="BranchDefinition"/> with the specified name</returns>
    public virtual BranchDefinition? GetBranch(string name) => this.Branches.FirstOrDefault(b => b.Name == name);

    /// <summary>
    /// Attempts to get the <see cref="BranchDefinition"/> with the specified name
    /// </summary>
    /// <param name="name">The name of the <see cref="BranchDefinition"/> to get</param>
    /// <param name="branch">The <see cref="BranchDefinition"/> with the specified name</param>
    /// <returns>A boolean indicating whether or not a <see cref="BranchDefinition"/> with the specified name could be found</returns>
    public virtual bool TryGetBranch(string name, out BranchDefinition branch)
    {
        branch = this.GetBranch(name)!;
        return branch != null;
    }

}
