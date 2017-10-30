using StudentSystem2.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StudentSystem2.Domain
{
    public class Resource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ResourceType ResourceKind { get; set; }

        public string URL { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public List<License> Licenses { get; set; } = new List<License>();
    }
}
//•	Resources: id, name, type of resource (video / presentation / document / other), URL