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

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all types of actions
/// </summary>
public static class ActionType
{

    /// <summary>
    /// Indicates an action that invokes a function
    /// </summary>
    public const string Function = "function";

    /// <summary>
    /// Indicates an action that executes a cloud event trigger
    /// </summary>
    public const string Trigger = "trigger";

    /// <summary>
    /// Indicates an action that executes a subflow
    /// </summary>
    public const string Subflow = "subflow";

}
