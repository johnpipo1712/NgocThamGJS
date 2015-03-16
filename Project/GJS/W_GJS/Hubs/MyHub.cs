using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using W_GJS.Models;

namespace W_GJS.Hubs
{
    public class MyHub : Hub
    {
       
        static List<UserModel> UsersList = new List<UserModel>();
        static List<MessageModel> MessageList = new List<MessageModel>();

        //-->>>>> ***** Receive Request From Client [  Connect  ] *****
        public void Connect(string Name,string Password, string Email, string Phone,string status)
        {
            var id = Context.ConnectionId;
            string userGroup = "";
            //Manage Hub Class
            //if freeflag==0 ==> Busy
            //if freeflag==1 ==> Free

            //if tpflag==0 ==> User
            //if tpflag==1 ==> Admin


            GJSEntities db_gjs = new GJSEntities();
            S_USER User = null;
            if (status == "1")
            {
                 User = db_gjs.S_USER.Where(t => t.USER_NAME == Name && t.USER_PASS == Password).FirstOrDefault();
            }



            try
            {
                //You can check if user or admin did not login before by below line which is an if condition
                //if (UsersList.Count(x => x.ConnectionId == id) == 0)

                //Here you check if there is no userGroup which is same DepID --> this is User otherwise this is Admin
                //userGroup = DepID

                if (status == "0")
                {

                    //now we encounter ordinary user which needs userGroup and at this step, system assigns the first of free Admin among UsersList
                    UserModel strg = UsersList.Where(t => t.freeflag == "1" && t.tpflag == "1").FirstOrDefault();
                    userGroup = strg.UserGroup;

                    //Admin becomes busy so we assign zero to freeflag which is shown admin is busy
                    strg.freeflag = "0";

                    //now add USER to UsersList
                    UsersList.Add(new UserModel { ConnectionId = id, UserID = Name, UserName = Name, UserGroup = userGroup, freeflag = "0", tpflag = "0", });
                    //whether it is Admin or User now both of them has userGroup and I Join this user or admin to specific group 
                    Groups.Add(Context.ConnectionId, userGroup);
                    Clients.Caller.onConnected(id, Name, Name, userGroup);


                }
                else
                {
                    if (User != null)
                    {
                        if ((int)User.STATUS == 5)
                        {
                            //If user has admin code so admin code is same userGroup
                            //now add ADMIN to UsersList
                            UsersList.Add(new UserModel { ConnectionId = id, AdminID = User.USER_CD, UserName = Name, UserGroup = User.USER_CD.ToString(), freeflag = "1", tpflag = "1" });
                            //whether it is Admin or User now both of them has userGroup and I Join this user or admin to specific group 
                            Groups.Add(Context.ConnectionId, User.USER_CD.ToString());
                            Clients.Caller.onConnected(id, Name, User.USER_CD, User.USER_CD.ToString());

                        }
                        else
                        {
                            string msg = "Bạn không có quyền truy cập vào trang này";
                            //***** Return to Client *****
                            Clients.Caller.NoExistAdmin();
                        }
                    }
                    else
                    {
                        string msg = "Bạn không có quyền truy cập vào trang này";
                        //***** Return to Client *****
                        Clients.Caller.NoExistAdmin();
                    }
                    //else
                    //{
                    //    //now we encounter ordinary user which needs userGroup and at this step, system assigns the first of free Admin among UsersList
                    //    UserModel strg = UsersList.Where(t => t.freeflag == "1" && t.tpflag == "1").FirstOrDefault();
                    //    userGroup = strg.UserGroup;

                    //    //Admin becomes busy so we assign zero to freeflag which is shown admin is busy
                    //    strg.freeflag = "0";

                    //    //now add USER to UsersList
                    //    UsersList.Add(new UserModel { ConnectionId = id, UserID = Name, UserName = Name, UserGroup = userGroup, freeflag = "0", tpflag = "0", });
                    //    //whether it is Admin or User now both of them has userGroup and I Join this user or admin to specific group 
                    //    Groups.Add(Context.ConnectionId, userGroup);
                    //    Clients.Caller.onConnected(id, Name, Name, userGroup);

                    //}
                }




            }

            catch
            {
                string msg = "Tất cả các hổ trợ viên đang bận,vui lòng quay lai sau !!!";
                //***** Return to Client *****
                Clients.Caller.NoExistAdmin();

            }


        }
        // <<<<<-- ***** Return to Client [  NoExist  ] *****



        //--group ***** Receive Request From Client [  SendMessageToGroup  ] *****
        public void SendMessageToGroup(string userName, string message,string pst)
        {

            if (UsersList.Count != 0)
            {
                UserModel strg = UsersList.Where(t => t.UserName == userName).FirstOrDefault();
                MessageList.Add(new MessageModel { UserName = userName, Message = message, UserGroup = strg.UserGroup });
                string strgroup = strg.UserGroup;
                // If you want to Broadcast message to all UsersList use below line
                // Clients.All.getMessages(userName, message);

                //If you want to establish peer to peer connection use below line so message will be send just for user and admin who are in same group
                //***** Return to Client *****
                Clients.Group(strgroup).getMessages(userName, message,pst);
            }

        }
        // <<<<<-- ***** Return to Client [  getMessages  ] *****

      
        //--group ***** Receive Request From Client ***** { Whenever User close session then OnDisconneced will be occurs }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled = true)
        {
            var item = UsersList.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                UsersList.Remove(item);

                var id = Context.ConnectionId;

                if (item.tpflag == "0")
                {
                    //user logged off == user
                    try
                    { 
                        var stradmin = (from s in UsersList where (s.UserGroup == item.UserGroup) && (s.tpflag == "1") select s).First();
                        //become free
                        stradmin.freeflag = "1";
                    }
                    catch
                    {
                        //***** Return to Client *****
                        Clients.Caller.NoExistAdmin();
                    }

                }

                //save conversation to dat abase


            }

            return base.OnDisconnected( true); 
        }
    }
}