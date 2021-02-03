// Copyright (c) Parusnik.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Parusnik.Pcs.MqWebProxy.Api.Models
{
    public class QueueMessage
    {
        public string Topic { get; set; }
        public string Message { get; set; }
    }
}
