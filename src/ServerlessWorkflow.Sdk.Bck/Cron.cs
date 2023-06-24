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

using Cronos;

namespace ServerlessWorkflow.Sdk
{

    /// <summary>
    /// Defines helper methods to handle CRON expressions
    /// </summary>
    public static class Cron
    {

        /// <summary>
        /// Parses the specified input into a new <see cref="CronExpression"/>
        /// </summary>
        /// <param name="input">The input to parse</param>
        /// <returns>A new <see cref="CronExpression"/></returns>
        public static CronExpression Parse(string input) => CronExpression.Parse(input);

        /// <summary>
        /// Parses the specified input into a new <see cref="CronExpression"/>
        /// </summary>
        /// <param name="input">The input to parse</param>
        /// <param name="cron">The parsed <see cref="CronExpression"/>, if any</param>
        /// <returns>A boolean indicating whether or not the specified input could be parsed</returns>
        public static bool TryParse(string input, out CronExpression? cron)
        {
            cron = default;
            try
            {
                cron = Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
