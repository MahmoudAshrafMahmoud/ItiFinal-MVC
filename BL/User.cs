using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.SharedModels;
using System.Web;
using System.Net.Mail;

namespace BL
{
    public class User
    {
        CraftsEntities context = new CraftsEntities();

        public User_table login(string User_Email, string Password)
        {
            User_table user = context.User_table.Where(s => s.User_Email.Equals(User_Email) && s.Password.Equals(Password)).FirstOrDefault();
            return user;
        }




        public List<ProductModel> topSeller()
        {

            int[] query = (from o in context.OrderDetails_table
                           join p in context.Product_table
                           on o.Pro_Id equals p.Product_Id

                           group o.Quantity by p.Product_Id into g
                           orderby g.Sum() descending
                           select g.Key).ToArray(); // get the top 5 orders

            List<ProductModel> topSoldProducts = new List<ProductModel>();
            ProductModel[] topSold = new ProductModel[query.Length];
            for (int i = 0; i < query.Length; i++)
            {

                int pid = query[i];

                var x = (from p in context.Product_table
                         where p.Product_Id == pid
                         select new ProductModel
                         {
                             Product_Id = p.Product_Id,
                             Product_Description = p.Product_Description,
                             Product_Name = p.Product_Name,
                             Image = p.Image,
                             Product_Price = p.Product_Price,
                             Vendor_id = p.Vendor_id

                         }).ToList();

                topSoldProducts.AddRange((List<ProductModel>)x);
            }
            return topSoldProducts;
        }

        //user regist as vendor
        //public string Vendor_Register(string FullName, int NationalID, string SellerInfo, int id)
        //{
        //    var check_type = from type in context.User_table
        //                     where type.User_Id == id
        //                     select type.Type_id;

        //    var check_status = from status in context.Request_table
        //                       where status.User_Id == id
        //                       orderby status.reqState descending
        //                       select status.reqState;

        //    if (check_type.FirstOrDefault() == 2)
        //    {
        //        return ("You are already Vendor");
        //    }

        //    else if (check_status.Count() == 0 || check_status.FirstOrDefault().ToLower() == "no")
        //    {

        //        UserModel user = new UserModel();
        //        user.Bio = SellerInfo;
        //        user.NationalId = NationalID;
        //        user.FullName = FullName;

        //        Request_table req = new Request_table();
        //        req.National_ID = user.NationalId;
        //        req.Seller_info = user.Bio;
        //        req.Request_Date = DateTime.Now;
        //        req.Full_Name = user.FullName;
        //        req.reqState = "pending";
        //        req.User_Id = id;

        //        context.Request_table.Add(req);
        //        context.SaveChanges();

        //        return "Your Request completed sucessfully";

        //    }

        //    else if (check_status.FirstOrDefault().ToLower() == "pending")
        //    {
        //        return "Your account hasn't yet been approved to be avendor. when it is, you will receive an email telling you , your account is approved ";
        //    }

        //    else
        //    {
        //        return "Please Try Again";
        //    }




        //}

        public List<ProductModel> followedVendorsProducts(int userID)
        {
            var productsList = (from u in context.User_table
                                join f in context.Following_table
                                on u.User_Id equals f.User_id
                                join p in context.Product_table
                                on f.Vendor_id equals p.Vendor_id
                                where u.User_Id == userID && p.State == "yes"
                                select new ProductModel
                                {
                                    Product_Id = p.Product_Id,
                                    Product_Name = p.Product_Name,
                                    Product_Description = p.Product_Description,
                                    Product_Price = p.Product_Price,
                                    Image = p.Image,
                                    Vendor_id = p.Vendor_id
                                    
                                }).ToList();
            return productsList;
        }



        public List<ProductModel> topSellerSup()
        {
            User_table userSession = (User_table)HttpContext.Current.Session["user"];
            int[] query = (from o in context.OrderDetails_table
                           join p in context.Product_table
                           on o.Pro_Id equals p.Product_Id

                           group o.Quantity by p.Product_Id into g
                           orderby g.Sum() descending
                           select g.Key).ToArray(); // get the top 5 orders

            List<ProductModel> topSoldProducts = new List<ProductModel>();
            ProductModel[] topSold = new ProductModel[query.Length];
            for (int i = 0; i < query.Length; i++)
            {

                int pid = query[i];

                var x = (from p in context.Product_table
                         join s in context.Subscribtion_table
                         on p.Cat_id equals s.Cat_Id
                         where p.Product_Id == pid && s.User_Id == userSession.User_Id && p.State =="yes"
                         select new ProductModel
                         {
                             Product_Id = p.Product_Id,
                             Product_Description = p.Product_Description,
                             Product_Name = p.Product_Name,
                             Image = p.Image,
                             Product_Price = p.Product_Price,
                             Vendor_id = p.Vendor_id

                         }).ToList();

                topSoldProducts.AddRange((List<ProductModel>)x);
            }
            return topSoldProducts;
        }

        public List<ProductModel> lastAdded()
        {
            User_table userSession = (User_table)HttpContext.Current.Session["user"];



            var query = (from p in context.Product_table
                                                  orderby p.Add_Date descending
                                                  select p).ToList(); 

            var x = (from p in query
                     join f in context.Following_table
                     on p.Vendor_id equals f.Vendor_id
                     where f.User_id == userSession.User_Id && p.State == "yes"
                     select new ProductModel
                     {
                         Product_Id = p.Product_Id,
                         Product_Description = p.Product_Description,
                         Product_Name = p.Product_Name,
                         Image = p.Image,
                         Product_Price = p.Product_Price,
                         Vendor_id = p.Vendor_id

                     }).ToList();
            return x;
        }



        public void sendEmail(string Email)
        {
            string  MyMail = "projectsalary4444@gmail.com", pwd = "Psalaryproject4444";
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(MyMail);
                mail.To.Add(Email);
                mail.Subject = "Test Mail";
                mail.IsBodyHtml = true;
                string htmlString = @"<html>" +
                                                "<head>" +
                                               " </head>" +
                                               " <body>" +
                                                     "we received your message sucessfully and will reply back  As Son As Possible if needed"+
                                                "</body>" +
                                                "</html>";


                mail.Body = htmlString;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new
                System.Net.NetworkCredential(MyMail, pwd);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch { }
        }

        public void addMessage(string name, string email, string subject, string message)
        {
            Message_table mt = new Message_table();
            mt.name = name;
            mt.email = email;
            mt.subject = subject;
            mt.message = message;
            context.Message_table.Add(mt);
            context.SaveChanges();
        }

    }
}
