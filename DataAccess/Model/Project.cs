using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public int ProjectCategoryId { get; set; }

        [ForeignKey("ProjectCategoryId")]
        public ProjectCategory ProjectCategory { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<ProjectTime> ProjectTimes { get; set; }
        public ICollection<ProjectMember> ProjectMembers { get; set; }
    }
}
