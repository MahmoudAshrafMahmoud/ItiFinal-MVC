using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.SharedModels;
using System.Web;
using System.Net.Mail;
using System.IO;

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


        //Get User_id After Signup
        public int GetUSerID(string email)
        {
            var get = context.User_table.Where(x => x.User_Email == email).Select(x=>x.User_Id).FirstOrDefault();
            return get;
        }


        //User Register (Sign Up)
        public bool Sign_Up_As_User(UserModel newUser)
        {
            bool Check;
            byte[] fileData = null;
            var binaryReader = new BinaryReader(newUser.ProfilePicture.InputStream);
            fileData = binaryReader.ReadBytes(newUser.ProfilePicture.ContentLength);

            var RegistUser = (from RegUser in context.User_table
                              where RegUser.User_Email.Equals(newUser.email)
                              select RegUser.User_Email
                              ).FirstOrDefault();


            if (RegistUser==null)
            {
                User_table user = new User_table();

                user.FName = newUser.fname;
                user.LName = newUser.lname;
                user.User_Name = newUser.username;
                user.User_Email = newUser.email;
                user.Password = newUser.password;
                user.PhoneNumber = newUser.phone;
                user.SSN = newUser.ssn;
                user.Type_id = 1;
                user.ProfilePicture = fileData;
                user.Gender = newUser.gender;
                user.Bio = newUser.Bio;
                user.rating = 0;

                context.User_table.Add(user);
                context.SaveChanges();
                Check = true;
            }

            else
            {
                Check = false;
            }

            return Check;

        }


        //Insert SubscribedCategories in Subscribtion table

        public void putSubscribeCategories(int[] Categories,int id)
        {

            Subscribtion_table subscribe = new Subscribtion_table();
            subscribe.User_Id =id ;



 //         Enter Session           subscribe.User_Id =;



            for (int i = 0; i < Categories.Length; i++)
            {
                subscribe.Cat_Id = Categories[i];
                context.Subscribtion_table.Add(subscribe);
                context.SaveChanges();
            }
        }



        List<int> VendorshasCategories = new List<int>();
        List<string>VendorsName=new List<string>();
        public List<string> FollowVendorsSuppliedThatCategories(int User_id)
        {
            List<int> SubCategries = context.Subscribtion_table.Where(x => x.User_Id == User_id).Select(x=>x.Cat_Id).ToList();
            for (int i = 0; i < SubCategries.Count(); i++)
            {
                int SelectedCategories = SubCategries[i];
                List<int> Vendors = context.Product_table.Where(x => x.Cat_id == SelectedCategories).Select(x => x.Vendor_id).ToList();


                
                foreach (var item in Vendors)
                {
                    if (VendorshasCategories.Count() == 0)
                    {
                        VendorshasCategories.Add(Vendors[0]);
                    }

                    int count = 0;

                    for (int j = 0; j < VendorshasCategories.Count(); j++)
                    {
                        if (item == VendorshasCategories[j])
                        {
                            continue;
                        }
                        else
                        {
                            count++;
                            
                        }
                    }

                    if (count == VendorshasCategories.Count())
                    {
                        VendorshasCategories.Add(item);

                    }


                }
                
            }

            for (int i = 0; i < VendorshasCategories.Count(); i++)
            {
                int usrid = VendorshasCategories[i];
                var VendorName = context.User_table.Where(x => x.User_Id == usrid).Select(x => x.User_Name).FirstOrDefault().ToString();
                VendorsName.Add(VendorName);
            }


            return VendorsName;

        }

        //Regist Vendor that selected by user
        List<int>Vendors=new List<int>();
        public void RegisterFollowedVendor(string[] SelectedVendor,int id)
        {
            for (int i = 0; i < SelectedVendor.Length; i++)
            {
                string selected = SelectedVendor[i];
                int vendorid = context.User_table.Where(x => x.User_Name.Equals(selected)).Select(x => x.User_Id).FirstOrDefault();
                Vendors.Add(vendorid);
            }

            Following_table followVendor=new Following_table();

            for (int i = 0; i < Vendors.Count(); i++)
            {
                followVendor.User_id = id;
                followVendor.Vendor_id = Vendors[i];
                context.Following_table.Add(followVendor);
                context.SaveChanges();
            }

        }


        //user regist as vendor
        public string Vendor_Register(string FullName, int NationalID, string SellerInfo, int id)
        {
            var check_type = from type in context.User_table
                             where type.User_Id == id
                             select type.Type_id;

            var check_status = from status in context.Request_table
                               where status.User_Id == id
                               orderby status.reqState descending
                               select status.reqState;

            if (check_type.FirstOrDefault() == 2)
            {
                return ("You are already Vendor");
            }

            else if (check_status.Count() == 0 || check_status.FirstOrDefault().ToLower() == "no")
            {

                UserModel user = new UserModel();
                user.Bio = SellerInfo;
                user.NationalId = NationalID;
                user.FullName = FullName;

                Request_table req = new Request_table();
                req.National_ID = user.NationalId;
                req.Seller_info = user.Bio;
                req.Request_Date = DateTime.Now;
                req.Full_Name = user.FullName;
                req.reqState = "pending";
                req.User_Id = id;

                context.Request_table.Add(req);
                context.SaveChanges();

                return "Your Request completed sucessfully";

            }

            else if (check_status.FirstOrDefault().ToLower() == "pending")
            {
                return "Your account hasn't yet been approved to be avendor. when it is, you will receive an email telling you , your account is approved ";
            }

            else
            {
                return "Please Try Again";
            }




        }

        public List<ProductModel> followedVendorsProducts(int userID)
        {
            var productsList = (from u in context.User_table
                                join f in context.Following_table
                                on u.User_Id equals f.User_id
                                join p in context.Product_table
                                on f.Vendor_id equals p.Vendor_id
                                where u.User_Id == userID && p.State == "approved"
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
                         where p.Product_Id == pid && s.User_Id == userSession.User_Id && p.State == "approved"
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
                     where f.User_id == userSession.User_Id && p.State == "approved"
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
            string MyMail = "projectsalary4444@gmail.com", pwd = "Psalaryproject4444";
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
                                                     "we received your message sucessfully and will reply back  As Son As Possible if needed" +
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

        public List<Category_table> mySubCat(int ID)
        {
            List<Category_table> mysubCategories = new List<Category_table>();
            List<int> categoriesIds = context.Subscribtion_table.Where(s => s.User_Id == ID).Select(s => s.Cat_Id).ToList();
            for(int i =0; i < categoriesIds.Count; i++)
            {
                int catID = categoriesIds[i];
                Category_table mysubCategorie = context.Category_table.FirstOrDefault(s => s.Cat_Id == catID);
                mysubCategories.Add(mysubCategorie);
            }
            return mysubCategories;
        }

        public List<User_table> myFollowedVendor(int ID)
        {
            List<User_table> FollwedUsers = new List<User_table>();
            List<int> FollowedIds = context.Following_table.Where(s => s.User_id == ID).Select(s => s.Vendor_id).ToList();
            for (int i = 0; i < FollowedIds.Count; i++)
            {
                int followid = FollowedIds[i];
                User_table FollwedUser = context.User_table.FirstOrDefault(s => s.User_Id == followid);
                FollwedUsers.Add(FollwedUser);
            }
            return FollwedUsers;
        }
    


        //Get User or Vendor Which is clicked on his profile

        public User_table GetSelectedUser(int id)
        {
            User_table SelectedUser = context.User_table.Where(x => x.User_Id == id && x.Type_id==2).FirstOrDefault();
            return SelectedUser;
        }



        int x = 0,z=0;
        List<int> AllVendors = new List<int>();

        public List<int> PersonsHasSameCategory(int Vendorid)
        {
            

            var query = (from pro in context.Product_table
                         where pro.Vendor_id == Vendorid
                         select pro.Cat_id
                       ).Distinct().ToList();

            for (int i = 0; i < query.Count(); i++)
            {
                int selectCategory = query[i];

                var vendors = (from vendor in context.Product_table
                               where vendor.Cat_id == selectCategory
                               select vendor.Vendor_id
                             ).Distinct().ToList();

                foreach (var item in vendors)
                {
                    AllVendors.Add(item);
                }
                
               
            }

            List<int> SelectedVendors = new List<int>();
            for (int i = 0;i<AllVendors.Count();i++)

            {
                z = 0;
                if (AllVendors[i] == Vendorid)
                {
                    continue;
                }
                else
                {
                    if (i == 0)
                    {
                        SelectedVendors.Add(AllVendors[0]);
                    }
                    else
                    {
                        for (int j = 0; j < i && z==0; j++)
                        {
                            if (AllVendors[i] == AllVendors[j])
                            {
                                continue;
                            }

                            else
                            {
                                SelectedVendors.Add(AllVendors[i]);
                                z = 1;
                            }
                        }
                    }
                }

                    
                }
            


            return SelectedVendors;


           



        }
            
            


        }
    }
