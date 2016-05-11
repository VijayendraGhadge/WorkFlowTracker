using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkFlowTracker.Models
{
    public class CreateNewStepModels
    {
        [Required(ErrorMessage = "Estimated Start Date Required")]
        [Display(Name = "Estimated Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:mm/dd/yyyy}")]//to do date must be greater than current date.
        public DateTime EstStartDate { get; set; }

        [Required(ErrorMessage = "Estimated End Date Required")]
        [Display(Name = "Estimated End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:mm/dd/yyyy}")]//to do date must be greater than current date.
        public DateTime EstEndDate { get; set; }

        [Required(ErrorMessage = "Status Required")]
        [Display(Name = "Status")]
        public String Status { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [Display(Name = "Step name")]
        public String StepName { get; set; }

        public String ProjectName { get; set; }
    }

    public class StepAndComments
    {
        public  EntityLayer.step Step { get; set; }
        public IEnumerable<EntityLayer.comment> Comments { get; set; }
    }

    public class CommentsAndJustifications
    {
        public IEnumerable<EntityLayer.comment> Comments { get; set; }
        public IEnumerable<EntityLayer.justification> Justifications { get; set; }
    }
}