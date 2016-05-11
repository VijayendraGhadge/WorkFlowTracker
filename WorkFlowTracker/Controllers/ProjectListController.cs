using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.UI;

namespace WorkFlowTracker.Controllers
{
    public class ProjectListController : Controller
    {
        [Authorize]
        // GET: ListProject
        public ActionResult ListProjects()
        {

            //ILog log = LogManager.GetLogger("ListProjects");

            //// Log away
            //log.Fatal("This is a fatal error");
            //log.Error("This is an error");
            //log.Warn("This is a warning");
            //log.Debug("This is a debug message");
            //log.Info("This is an info message");

            //log.Fatal("This is a fatal w/ exception", new Exception("some fatal ex"));
            //log.Error("This is an error w/ exception", new Exception("some exception"));

            if(User.Identity.IsAuthenticated)
            {
                Data.Data context = new Data.Data();            
                if(context.GetUserRole(User.Identity.Name).Equals("admin"))
                {
                    return View(context.GetAllProjectsAdmin());
                }
                else
                {
                    return View(context.GetProjects(User.Identity.Name));
                }
            }
            else
            {
                ViewBag.user = "Not authenticated, Please Login!";
            }


            return View();
        }
       
        [Authorize]
        public ActionResult AuthorizeUser(WorkFlowTracker.Models.CreateNewProjectModels m)
        {
            
            ViewBag.ProjectName = m.ProjectName;
            var SelectionModel = new Models.UserSelectionViewModel();
            Data.Data context = new Data.Data();
            int pid = context.GetProjectId(m.ProjectName);
            if (context.GetCreatorid(pid)==User.Identity.Name) //TODO :or admin 
            {
                var UserModel = new Models.UserSelectionViewModel();
                foreach (List<String> l in context.GetAllData())
                {
                    UserModel = new Models.UserSelectionViewModel();
                    UserModel.Email = l[0];
                    UserModel.First_Name = l[1];
                    UserModel.Last_Name = l[2];
                    UserModel.Authorized = false;
                    UserModel.ProjectName = m.ProjectName;
                    SelectionModel.Users.Add(UserModel);
                }

                return View(SelectionModel.Users);
            }
            else
            {
                ILog log = LogManager.GetLogger("AuthoriseUsers");
                log.Fatal("Hack Attempt at authorising users");
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitAuthorizedUser(FormCollection fc)
        {
            var proj_name = Request.Form["ProjectName"];
            Data.Data context = new Data.Data();
            if (context.GetCreatorid(context.GetProjectId(proj_name)) == User.Identity.Name) //TODO :or admin 
            {
                List<String> temp = context.GetAllUserEmails();
                List<String> AuthorisedUsers = new List<string>();
                foreach (String x in temp)
                {
                    var d = Request.Form[x];
                    if (d != null)
                    {
                        if (d.Equals("on"))
                        {
                            //Session["AuthorisedUsers"] = Session["AuthorisedUsers"] + "GotAuthorizedUsers " + x;
                            AuthorisedUsers.Add(x);
                        }
                    }
                }
                context.AuthorizeUsers(proj_name, AuthorisedUsers);
                return RedirectToAction("ListProjects", "ProjectList");
            }
            else
            {

                ILog log = LogManager.GetLogger("Authorise users");
                log.Fatal("Hack Attempt at authorising users");
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        [Authorize]
        public ActionResult CreateNewProject()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Generate(WorkFlowTracker.Models.CreateNewProjectModels CreateModel)
        {
            if (ModelState.IsValid)
            {
                Data.Data temp = new Data.Data();
                temp.CreateNewProject(CreateModel.ProjectName, CreateModel.EstDateOfCompletion, User.Identity.Name);
                //TempData["ProjectName"] = CreateModel.ProjectName;
                return RedirectToAction("AuthorizeUser", "ProjectList",new { CreateModel.ProjectName});
            }

            CreateModel.EstDateOfCompletion = DateTime.Today;

            return View("CreateNewProject", CreateModel);
        }

        [Authorize]
        public ActionResult EditProject(EntityLayer.project model)
        {
            try
            {
                Data.Data context = new Data.Data();
                if (context.AccessControlByPID(model.pid, User.Identity.Name))
                {
                    return View(context.GetProjectDetails(model.pid));
                }
                else
                {
                    ILog log = LogManager.GetLogger("EditProject");
                    log.Fatal("Hack Attempt at Editing projects");
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch (Exception e)
            {
                ILog log = LogManager.GetLogger("EditProject");
                log.Error(e+User.Identity.Name);
                return RedirectToAction("PageNotFound", "Error");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditingProject(EntityLayer.project model, FormCollection fc)
        {
            try
            {
                Data.Data context = new Data.Data();
                if (context.AccessControlByPID(model.pid, User.Identity.Name))
                {
                    // int pid = context.GetProjectId(Convert.ToInt32(Request.Form["StepId"]));
                    if (ModelState.IsValid)
                    {
                        context.EditProject(model);
                        return RedirectToAction("ListProjects", "ProjectList");
                    }
                    else
                    {
                        return View("EditProject", model);
                    }
                }
                else
                {
                    ILog log = LogManager.GetLogger("EditingProject");
                    log.Fatal("Hack Attempt at Editing projects by" + User.Identity.Name);
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch(Exception e)
            {
                ILog log = LogManager.GetLogger("EditingProject");
                log.Error(e+User.Identity.Name);
                return RedirectToAction("PageNotFound", "Error");
            }

        }
        //// GET: ProjectList/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: ProjectList/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ProjectList/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ProjectList/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ProjectList/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {


        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ProjectList/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ProjectList/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {


        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
