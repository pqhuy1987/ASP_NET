namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;



    public class ProjectViewModel
    {
        public List<Project> Project { get; set; }
        public Project SelectedProject { get; set; }
        public string DisplayMode { get; set; }
    }

    [Table("Project")]
    public partial class Project
    {

        public int ID { get; set; }

        [StringLength(100)]
        public string Project_Name { get; set; }
    }
}
