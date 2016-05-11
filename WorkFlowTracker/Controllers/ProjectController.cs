using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkFlowTracker.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult DisplayProject(EntityLayer.project proj)
        {
            try
            {
                Data.Data context = new Data.Data();
                if (context.AccessControlByPID(proj.pid, User.Identity.Name))
                {
                    ViewBag.ProjectName = context.GetProjectName(proj.pid);
                    TempData["ProjectName"] = context.GetProjectName(proj.pid);
                    ViewBag.Role = context.GetUserRole(User.Identity.Name);

                    return View(context.GAS(proj.pid));//result);
                }
                else
                {
                    Session.Clear();
                    Session.Abandon();
                    ILog log = LogManager.GetLogger("DisplayProject");
                    log.Fatal("Hack Attempt at Editing projects by" + User.Identity.Name);

                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch(Exception e)
            {
                ILog log = LogManager.GetLogger("EditProject");
                log.Error(e+User.Identity.Name);
                return RedirectToAction("PageNotFound", "Error");
            }
        }

        [Authorize]
        public ActionResult CalenderView(EntityLayer.project proj)
        {
            
            return View();
        }

        [Authorize]
        // GET: Project/Create
        public ActionResult CreateStep()
        {
            try
            {
            ViewBag.ProjectName = TempData["ProjectName"];
            return View();
            }
            catch
            {
                return RedirectToAction("PageNotFound", "Error");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Generate(WorkFlowTracker.Models.CreateNewStepModels model,FormCollection fc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Data.Data temp = new Data.Data();
                    int pid = temp.GetProjectId(model.ProjectName);
                    if (temp.GetUserRole(User.Identity.Name).Equals("admin"))
                    {
                        temp.CreateNewStep(Request.Form["ProjectName"], model.StepName, model.Status, model.EstStartDate, model.EstEndDate); 
                    }
                    else
                    {
                        ILog log = LogManager.GetLogger("CreateStep");
                        log.Fatal("Hack Attempt at creating steps"+User.Identity.Name);
                        return RedirectToAction("AccessDenied", "Error");
                    }
                    return RedirectToAction("DisplayProject", "Project",new { pid });
                }
                ViewBag.ProjectName = model.ProjectName;
                return View("CreateStep", model);
            }
            catch(Exception e)
            {
                ILog log = LogManager.GetLogger("CreateStep");
                log.Error(e+User.Identity.Name);
                return RedirectToAction("PageNotFound", "Error");
            }
        }
        [Authorize]
        public ActionResult ShowStep(EntityLayer.step s)
        {
            try
            {
                Data.Data context = new Data.Data();
                if (context.AccessControlBySID(s.sid, User.Identity.Name))
                {
                    ViewBag.ProjectName = context.GetProjectNameFromSid(s.sid);
                    var model = new WorkFlowTracker.Models.StepAndComments { Step = context.GetStepDetails(s.sid), Comments = context.GetAllComments(s.sid) };

                    return View(model);
                }
                else
                {
                    ILog log = LogManager.GetLogger("ShowStep");
                    log.Fatal("Hack Attempt at Show steps" + User.Identity.Name);
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch
            {
                ILog log = LogManager.GetLogger("CreateStep");
                log.Error("Hack Attempt at creating steps" + User.Identity.Name);
                return RedirectToAction("PageNotFound", "Error");
            }
        }

        [Authorize]
        public ActionResult EditStep(EntityLayer.step model)
        {
            try
            {
                Data.Data context = new Data.Data();
                if (context.AccessControlBySID(model.sid, User.Identity.Name))
                {
                    return View(context.GetStepDetails(model.sid));
                }
                else
                {
                    ILog log = LogManager.GetLogger("EditStep");
                    log.Fatal("Hack Attempt at Editing steps" + User.Identity.Name);
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch
            {
                ILog log = LogManager.GetLogger("CreateStep");
                log.Error("someting fishy" + User.Identity.Name);
                return RedirectToAction("PageNotFound", "Error");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editor(EntityLayer.step model,FormCollection fc)
        {
            try
            {
                Data.Data context = new Data.Data();
                model.sid = Convert.ToInt32(Request.Form["StepId"]);
                if (context.AccessControlBySID(model.sid, User.Identity.Name))
                {
                    int pid = context.GetProjectIdFromSID(Convert.ToInt32(Request.Form["StepId"]));
                    if (ModelState.IsValid)
                    {
                        context.EditStep(model);
                        context.UpdateProgress();
                        return RedirectToAction("DisplayProject", "Project", new { pid });
                    }
                    return View("EditStep", "Project", model);
                }
                else
                {
                    ILog log = LogManager.GetLogger("EditStep");
                    log.Fatal("Hack Attempt at editing steps" + User.Identity.Name);
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch
            {
                ILog log = LogManager.GetLogger("CreateStep");
                log.Error("fishy with server" + User.Identity.Name);
                return RedirectToAction("PageNotFound", "Error");
            }
           
        }

        [Authorize]
        public ActionResult ChangeStatus (EntityLayer.step s)
        {
            try
            {
                Data.Data context = new Data.Data();
                if (context.AccessControlBySID(s.sid, User.Identity.Name))
                {                    
                    return View(context.GetStepDetails(s.sid));
                }
                else
                {
                    ILog log = LogManager.GetLogger("ChangeStatus");
                    log.Fatal("Hack Attempt at changing status of step" + User.Identity.Name);
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch
            {
                ILog log = LogManager.GetLogger("ChangeStatus");
                log.Error("Fishy" + User.Identity.Name);
                return RedirectToAction("PageNotFound", "Error");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangingStatus(EntityLayer.step m, FormCollection fc)
        {
           // try
            //{
                Data.Data temp = new Data.Data();
                m.sid = Convert.ToInt32(Request.Form["StepId"]);
                if (temp.AccessControlBySID(m.sid, User.Identity.Name))
                {
                    if (ModelState.IsValid)
                    {
                        if (Request.Form["Justify"] == "")
                        {
                            ViewBag.Error = "Empty Justification";
                            return View("ChangeStatus", m);
                        }
                        else
                        {
                            temp.Justify(User.Identity.Name, m.step_status, m.sid, Request.Form["Justify"]);
                            temp.ChangeStepStatus(m.sid, m.step_status);
                            temp.UpdateProgress();
                            return RedirectToAction("ShowStep", "Project", new { m.sid });
                        }
                    }
                    else
                    {
                        return View("ChangeStatus",m);
                    }
                }
                else
                {
                ILog log = LogManager.GetLogger("ChangeStatus");
                log.Fatal("Hack Attempt at changing status of step" + User.Identity.Name);
                return RedirectToAction("AccessDenied", "Error");
                }
          //  }
       //     catch
        //    {
              //  return RedirectToAction("PageNotFound", "Error");
         //   }
        }

        [Authorize]
        public ActionResult AddComment(EntityLayer.step s)
        {
            try
            {
                Data.Data context = new Data.Data();
                if (context.AccessControlBySID(s.sid, User.Identity.Name))
                {
                    ViewBag.StepId = s.sid;
                    ViewBag.StepName = context.GetStepName(s.sid);
                    return View();
                }
                else
                {
                    ILog log = LogManager.GetLogger("AddComment");
                    log.Fatal("Hack Attempt at Adding Comemnt of step" + User.Identity.Name);
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch
            {
                ILog log = LogManager.GetLogger("ChangeStatus");
                log.Error("fishy" + User.Identity.Name);
                return RedirectToAction("PageNotFound", "Error");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddingComment (EntityLayer.comment c, FormCollection fc)
        {
            try
            {
                Data.Data temp = new Data.Data();
                int sid = Convert.ToInt32(Request.Form["StepId"]);
                if (temp.AccessControlBySID(sid, User.Identity.Name))
                {
                    if (ModelState.IsValid)
                    {
                        var test = Request.Form["file"];
                        if (Request.Form["isfile"] == "true")
                        {
                            if (Request.Files.Count > 0)
                            {
                                var file = Request.Files[0];

                                if (file != null && file.ContentLength > 0)
                                {
                                    var fileName = Path.GetFileName(file.FileName);
                                    var path = Path.Combine(Server.MapPath("~/Files/"), fileName);
                                    file.SaveAs(path);
                                    temp.CreateFileComment(sid, file.FileName, User.Identity.Name);
                                }
                                else
                                {

                                    Data.Data context = new Data.Data();
                                    ViewBag.StepId = Request.Form["StepId"];
                                    ViewBag.StepName = context.GetStepName(Convert.ToInt32(Request.Form["StepId"]));
                                    ModelState.AddModelError("text", "Required Field: Please Attach a File !");
                                    return View("AddComment", c);
                                }
                            }
                        }
                        else
                        {
                            if(c.text==""||c.text==null)
                            {
                                Data.Data context = new Data.Data();
                                ViewBag.StepId = Request.Form["StepId"];
                                ViewBag.StepName = context.GetStepName(Convert.ToInt32(Request.Form["StepId"]));
                                ModelState.AddModelError("text", "Required Field: Comment Text Empty !");
                                return View("AddComment", c);
                            }
                            temp.CreateTextComment(sid, c.text, User.Identity.Name);
                        }

                        return RedirectToAction("ShowStep", "Project", new { sid });
                    }
                    else
                    {
                        return View("AddComment", c);
                    }
                }
                else
                {
                    ILog log = LogManager.GetLogger("AddingComment");
                    log.Fatal("Hack Attempt at adding comment of step" + User.Identity.Name);
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch
            {
                ILog log = LogManager.GetLogger("adding comment");
                log.Fatal("ServerError" + User.Identity.Name);
                return RedirectToAction("PageNotFound", "Error");
            }

        }

        [Authorize]
        public ActionResult StepHistory(EntityLayer.step s)
        {
            try
            {
                Data.Data context = new Data.Data();
                if (context.AccessControlBySID(s.sid, User.Identity.Name))
                {
                    ViewBag.StepName = context.GetStepName(s.sid);
                    var model = new WorkFlowTracker.Models.CommentsAndJustifications { Justifications = context.StepHistory(s.sid), Comments = context.GetAllComments(s.sid) };
                    return View(model);
                }
                else
                {
                    ILog log = LogManager.GetLogger("AddingComment");
                    log.Fatal("Hack Attempt at Step History" + User.Identity.Name);
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch
            {
                ILog log = LogManager.GetLogger("History");
                log.Fatal("ServerError" + User.Identity.Name);
                return RedirectToAction("PageNotFound", "Error");
            }
        }

        [Authorize]
        public ActionResult ProjectHistory(EntityLayer.step s)
        {

            try
            {
                Data.Data context = new Data.Data();
                int pid = context.GetProjectIdFromSID(s.sid);
                if (context.AccessControlByPID(pid, User.Identity.Name))
                {
                    ViewBag.ProjectName = context.GetProjectName(pid);
                    var model = new List<WorkFlowTracker.Models.ProjectHistory>();
                    var temp = new WorkFlowTracker.Models.ProjectHistory();
                    int[] steps = context.GetProjectSteps(pid);

                    foreach(int sid in steps)
                    {
                        temp = new WorkFlowTracker.Models.ProjectHistory();
                        temp.Justifications = context.StepHistory(sid);
                        temp.Comments = context.GetAllComments(sid);
                        model.Add(temp);
                    }
                    return View(model);
                }
                else
                {
                    ILog log = LogManager.GetLogger("AddingComment");
                    log.Fatal("Hack Attempt at project history" + User.Identity.Name);
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch
            {
                ILog log = LogManager.GetLogger("ProjectHistory");
                log.Fatal("ServerError" + User.Identity.Name);
                return RedirectToAction("PageNotFound", "Error");
            }
        }
    }
}
