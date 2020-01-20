using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.CustomExceptions
{
    public class ProjectTypeNotFoundException : Exception
    {
        public int ProjectTypeId { get; set; }

        public ProjectTypeNotFoundException()
        {

        }

        public ProjectTypeNotFoundException(string message)
            : base(message)
        {

        }

        public ProjectTypeNotFoundException(string message, Exception inner)
            : base(message, inner)
        {

        }

        public ProjectTypeNotFoundException(string message, int projectTypeId)
            :this(message)
        {
            ProjectTypeId = projectTypeId;
        }
    }
}
