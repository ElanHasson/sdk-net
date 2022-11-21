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
/// Enumerates all <see href="https://datatracker.ietf.org/doc/html/rfc6749#section-4">OAuth 2 grant types</see> supported for workflow runtime token generation
/// </summary>
public static class OAuth2GrantType
{

    /// <summary>
    /// Indicates the <see href="https://datatracker.ietf.org/doc/html/rfc6749#section-4.3">resource-owner password credentials grant type</see>
    /// </summary>
    public const string Password = "password";

    /// <summary>
    /// Indicates the <see href="https://datatracker.ietf.org/doc/html/rfc6749#section-4.4">client credentials grant type</see>
    /// </summary>
    public const string ClientCredentials = "client_credentials";

    /// <summary>
    /// Indicates the <see href="https://datatracker.ietf.org/doc/html/rfc8693">token exchange grant type</see>
    /// </summary>
    public const string TokenExchange = "urn:ietf:params:oauth:grant-type:token-exchange";

}
