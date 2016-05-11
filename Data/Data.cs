using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Data
    {
        //public int GetCount()
        //{
        //    using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
        //    {
        //        int temp=0;
        //        foreach (EntityLayer.test str in context.tests)
        //        {
        //            temp++;
        //        }
        //        return temp;
        //    }

        //} //Was for getting id+1 values.

        //public ICollection<String> GetAllEntries()
        //{

        //    this.insert();
        //    using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
        //    {
        //        ICollection<String> L = new List<String>();
        //        foreach (EntityLayer.test str in context.tests)
        //        {
        //            L.Add(str.name.ToString());
        //        }
        //        return L;
        //    }

        //}

        //public void insert()
        //{
        //    using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
        //    {
        //        //      int temp = this.GetCount() + 1;
        //        Random r = new Random();
        //        string t = "test "+r.Next(1000).ToString();

        //        EntityLayer.test x = context.tests.OrderByDescending(c => c.Id).FirstOrDefault();//to get id +1
        //        int newId = (null == x ? 0 : x.Id) + 1;//to get id +1
        //        EntityLayer.test goingin = new EntityLayer.test()
        //        {
        //            Id = newId,
        //            name = t
        //        };

        //        context.tests.Add(goingin);
        //        context.SaveChanges();

        //    }

        //}

        //public int GetProjectCount()
        //{
        //    using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
        //    {
        //        int temp = 0;
        //        foreach (EntityLayer.project str in context.projects)
        //        {
        //            temp++;
        //        }
        //        return temp;
        //    }

        //}


        public List<EntityLayer.project> GetProjects(string email)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {

                int[] proj = (from a in context.accesses
                              where a.uid.Equals(email)
                              select a.pid).ToArray();
                List<EntityLayer.project> result=new List<EntityLayer.project>();
                EntityLayer.project temp;
                foreach (int x in proj)
                {
                    temp= (from p in context.projects
                            where p.pid.Equals(x)
                            select p).ToList().First();
                    result.Add(temp);
                }
                return result;
            }
        }

        public List<EntityLayer.project> GetAllProjectsAdmin()
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                List<EntityLayer.project> result = new List<EntityLayer.project>();
                result = (from p in context.projects
                        select p).ToList();

                return result;
            }
        }

        public List<EntityLayer.step> GAS(int pid)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {

                int[] arr = (from p in context.project_step
                             where p.pid.Equals(pid)
                             select p.sid).ToArray();

                List<EntityLayer.step> result = new List<EntityLayer.step>();
                EntityLayer.step temp;

                foreach (int i in arr)
                {
                    temp = (from s in context.steps
                                  where s.sid.Equals(i)
                                  select s).ToList().First();

                    result.Add(temp);
                }
                return result;
            }
           
        }

        public String GetProjectName(int pid)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                String str = (from p in context.projects
                              where p.pid.Equals(pid)
                              select p.name).ToList().First();

                return str;
            }
        }

        public String GetStepName(int sid)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                String str = (from p in context.steps
                              where p.sid.Equals(sid)
                              select p.name).ToList().First();

                return str;
            }
        }
        public String GetProjectNameFromSid(int sid)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                int proj= (from p in context.project_step
                              where p.sid.Equals(sid)
                              select p.pid).ToList().First();

                String str= (from p in context.projects
                             where p.pid.Equals(proj)
                             select p.name).ToList().First();

                return str;
            }
        }

        public int GetProjectId(string pname)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                int str = (from p in context.projects
                              where p.name.Equals(pname)
                              select p.pid).ToList().First();

                return str;
            }
        }

        public EntityLayer.project GetProjectDetails(int pid)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                EntityLayer.project str = (from p in context.projects
                                        where p.pid.Equals(pid)
                                        select p).ToList().First();

                return str;
            }
        }

        public EntityLayer.step GetStepDetails(int sid)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                EntityLayer.step str = (from p in context.steps
                           where p.sid.Equals(sid)
                           select p).ToList().First();

                return str;
            }
        }

        public IEnumerable<EntityLayer.comment> GetAllComments(int sid)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {

                int[] arr = (from p in context.step_comment
                             where p.sid.Equals(sid)
                             select p.cid).ToArray();

                List<EntityLayer.comment> result = new List<EntityLayer.comment>();
                EntityLayer.comment temp;

                foreach (int i in arr)
                {
                    temp = (from s in context.comments
                            where s.cid.Equals(i)
                            select s).ToList().First();

                    result.Add(temp);
                }
                return result;
            }

        }

        public void ChangeStepStatus(int sid,string status)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                var step = context.steps.Single(u => u.sid == sid);
                
                step.step_status = status;

                context.SaveChanges();
            }
        }

        public int GetProjectIdFromSID(int sid)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                int str = (from p in context.project_step
                           where p.sid.Equals(sid)
                           select p.pid).ToList().First();

                return str;
            }
        }

        private void DateShiftEst (EntityLayer.step s,TimeSpan shift)
        {
            int pid = this.GetProjectIdFromSID(s.sid);
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                int[] steps = (from p in context.project_step
                               where p.pid.Equals(pid)
                               select p.sid).ToArray();
                List<EntityLayer.step> ToEdit = new List<EntityLayer.step>();
                EntityLayer.step temp = new EntityLayer.step();
                foreach (int i in steps)
                {
                    if(i!=s.sid)
                    {
                        temp = (from p in context.steps
                                where p.sid.Equals(i)
                                select p).ToList().First();

                        ToEdit.Add(temp);
                    }
                }

                foreach(EntityLayer.step x in ToEdit)
                {
                    x.est_end_date = x.est_end_date + shift;
           //         x.est_start_date = x.est_start_date + shift;
                }

                context.SaveChanges();


            }
        }

        private void DateShiftActual(EntityLayer.step s, TimeSpan shift)
        {
            int pid = this.GetProjectIdFromSID(s.sid);
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                int[] steps = (from p in context.project_step
                               where p.pid.Equals(pid)
                               select p.sid).ToArray();
                List<EntityLayer.step> ToEdit = new List<EntityLayer.step>();
                EntityLayer.step temp = new EntityLayer.step();
                foreach (int i in steps)
                {
                    if (i != s.sid)
                    {
                        temp = (from p in context.steps
                                where p.sid.Equals(i)
                                select p).ToList().First();

                        ToEdit.Add(temp);
                    }
                }

                foreach (EntityLayer.step x in ToEdit)
                {
                    x.actual_end_date = x.actual_end_date + shift;
               //     x.actual_start_date = x.actual_start_date + shift;
                }

                context.SaveChanges();


            }
        }
        public void EditStep(EntityLayer.step s)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                var step = context.steps.Single(u=>u.sid==s.sid);

                if(s.est_end_date>step.est_end_date)
                {
                    TimeSpan shift = s.est_end_date - step.est_end_date;
                    this.DateShiftEst(s,shift);
                }
                if (step.actual_end_date != null)
                {
                    if (s.actual_end_date > step.actual_end_date)
                    { 
                        TimeSpan shift = s.actual_end_date.GetValueOrDefault(DateTime.Now) - step.actual_end_date.GetValueOrDefault(DateTime.Now);
                        this.DateShiftActual(s, shift);
                    }
                }

                step.name = s.name;
                step.actual_end_date = s.actual_end_date;
                step.actual_start_date = s.actual_start_date;
                step.est_end_date = s.est_end_date;
                step.est_start_date = s.est_start_date;
                step.step_status = s.step_status;

                context.SaveChanges();
            }
        }

        public void EditProject(EntityLayer.project s)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                var prog = context.projects.Single(u => u.pid == s.pid);
                prog.name = s.name;
                prog.actual_doc = s.actual_doc;
                prog.est_doc_cur = s.est_doc_cur;
                prog.status = s.status;         

                context.SaveChanges();
            }
        }

        public string GetUserRole (string email)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                string str = (from p in context.users
                                        where p.uid.Equals(email)
                                        select p.level).ToList().First();

                return str;
            }
        }

        public string GetCreatorid(int pid)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                string str = (from p in context.projects
                              where p.pid.Equals(pid)
                              select p.creator_email).ToList().First();

                return str;
            }
        }

        public List<String> GetAllProjectNames()
        {
            List<String> temp = new List<String>();
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                foreach (EntityLayer.project x in context.projects)
                {
                    temp.Add(x.name);
                }

                return temp;
            }

        }

        public int [] GetProjectSteps(int pid)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                int[] str = (from p in context.project_step
                             where p.pid.Equals(pid)
                             select p.sid).ToArray();

                return str;
            }
        }

        public void Justify(string email, string status,int sid,string text)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
              
                EntityLayer.justification ps = context.justifications.OrderByDescending(c => c.jid).FirstOrDefault();
                int curr_id = (null == ps ? 0 : ps.jid) + 1;

                string old_stat=context.steps.Find(sid).step_status;
                EntityLayer.justification entry = new EntityLayer.justification()
                {
                    jid=curr_id,
                    status_old=old_stat,
                    status_new=status,
                    timestamp=DateTime.Now,
                    uid=email,
                    justification1=text
                };
                EntityLayer.step_justification q = context.step_justification.OrderByDescending(c => c.jid).FirstOrDefault();
                int currentID = (null == q ? 0 : q.Id) + 1;
                EntityLayer.step_justification goingin = new EntityLayer.step_justification()
                {
                    Id=currentID,
                    sid=sid,
                    jid=curr_id
                };

                context.step_justification.Add(goingin);
                context.justifications.Add(entry);
                context.SaveChanges();
            }
        }

        public void CreateTextComment(int sid, string text,string email)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                EntityLayer.comment cs = context.comments.OrderByDescending(c => c.cid).FirstOrDefault();
                int curr_id = (null == cs ? 0 : cs.cid) + 1;
                EntityLayer.comment entry = new EntityLayer.comment()
                {
                   cid=curr_id,
                   text=text,
                   uid=email,
                   isfile=false,
                   timestamp=DateTime.Now
                };

                EntityLayer.step_comment sc = context.step_comment.OrderByDescending(c => c.Id).FirstOrDefault();
                int temp_id = (null == sc ? 0 : cs.cid) + 1;

                EntityLayer.step_comment goingin = new EntityLayer.step_comment()
                {
                    sid = sid,
                    cid=curr_id,
                    Id=temp_id
                };

                context.step_comment.Add(goingin);
                context.comments.Add(entry);
                context.SaveChanges();
            }
        }

        public void CreateFileComment(int sid, string path, string email)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                EntityLayer.comment cs = context.comments.OrderByDescending(c => c.cid).FirstOrDefault();
                int curr_id = (null == cs ? 0 : cs.cid) + 1;
                EntityLayer.comment entry = new EntityLayer.comment()
                {
                    cid = curr_id,
                    uid = email,
                    fpath=path,
                    isfile=true,
                    timestamp = DateTime.Now
                    
                };

                EntityLayer.step_comment sc = context.step_comment.OrderByDescending(c => c.Id).FirstOrDefault();
                int temp_id = (null == sc ? 0 : cs.cid) + 1;

                EntityLayer.step_comment goingin = new EntityLayer.step_comment()
                {
                    sid = sid,
                    cid = curr_id,
                    Id = temp_id
                };

                context.step_comment.Add(goingin);
                context.comments.Add(entry);
                context.SaveChanges();
            }
        }

        public void CreateNewProject(string name, DateTime date, string creator_email)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                List <String> test=this.GetAllProjectNames();
                if (test.Contains(name)) return; //to do : launch exception / give feed back to user
                //int temp = this.GetProjectCount() + 1;
                string creator_name = context.users.Find(creator_email).fname+ " "+context.users.Find(creator_email).lname;
                
                EntityLayer.project x = context.projects.OrderByDescending(c => c.pid).FirstOrDefault();//to get id +1
                int temp = (null == x ? 0 : x.pid) + 1;//to get id +1

                EntityLayer.project goingin = new EntityLayer.project()
                {
                    pid = temp,
                    name = name,
                    creation_time = DateTime.Now,
                    creator_name=creator_name,
                    creator_email=creator_email,
                    est_doc_ori=date
                };

                context.projects.Add(goingin);
                context.SaveChanges();

            }

        }
        

        public void AuthorizeUsers(string project_name , List<String> emails)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                int pid = (from a in context.projects
                           where a.name.Contains(project_name)
                           select a.pid).ToArray().First(); //to do : same project name entring and getting authorization to wrong if later is the one intended.
                EntityLayer.access x = context.accesses.OrderByDescending(c => c.Id).FirstOrDefault();//to get id +1
                int temp = (null == x ? 0 : x.Id) + 1;//to get id +1

                IEnumerable<string> admins = this.GetAllAdmins();
                foreach(string ad in admins)
                {
                    if(emails.Contains(ad))
                    {
                        continue;
                    }
                    else
                    {
                        emails.Add(ad);
                    }
                }

                String creator_email = (from str in context.projects
                                        where str.pid.Equals(pid)
                                        select str.creator_email).ToList().First();

                if(emails.Contains(creator_email))
                {

                }
                else
                {
                    emails.Add(creator_email);
                }

                foreach (String email in emails)
                {
                    EntityLayer.access goingin = new EntityLayer.access()
                    {
                        Id=temp,
                        uid = email,
                        pid = pid
                    };
                    context.accesses.Add(goingin);
                    temp++;
                }
                context.SaveChanges();

            }
        }

        public IEnumerable<string> GetAllAdmins()
        {
            IEnumerable<string> admins = new List<string>();

            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                admins = (from u in context.users
                          where u.level.Equals("admin")
                          select u.email).ToList();
            }

                return admins;
        }

        //public ICollection<String> GetAuthorisedProjects(string email)
        //{
        //    ICollection<String> temp = new List<String>();
        //    using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
        //    {
        //        int [] proj = (from a in context.accesses
        //                        where a.uid.Equals(email)
        //                        select a.pid).ToArray();

        //        foreach (int x in proj)
        //        {
        //            //temp.Add((from p in context.projects
        //            //          where p.pid.Equals(x)
        //            //          select p.name).ToString());
        //            String str = (from p in context.projects
        //                          where p.pid.Equals(x)
        //                          select p.name).First().ToString();
        //            temp.Add(str);
        //        }
        //    }
        //    return temp;
        //}

        public void RegisterUser(string email,string role, string fname,string lname)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                EntityLayer.user goingin = new EntityLayer.user()
                {
                    uid = email,
                    level = role,
                    fname = fname,
                    lname = lname,
                    email = email,
                    status = "enabled",
                    lld = DateTime.Now
                };

                context.users.Add(goingin);
                context.SaveChanges();

            }
        }

        public void UpdateLastLogin(string email)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                EntityLayer.user temp = context.users.Find(email);
                temp.lld = DateTime.Now;
                context.SaveChanges();
            }
        }

        public string FetchLastLogin(string email)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                EntityLayer.user temp = context.users.Find(email);
                return temp.lld.ToString();
            }

        }

        public void UpdateProgress()
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                int[] proj = (from p in context.projects
                              select p.pid).ToArray();
                foreach(int pid in proj)
                {
                    String[] str = (from s in context.project_step
                                    where s.pid.Equals(pid)
                                    select s.step.step_status).ToArray();
                    double count = 0;
                    foreach(String x in str)
                    {
                        if (x.Equals("Completed"))
                            count++;
                    }
                    EntityLayer.project temp = context.projects.Find(pid);
                    if (str.Length != 0)
                    {
                        double progress = (count / str.Length) * 100;                       
                        temp.progress = Convert.ToInt32(progress);
                    }
                    else
                    {
                        temp.progress = 0;
                    }
                    context.SaveChanges();

                }
            }
        }

        //public List<String> GetAllUserFirstNames()
        //{
        //    List<String> temp = new List<String>();
        //    using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
        //    {
        //        foreach(EntityLayer.user x in context.users)
        //        {
        //            temp.Add(x.fname);
        //        }

        //        return temp;
        //    }

        //}

        //public List<String> GetAllUserLastNames()
        //{
        //    List<String> temp = new List<String>();
        //    using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
        //    {
        //        foreach (EntityLayer.user x in context.users)
        //        {
        //            temp.Add(x.lname);
        //        }

        //        return temp;
        //    }

        //}

        public List<String> GetAllUserEmails()
        {
            List<String> temp = new List<String>();
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                foreach (EntityLayer.user x in context.users)
                {
                    temp.Add(x.email);
                }

                return temp;
            }

        }

        public List<IEnumerable<String>> GetAllData()
        {
            List<String> temp;
            List<IEnumerable<String>> final = new List<IEnumerable<String>>();
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                foreach (EntityLayer.user x in context.users)
                {
                    temp = new List<String>();
                    temp.Add(x.email);
                    temp.Add(x.fname);
                    temp.Add(x.lname);
                    final.Add(temp);
                }

                return final;
            }

        }

        public void CreateNewStep(string project_name, string step_name, string status, DateTime est_start_date, DateTime est_end_date)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                EntityLayer.step x = context.steps.OrderByDescending(c => c.sid).FirstOrDefault();//to get id +1
                int temp = (null == x ? 0 : x.sid) + 1;//to get id +1

                int pid = (from a in context.projects
                           where a.name.Contains(project_name)
                           select a.pid).ToArray().First();

                EntityLayer.project_step ps = context.project_step.OrderByDescending(c => c.Id).FirstOrDefault();
                int curr_id = (null == x ? 0 : ps.Id) + 1;
                EntityLayer.project_step entry = new EntityLayer.project_step()
                {
                    sid = temp,
                    pid = pid,
                    Id = curr_id
                };

                EntityLayer.step goingin = new EntityLayer.step()
                {
                    sid = temp,
                    name = step_name,
                    step_status = status,
                    est_start_date = est_start_date,
                    est_end_date= est_end_date
                };

                context.steps.Add(goingin);
                context.project_step.Add(entry);
                context.SaveChanges();
            }
        }

        public bool AccessControlByPID (int pid,string email)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                String[] proj = (from p in context.accesses
                                 where p.pid.Equals(pid)
                                 select p.uid).ToArray();

                if (proj.Contains(email)) return true;
            }


            return false;
        }
        
        public bool AccessControlBySID (int sid,string email)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                int proj = (from p in context.project_step
                                 where p.sid.Equals(sid)
                                 select p.pid).ToArray().First();


                String[] acc = (from p in context.accesses
                                 where p.pid.Equals(proj)
                                 select p.uid).ToArray();

                if (acc.Contains(email)) return true;
            }

            return false;
        }

        public IEnumerable<EntityLayer.justification> StepHistory(int sid)
        {
            using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
            {
                List<EntityLayer.justification> result = new List<EntityLayer.justification>();
                EntityLayer.justification temp = new EntityLayer.justification();

                int[] jus = (from j in context.step_justification
                             where j.sid.Equals(sid)                             
                             select j.jid).ToArray();
                foreach (int i in jus)
                {
                    temp = (from p in context.justifications
                              where p.jid.Equals(i)
                              select p).ToList().First();
                    result.Add(temp);
                }

                return result;
            }
        }
        //public List<String> GetStepInfo(string step_name)
        //{
        //    using (EntityLayer.vvghadgeDataBaseEntities context = new EntityLayer.vvghadgeDataBaseEntities())
        //    {

        //        List<String> result = new List<String>();

        //        string status = (from e in context.steps
        //                         where e.name.Equals(step_name)
        //                         select e.step_status).ToList().First().ToString();

        //        string esd = (from e in context.steps
        //                      where e.name.Equals(step_name)
        //                      select e.est_start_date).ToList().First().ToString();

        //        string eed = (from e in context.steps
        //                      where e.name.Equals(step_name)
        //                      select e.est_end_date).ToList().First().ToString();

        //        result.Add(status);
        //        result.Add(esd);
        //        result.Add(eed);

        //        return result;

        //    }
        //}
    }
}
