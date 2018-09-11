using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Future.Api.Models
{
    public class Sync
    {
        public int Id { get; set; }

        public string Data { get; set; }

        public Status Status { get; set; }
    }

    public enum Status
    {
        Pending,
        Completed
    }
}