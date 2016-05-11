using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkFlowTracker.Models
{
    public class CreateNewProjectModels
    {
        [Required (ErrorMessage ="Project Name Required")]
        [StringLength (50,ErrorMessage ="Must be less than 50 chars")]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Estimated Date of completion Required")]
        [Display(Name = "Estimated Date of Completion")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:mm/dd/yyyy}")]//to do date must be greater than current date.
        public DateTime EstDateOfCompletion { get; set; }
    }

    public class Projects
    {
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

    }

    public class ProjectHistory
    {
        public IEnumerable<EntityLayer.comment> Comments { get; set; }
        public IEnumerable<EntityLayer.justification> Justifications { get; set; }
    }

    public class UserSelectionViewModel
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public bool Authorized { get; set; }
        public List<UserSelectionViewModel> Users { get; set; }
        public UserSelectionViewModel()
        {
            this.Users = new List<UserSelectionViewModel>();
        }
        public string ProjectName { get; set; }
    }

    //public class UserModel
    //{
    //    public string FirstName { get; set; }

    //    public string LasttName { get; set; }

    //    public string Email { get; set; }

    //    public bool Authorized { get; set; }
    //}
}